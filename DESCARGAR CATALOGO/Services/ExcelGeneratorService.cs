using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DESCARGAR_CATALOGO.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace DESCARGAR_CATALOGO.Services
{
    public class ExcelGeneratorService : IExcelGeneratorService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ExcelGeneratorService> _logger;

        private readonly string? _rutaBaseFotos;
        private readonly string? _rutaLogo;
        private readonly bool _desactivarImagenes;

        // ====== Ajustes visuales ======
        private const int COL_FOTO = 2;             // Columna B
        private const double ANCHO_COL_FOTO = 18.0; // ancho columna B
        private const double ALTO_FILA_PT = 110.0;  // alto de fila en puntos

        public ExcelGeneratorService(IConfiguration config, ILogger<ExcelGeneratorService> logger)
        {
            _config = config;
            _logger = logger;

            _rutaBaseFotos = _config.GetValue<string>("RutaBaseFotos");
            _rutaLogo = _config.GetValue<string>("RutaLogo");
            _desactivarImagenes = _config.GetValue<bool>("DesactivarImagenes", false);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<byte[]> GenerarExcelConFotosAsync(IEnumerable<ProductoDto> productos)
        {
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Catálogo");

            // ========= ENCABEZADO, LOGO Y TÍTULOS =========
            if (!_desactivarImagenes && !string.IsNullOrWhiteSpace(_rutaLogo) && File.Exists(_rutaLogo!))
            {
                try
                {
                    var logoBytes = await File.ReadAllBytesAsync(_rutaLogo!);
                    var logo = ws.Drawings.AddPicture("LogoEmpresa", new MemoryStream(logoBytes));
                    logo.SetPosition(0, 5, 0, 5);
                    logo.SetSize(180, 60);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "No se pudo cargar el logo desde {RutaLogo}", _rutaLogo);
                }
            }

            ws.Cells["A1:C3"].Merge = true;
            ws.Row(1).Height = 25;
            ws.Row(2).Height = 25;

            int headerRow = 4;
            ws.Cells[headerRow, 1].Value = "Código";
            ws.Cells[headerRow, 2].Value = "Foto";
            ws.Cells[headerRow, 3].Value = "Descripción";
            ws.Cells[headerRow, 4].Value = "Marca";
            ws.Cells[headerRow, 5].Value = "Familia";
            ws.Cells[headerRow, 6].Value = "Caja";
            ws.Cells[headerRow, 7].Value = "Inner";
            ws.Cells[headerRow, 8].Value = "Precio Neto";
            ws.Cells[headerRow, 9].Value = "Precio C/IVA";
            ws.Cells[headerRow, 10].Value = "Cantidad";
            ws.Cells[headerRow, 11].Value = "Total";
            ws.Cells[headerRow, 12].Value = "Precio Caja";

            using (var hdr = ws.Cells[headerRow, 1, headerRow, 12])
            {
                hdr.Style.Font.Bold = true;
                hdr.Style.Fill.PatternType = ExcelFillStyle.Solid;
                hdr.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                hdr.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
            ws.Cells[headerRow, 1, headerRow, 12].AutoFilter = true;

            // Anchos columna y formato moneda
            ws.Column(COL_FOTO).Width = ANCHO_COL_FOTO;

            ws.Column(1).Width = 12;
            ws.Column(3).Width = 35;
            ws.Column(4).Width = 20;
            ws.Column(5).Width = 20;
            ws.Column(6).Width = 8;
            ws.Column(7).Width = 8;
            ws.Column(8).Width = 12;
            ws.Column(9).Width = 12;
            ws.Column(10).Width = 10;
            ws.Column(11).Width = 12;
            ws.Column(12).Width = 12;

            ws.Column(1).Style.Numberformat.Format = "0";
            ws.Column(8).Style.Numberformat.Format = "$ #,##0";
            ws.Column(9).Style.Numberformat.Format = "$ #,##0";
            ws.Column(11).Style.Numberformat.Format = "$ #,##0";
            ws.Column(12).Style.Numberformat.Format = "$ #,##0";

            // ========= FILAS =========
            int fila = headerRow + 1;

            foreach (var p in productos ?? Enumerable.Empty<ProductoDto>())
            {
                // Alto de fila consistente para que la foto encaje
                ws.Row(fila).Height = ALTO_FILA_PT;

                // Código
                if (long.TryParse(p.ItemCode, out long skuNum))
                    ws.Cells[fila, 1].Value = skuNum;
                else
                    ws.Cells[fila, 1].Value = p.ItemCode;

                // Descripción / datos
                ws.Cells[fila, 3].Value = p.ItemName;
                ws.Cells[fila, 4].Value = p.Marca;
                ws.Cells[fila, 5].Value = p.UFamilia;
                ws.Cells[fila, 6].Value = p.Caja;
                ws.Cells[fila, 7].Value = p.M;
                ws.Cells[fila, 8].Value = p.PrecioCliente;
                ws.Cells[fila, 9].Formula = $"H{fila}*1.19";
                ws.Cells[fila, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[fila, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Orange);
                ws.Cells[fila, 12].Formula = $"H{fila}*0.95";
                ws.Cells[fila, 11].Formula = $"IF(AND(J{fila}>=F{fila},L{fila}>0),J{fila}*L{fila},J{fila}*H{fila})";

                // FOTO
                if (!_desactivarImagenes && !string.IsNullOrWhiteSpace(_rutaBaseFotos))
                {
                    var rutaImg = BuscarFotoPorCodigo(_rutaBaseFotos!, p.ItemCode);
                    if (rutaImg is not null)
                    {
                        try
                        {
                            await InsertarImagenEnCeldaAsync(ws, rutaImg, fila, COL_FOTO, p.ItemCode);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Error insertando imagen {ItemCode} desde {Ruta}", p.ItemCode, rutaImg);
                        }
                    }
                    else
                    {
                        _logger.LogDebug("Sin imagen para ItemCode={ItemCode}", p.ItemCode);
                    }
                }

                fila++;
            }

            ws.View.FreezePanes(headerRow + 1, 1);
            return await package.GetAsByteArrayAsync();
        }

        // ================== Helpers ==================

        /// <summary>
        /// Inserta imagen en celda con proporción correcta y anclada para filtros
        /// </summary>
        private async Task InsertarImagenEnCeldaAsync(ExcelWorksheet ws, string rutaImagen, int fila, int columna, string? itemCode)
        {
            // Leer imagen como FileStream para EPPlus
            using var fileStream = new FileStream(rutaImagen, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Crear memoria stream para obtener dimensiones
            var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            // Obtener dimensiones de imagen usando EPPlus interno
            int imgWidth = 0;
            int imgHeight = 0;

            try
            {
                using (var tempImage = System.Drawing.Image.FromStream(memoryStream, false, false))
                {
                    imgWidth = tempImage.Width;
                    imgHeight = tempImage.Height;
                }
            }
            catch
            {
                // Valores por defecto si falla la lectura
                imgWidth = 200;
                imgHeight = 200;
            }

            // Reiniciar stream para EPPlus
            memoryStream.Position = 0;

            // Crear nombre único para la imagen
            var picName = $"img_{(itemCode ?? "SKU")}_{fila}";
            var pic = ws.Drawings.AddPicture(picName, memoryStream);

            // Calcular dimensiones de la celda en píxeles
            double cellWidthChars = ws.Column(columna).Width;
            double cellHeightPts = ws.Row(fila).Height;

            int cellWidthPx = (int)Math.Round(cellWidthChars * 7.0);
            int cellHeightPx = (int)Math.Round(cellHeightPts * 96.0 / 72.0);

            // Calcular escala manteniendo proporción (con padding)
            int padding = 8;
            int maxWidth = cellWidthPx - (padding * 2);
            int maxHeight = cellHeightPx - (padding * 2);

            double scaleWidth = (double)maxWidth / imgWidth;
            double scaleHeight = (double)maxHeight / imgHeight;
            double scale = Math.Min(scaleWidth, scaleHeight);
            scale = Math.Min(scale, 1.0); // No agrandar imagen

            int finalWidth = (int)Math.Round(imgWidth * scale);
            int finalHeight = (int)Math.Round(imgHeight * scale);

            // CRÍTICO: Configurar anclaje ANTES de posición
            pic.EditAs = eEditAs.OneCell; // Se mueve y oculta con la celda al filtrar

            // Posicionar imagen (centrada en la celda)
            int offsetX = (cellWidthPx - finalWidth) / 2;
            int offsetY = (cellHeightPx - finalHeight) / 2;

            pic.SetPosition(fila - 1, Math.Max(0, offsetY), columna - 1, Math.Max(0, offsetX));
            pic.SetSize(finalWidth, finalHeight);
        }

        /// <summary>
        /// Busca foto por SKU. Acepta formatos f{000000}.jpg/jpeg, F{000000}.jpg/jpeg o {ItemCode}.jpg/jpeg.
        /// </summary>
        private static string? BuscarFotoPorCodigo(string basePath, string? itemCode)
        {
            if (string.IsNullOrWhiteSpace(basePath) || string.IsNullOrWhiteSpace(itemCode))
                return null;

            var digits = new string(itemCode.Where(char.IsDigit).ToArray());
            var list = new List<string>();

            if (!string.IsNullOrEmpty(digits))
            {
                var seis = digits.PadLeft(6, '0');
                list.Add(Path.Combine(basePath, $"f{seis}.jpg"));
                list.Add(Path.Combine(basePath, $"f{seis}.jpeg"));
                list.Add(Path.Combine(basePath, $"F{seis}.jpg"));
                list.Add(Path.Combine(basePath, $"F{seis}.jpeg"));
            }

            list.Add(Path.Combine(basePath, $"f{itemCode}.jpg"));
            list.Add(Path.Combine(basePath, $"f{itemCode}.jpeg"));
            list.Add(Path.Combine(basePath, $"{itemCode}.jpg"));
            list.Add(Path.Combine(basePath, $"{itemCode}.jpeg"));

            foreach (var p in list)
                if (File.Exists(p)) return p;

            return null;
        }
    }
}