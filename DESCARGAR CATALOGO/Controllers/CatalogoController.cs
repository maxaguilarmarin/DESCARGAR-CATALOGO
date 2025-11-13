using DESCARGAR_CATALOGO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DESCARGAR_CATALOGO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IExcelGeneratorService _excelGenerator;
        private readonly ILogger<CatalogoController> _logger;

        public CatalogoController(
            IProductoService productoService,
            IExcelGeneratorService excelGenerator,
            ILogger<CatalogoController> logger)
        {
            _productoService = productoService;
            _excelGenerator = excelGenerator;
            _logger = logger;
        }

        // ============== POST: generar por filtros ==============
        [HttpPost("generar")]
        [Consumes("application/json")]
        public async Task<IActionResult> GenerarCatalogo([FromBody] FiltrosRequest req)
        {
            try
            {
                // si viene esPrime true, forzamos tipo "prime"
                var tipo = (req.esPrime ?? false) ? "prime" : (req.tipo ?? "general");

                var productos = await _productoService.GetProductosAsync(
                    marcas: req.marca,
                    stockMinimo: req.stockMinimo,
                    familias: req.familia,
                    subFamilias: req.subFamilia,
                    tipoCatalogo: tipo,
                    grupos: req.grupo,
                    skus: null,
                    campanias: req.campanias
                );

                if (!productos.Any())
                    return NotFound("No se encontraron productos con esos criterios.");

                var fileBytes = await _excelGenerator.GenerarExcelConFotosAsync(productos);
                string fileName = $"catalogo_{DateTime.Now:yyyyMMddHHmm}.xlsx";

                return File(
                    fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR en GenerarCatalogo");
                return StatusCode(500, $"Error interno al generar el catálogo: {ex.Message}");
            }
        }

        // ============== POST: generar desde Excel con SKUs ==============
        [HttpPost("generar-desde-excel")]
        [RequestSizeLimit(100_000_000)] // 100 MB
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GenerarCatalogoDesdeExcel(
            [Required] IFormFile archivo,
            [FromForm] string[]? marca,
            [FromForm] string[]? familia,
            [FromForm] string[]? subFamilia,
            [FromForm] string[]? grupo,
            [FromForm] int[]? campanias,
            [FromForm] string? tipo = "general",
            [FromForm] bool? esPrime = null,
            [FromForm] int? stockMinimo = null)
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                    return BadRequest("Debes adjuntar un archivo .xlsx con los SKUs.");

                // lee SKUs desde el Excel (columna encabezado SKU o ItemCode; si no, toma col 1)
                List<string> skus = new();
                using (var ms = new MemoryStream())
                {
                    await archivo.CopyToAsync(ms);
                    ms.Position = 0;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var pkg = new ExcelPackage(ms))
                    {
                        var ws = pkg.Workbook.Worksheets.FirstOrDefault();
                        if (ws == null) return BadRequest("El Excel no tiene hojas.");

                        int colSku = -1;
                        int lastCol = ws.Dimension?.End.Column ?? 1;
                        for (int c = 1; c <= lastCol; c++)
                        {
                            var header = (ws.Cells[1, c].Text ?? "").Trim();
                            if (header.Equals("SKU", StringComparison.OrdinalIgnoreCase) ||
                                header.Equals("ItemCode", StringComparison.OrdinalIgnoreCase))
                            {
                                colSku = c;
                                break;
                            }
                        }
                        if (colSku == -1) colSku = 1;

                        int lastRow = ws.Dimension?.End.Row ?? 1;
                        for (int r = 2; r <= lastRow; r++)
                        {
                            var sku = (ws.Cells[r, colSku].Text ?? "").Trim();
                            if (!string.IsNullOrWhiteSpace(sku))
                                skus.Add(sku);
                        }
                    }
                }

                skus = skus.Select(s => s.Trim())
                           .Where(s => !string.IsNullOrWhiteSpace(s))
                           .Distinct(StringComparer.OrdinalIgnoreCase)
                           .ToList();

                if (skus.Count == 0)
                    return BadRequest("No se encontraron SKUs válidos en el Excel.");

                _logger.LogInformation("GenerarCatalogoDesdeExcel: {Count} SKUs leídos.", skus.Count);

                // si viene esPrime true, forzamos tipo prime
                var tipoEf = (esPrime ?? false) ? "prime" : (tipo ?? "general");

                var productos = await _productoService.GetProductosAsync(
                    marcas: marca,
                    stockMinimo: stockMinimo,
                    familias: familia,
                    subFamilias: subFamilia,
                    tipoCatalogo: tipoEf,
                    grupos: grupo,
                    skus: skus.ToArray(),
                    campanias: campanias
                );

                if (!productos.Any())
                    return NotFound("No se encontraron productos para los SKUs enviados (revisa códigos y filtros).");

                var fileBytes = await _excelGenerator.GenerarExcelConFotosAsync(productos);
                string fileName = $"catalogo_{DateTime.Now:yyyyMMddHHmm}_SKUs.xlsx";
                return File(
                    fileBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR en GenerarCatalogoDesdeExcel");
                return StatusCode(500, $"Error interno al generar el catálogo: {ex.Message}");
            }
        }
    }

    // ===== DTO del request =====
    public class FiltrosRequest
    {
        public string[]? marca { get; set; }
        public string[]? familia { get; set; }
        public string[]? subFamilia { get; set; }
        public string[]? grupo { get; set; }

        // si esPrime == true, el controller fuerza tipo="prime"
        public bool? esPrime { get; set; }

        public string? tipo { get; set; } = "general";
        public int? stockMinimo { get; set; }

        // IDs de propiedades OITG seleccionadas
        public int[]? campanias { get; set; }
    }
}
