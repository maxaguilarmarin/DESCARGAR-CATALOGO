using System.Collections.Generic;
using System.Threading.Tasks;

namespace DESCARGAR_CATALOGO.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> GetProductosAsync(
            string[]? marcas,
            int? stockMinimo,
            string[]? familias,
            string[]? subFamilias,
            string tipoCatalogo,
            string[]? grupos,
            string[]? skus,
            int[]? campanias
        );
    }

    // DTO usado por Excel y Front
    public sealed class ProductoDto
    {
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string Grupo { get; set; } = "";
        public decimal PrecioLista1 { get; set; }
        public decimal PrecioCliente { get; set; }   // <--- NUEVO (precio con descuento PRIME si aplica)
        public decimal Caja { get; set; }
        public decimal M { get; set; }
        public string UFamilia { get; set; } = "";
        public string USubFamilia { get; set; } = "";
        public string Marca { get; set; } = "";
        public decimal Stock { get; set; }
    }
}
