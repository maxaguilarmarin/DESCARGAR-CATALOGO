using System.Collections.Generic;
using System.Threading.Tasks;
using DESCARGAR_CATALOGO.Models;

namespace DESCARGAR_CATALOGO.Services
{
    public interface IExcelGeneratorService
    {
        Task<byte[]> GenerarExcelConFotosAsync(IEnumerable<ProductoDto> productos);
    }
}
