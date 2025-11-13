using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DESCARGAR_CATALOGO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DESCARGAR_CATALOGO.Services
{
    public class ProductoService : IProductoService
    {
        private readonly TuDbContext _db;
        private readonly ILogger<ProductoService> _logger;

        // Grupos que nunca deben aparecer
        private static readonly HashSet<string> _gruposExcluidos = new HashSet<string>(new[]
        {
            "asesoria legal","descuento 60%","descuento 80%","insumos","mano de obra externa","marketing","redondeo"
        }.Select(s => s.ToLowerInvariant()));

        // Grupos que reciben descuento Prime (-10%)
        private static readonly HashSet<string> _gruposPrime = new HashSet<string>(new[]
        {
            "campaña","campanas","campañas","celebraciones","cumpleaños","cumpleanos","decoracion","decoración",
            "globos","lineas y licencias","líneas y licencias","promocion","promoción","velas"
        }.Select(s => s.ToLowerInvariant()));

        public ProductoService(TuDbContext db, ILogger<ProductoService> logger)
        {
            _db = db;
            _logger = logger;
        }

        // Fila intermedia para evitar tipos anónimos en expresiones
        private sealed class Row
        {
            public Oitm O { get; set; } = null!;
            public Oitb G { get; set; } = null!;
            public Itm1 I { get; set; } = null!;
            public decimal Stock { get; set; }
        }

        public async Task<IEnumerable<ProductoDto>> GetProductosAsync(
            string[]? marcas,
            int? stockMinimo,
            string[]? familias,
            string[]? subFamilias,
            string tipoCatalogo,
            string[]? grupos,
            string[]? skus,
            int[]? campanias)
        {
            var setMarcas = (marcas ?? Array.Empty<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
            var setFamilias = (familias ?? Array.Empty<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
            var setSubFamilias = (subFamilias ?? Array.Empty<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
            var setGruposIncl = (grupos ?? Array.Empty<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
            var setSkus = (skus ?? Array.Empty<string>()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToArray();
            var setCampanias = (campanias ?? Array.Empty<int>()).Distinct().ToArray();

            // ============= BASE =============
            var q =
                from o in _db.Oitms
                join g in _db.Oitbs on o.ItmsGrpCod equals (short?)g.ItmsGrpCod
                join i in _db.Itm1s on o.ItemCode equals i.ItemCode
                where o.ValidFor == "Y"
                      && o.ItmsGrpCod != null
                      && i.PriceList == 1
                      // Ocultar familia/subfamilia '#' e 'insumos'
                      && (o.UFamilia ?? "") != "#"
                      && (o.USubFamilia ?? "") != "#"
                      && (o.UFamilia ?? "").ToLower() != "insumos"
                      && (o.USubFamilia ?? "").ToLower() != "insumos"
                      // Excluir grupos indeseados (en server)
                      && g.ItmsGrpNam != null
                      && !_gruposExcluidos.Contains((g.ItmsGrpNam ?? "").ToLower())
                let stock01 = (
                    from w in _db.Oitws
                    where w.ItemCode == o.ItemCode && w.WhsCode == "01"
                    select ((decimal?)(w.OnHand ?? 0) - (w.IsCommited ?? 0))
                ).Sum() ?? 0m
                let stock02 = (
                    from w in _db.Oitws
                    where w.ItemCode == o.ItemCode && w.WhsCode == "02"
                    select ((decimal?)(w.OnHand ?? 0) - (w.IsCommited ?? 0))
                ).Sum() ?? 0m
                let stock10 = (
                    from w in _db.Oitws
                    where w.ItemCode == o.ItemCode && w.WhsCode == "10"
                    select ((decimal?)(w.OnHand ?? 0) - (w.IsCommited ?? 0))
                ).Sum() ?? 0m
                select new Row
                {
                    O = o,
                    G = g,
                    I = i,
                    Stock = stock01 + stock02 + stock10
                };

            // ============= FILTROS (SIN OPENJSON) =============

            if (setSkus.Length > 0)
                q = WhereIn(q, r => r.O.ItemCode, setSkus);

            if (setMarcas.Length > 0)
                q = WhereIn(q, r => r.O.UMarca!, setMarcas);

            if (setFamilias.Length > 0)
                q = WhereIn(q, r => r.O.UFamilia!, setFamilias);

            if (setSubFamilias.Length > 0)
                q = WhereIn(q, r => r.O.USubFamilia!, setSubFamilias);

            if (setGruposIncl.Length > 0)
                q = WhereIn(q, r => r.G.ItmsGrpNam!, setGruposIncl);

            if (stockMinimo.HasValue)
                q = q.Where(r => r.Stock >= stockMinimo.Value);

            if (setCampanias.Length > 0)
            {
                var campaniaPred = BuildCampaniasPredicate(setCampanias);
                q = q.Where(campaniaPred);
            }

            // ============= PROYECCIÓN + DESCUENTO PRIME =============
            bool esPrimeSolicitado = string.Equals(tipoCatalogo, "prime", StringComparison.OrdinalIgnoreCase);

            var lista = await q
                .OrderBy(r => r.O.ItemCode)
                .Select(r => new ProductoDto
                {
                    ItemCode = r.O.ItemCode,
                    ItemName = r.O.ItemName ?? "",
                    Grupo = r.G.ItmsGrpNam ?? "",
                    PrecioLista1 = r.I.Price ?? 0m,
                    PrecioCliente = (esPrimeSolicitado && _gruposPrime.Contains((r.G.ItmsGrpNam ?? "").Trim().ToLower()))
                                    ? Math.Round((r.I.Price ?? 0m) * 0.9m, 0, MidpointRounding.AwayFromZero)
                                    : (r.I.Price ?? 0m),
                    Caja = r.O.NumInBuy ?? 0m,
                    M = r.O.SalPackUn ?? 0m,
                    UFamilia = r.O.UFamilia ?? "",
                    USubFamilia = r.O.USubFamilia ?? "",
                    Marca = r.O.UMarca ?? "",
                    Stock = r.Stock
                })
                .ToListAsync();

            _logger.LogInformation("GetProductosAsync -> {Count} filas", lista.Count);
            return lista;
        }

        /// <summary>
        /// WHERE x == v1 OR x == v2 OR ... (evita OPENJSON)
        /// </summary>
        private static IQueryable<Row> WhereIn(IQueryable<Row> source, Expression<Func<Row, string>> selector, IEnumerable<string> values)
        {
            var vals = values.Where(v => !string.IsNullOrWhiteSpace(v)).Distinct().ToArray();
            if (vals.Length == 0) return source;

            var param = selector.Parameters[0];
            Expression? body = null;

            foreach (var v in vals)
            {
                // Igualdad exacta (DB ya nos entregó los valores reales para el selector del UI)
                var eq = Expression.Equal(selector.Body, Expression.Constant(v));
                body = body == null ? eq : Expression.OrElse(body, eq);
            }

            var lambda = Expression.Lambda<Func<Row, bool>>(body!, param);
            return source.Where(lambda);
        }

        /// <summary>
        /// (QryGroup1..QryGroup64) == "Y" para cualquiera de las campañas
        /// </summary>
        private static Expression<Func<Row, bool>> BuildCampaniasPredicate(int[] campanias)
        {
            var p = Expression.Parameter(typeof(Row), "r");
            Expression? body = null;

            foreach (var n in campanias)
            {
                if (n < 1 || n > 64) continue;
                var propO = Expression.Property(p, nameof(Row.O));
                var propGroup = Expression.Property(propO, $"QryGroup{n}");
                var constY = Expression.Constant("Y");
                var eq = Expression.Equal(propGroup, constY);
                body = body == null ? eq : Expression.OrElse(body, eq);
            }

            if (body == null)
                body = Expression.Constant(true);

            return Expression.Lambda<Func<Row, bool>>(body, p);
        }
    }
}
