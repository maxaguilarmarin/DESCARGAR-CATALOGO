using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;

using DESCARGAR_CATALOGO.Models;

namespace DESCARGAR_CATALOGO.Services
{
    public class ExcelGeneratorService : IExcelGeneratorService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ExcelGeneratorService> _logger;

        // AppSettings
        private readonly string? _rutaBaseFotos;
        private readonly string? _rutaLogo;
        private readonly bool _desactivarImagenes;

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
            _logger.LogInformation(
                "GenerarExcelConFotosAsync -> RutaBaseFotos='{RutaBaseFotos}', RutaLogo='{RutaLogo}', DesactivarImagenes={Flag}",
                _rutaBaseFotos, _rutaLogo, _desactivarImagenes
            );

            bool tieneRutaFotos = !_desactivarImagenes && !string.IsNullOrWhiteSpace(_rutaBaseFotos);
            bool tieneRutaLogo = !_desactivarImagenes && !string.IsNullOrWhiteSpace(_rutaLogo);

            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Catálogo");

            // ========== LOGO ==========
            if (tieneRutaLogo && File.Exists(_rutaLogo!))
            {
                try
                {
                    using var fsLogo = File.OpenRead(_rutaLogo!);
                    var picLogo = ws.Drawings.AddPicture("LogoEmpresa", fsLogo);
                    // El logo no necesita anclaje TwoCell; lo ponemos arriba a la izquierda
                    picLogo.SetPosition(0, 5, 0, 5); // fila 1, col A (ajústalo a gusto)
                    picLogo.SetSize(180, 60);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "No se pudo insertar el logo desde '{LogoPath}'", _rutaLogo);
                }
            }

            // ========== CABECERA Y TOTALES ==========
            ws.Cells["A1:C3"].Merge = true;
            ws.Row(1).Height = 25;
            ws.Row(2).Height = 25;

            ws.Cells["L1"].Value = "Total neto";
            ws.Cells["L2"].Value = "IVA";
            ws.Cells["L3"].Value = "Total";
            ws.Cells["L1:L3"].Style.Font.Bold = true;
            ws.Cells["M1:M3"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            ws.Cells["M1:M3"].Style.Numberformat.Format = "$ #,##0";
            ws.Cells["M1"].Formula = "SUBTOTAL(9,K:K)";
            ws.Cells["M2"].Formula = "M1*0.19";
            ws.Cells["M3"].Formula = "M1+M2";

            // ========== ENCABEZADOS ==========
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
                hdr.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                hdr.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
            ws.Cells[headerRow, 1, headerRow, 12].AutoFilter = true;

            // ========== Helper para encontrar la foto ==========
            string? BuscarFotoPorCodigo(string? itemCode)
            {
                if (!tieneRutaFotos || string.IsNullOrWhiteSpace(itemCode)) return null;

                var soloDigitos = new string(itemCode.Where(char.IsDigit).ToArray());

                var candidatos = new List<string>();
                if (!string.IsNullOrEmpty(soloDigitos))
                {
                    var seis = soloDigitos.PadLeft(6, '0'); // 3150 -> 003150
                    candidatos.Add(Path.Combine(_rutaBaseFotos!, $"f{seis}.jpg"));
                    candidatos.Add(Path.Combine(_rutaBaseFotos!, $"f{seis}.jpeg"));
                    candidatos.Add(Path.Combine(_rutaBaseFotos!, $"F{seis}.jpg"));
                    candidatos.Add(Path.Combine(_rutaBaseFotos!, $"F{seis}.jpeg"));
                }

                candidatos.Add(Path.Combine(_rutaBaseFotos!, $"f{itemCode}.jpg"));
                candidatos.Add(Path.Combine(_rutaBaseFotos!, $"{itemCode}.jpg"));
                candidatos.Add(Path.Combine(_rutaBaseFotos!, $"f{itemCode}.jpeg"));
                candidatos.Add(Path.Combine(_rutaBaseFotos!, $"{itemCode}.jpeg"));

                foreach (var ruta in candidatos)
                    if (File.Exists(ruta)) return ruta;

                return null;
            }

            // ========== FILAS ==========
            int fila = headerRow + 1; // empieza en 5
            foreach (var prod in productos ?? Enumerable.Empty<ProductoDto>())
            {
                // Altura de la fila pensada para la imagen
                ws.Row(fila).Height = 100;

                // Código (numérico si es posible)
                if (long.TryParse(prod.ItemCode, out long skuNumber))
                    ws.Cells[fila, 1].Value = skuNumber;
                else
                    ws.Cells[fila, 1].Value = prod.ItemCode;

                // ==== IMAGEN (anclaje TwoCell manual con From/To + offsets EMU) ====
                if (tieneRutaFotos)
                {
                    var rutaImg = !string.IsNullOrWhiteSpace(prod.ItemCode) ? BuscarFotoPorCodigo(prod.ItemCode) : null;

                    if (!string.IsNullOrWhiteSpace(rutaImg))
                    {
                        try
                        {
                            byte[] imgBytes = await File.ReadAllBytesAsync(rutaImg!);

                            // Leer dimensiones
                            int imgAncho, imgAlto;
                            using (var ms = new MemoryStream(imgBytes))
                            using (var img = System.Drawing.Image.FromStream(ms))
                            {
                                imgAncho = img.Width;
                                imgAlto = img.Height;
                            }

                            // Agregar imagen
                            var pic = ws.Drawings.AddPicture($"img_{prod.ItemCode}_{fila}", new MemoryStream(imgBytes));

                            // Tamaño máximo
                            int maxAncho = 125;
                            int maxAlto = 120;

                            // Calcular escala
                            double escala = Math.Min((double)maxAncho / imgAncho, (double)maxAlto / imgAlto);
                            escala = Math.Min(escala, 1.0);

                            int anchoFinal = (int)(imgAncho * escala);
                            int altoFinal = (int)(imgAlto * escala);

                            // Posicionar y dimensionar
                            int row0 = fila - 1;
                            int colB = 1;

                            pic.SetPosition(row0, 5, colB, 5);
                            pic.SetSize(anchoFinal, altoFinal);

                            // No llamar SetSize/SetPosition/EditAs: el área la define From/To
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error insertando imagen para {ItemCode} desde {Ruta}", prod.ItemCode, rutaImg);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(prod.ItemCode))
                            _logger.LogWarning("Imagen no encontrada para ItemCode='{ItemCode}' en base '{Base}'", prod.ItemCode, _rutaBaseFotos);
                    }
                }

                // Texto y números
                ws.Cells[fila, 3].Value = prod.ItemName; // Descripción
                ws.Cells[fila, 4].Value = prod.Marca;
                ws.Cells[fila, 5].Value = prod.UFamilia;

                ws.Cells[fila, 6].Value = prod.Caja;          // Caja
                ws.Cells[fila, 7].Value = prod.M;             // Inner
                ws.Cells[fila, 8].Value = prod.PrecioCliente; // Precio Neto

                // Precio con IVA
                ws.Cells[fila, 9].Formula = $"H{fila}*1.19";

                // Cantidad editable
                ws.Cells[fila, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[fila, 10].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                // Total (si J>=F y L>0 => J*L ; si no J*H)
                ws.Cells[fila, 11].Formula = $"IF(AND(J{fila}>=F{fila},L{fila}>0),J{fila}*L{fila},J{fila}*H{fila})";

                // Precio caja 5% desc.
                ws.Cells[fila, 12].Formula = $"H{fila}*0.95";

                // formatos
                ws.Cells[fila, 8].Style.Numberformat.Format = "$ #,##0";
                ws.Cells[fila, 9].Style.Numberformat.Format = "$ #,##0";
                ws.Cells[fila, 11].Style.Numberformat.Format = "$ #,##0";
                ws.Cells[fila, 12].Style.Numberformat.Format = "$ #,##0";

                fila++;
            }

            // ========== ANCHOS Y FORMATOS ==========
            ws.Column(1).Width = 12; // Código
            ws.Column(2).Width = 20; // Foto (ajusta 14–16)
            ws.Column(3).Width = 35; // Descripción
            ws.Column(4).Width = 20; // Marca
            ws.Column(5).Width = 20; // Familia
            ws.Column(6).Width = 8;  // Caja
            ws.Column(7).Width = 8;  // Inner
            ws.Column(8).Width = 12; // Precio Neto
            ws.Column(9).Width = 12; // Precio C/IVA
            ws.Column(10).Width = 10; // Cantidad
            ws.Column(11).Width = 12; // Total
            ws.Column(12).Width = 12; // Precio Caja

            // Código numérico sin decimales
            ws.Column(1).Style.Numberformat.Format = "0";

            // Fila de encabezado fija
            ws.View.FreezePanes(headerRow + 1, 1);

            var bytes = await package.GetAsByteArrayAsync();
            _logger.LogInformation("Excel generado: {Filas} filas de datos.", fila - (headerRow + 1));
            return bytes;
        }
    }
}
