using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DESCARGAR_CATALOGO.Models;

namespace DESCARGAR_CATALOGO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiltrosController : ControllerBase
    {
        private readonly TuDbContext _db;
        public FiltrosController(TuDbContext db) => _db = db;

        // GET: /api/Filtros/ping  (sin DB)
        [HttpGet("ping")]
        public IActionResult Ping() => Ok("OK");

        // GET: /api/Filtros/marcas
        [HttpGet("marcas")]
        public async Task<ActionResult<IEnumerable<string>>> GetMarcas()
        {
            var q = await _db.Oitms
                .Where(o => o.ValidFor == "Y" && o.UMarca != null && o.UMarca != "")
                .Select(o => o.UMarca!)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();
            return Ok(q);
        }

        // GET: /api/Filtros/familias
        [HttpGet("familias")]
        public async Task<ActionResult<IEnumerable<string>>> GetFamilias()
        {
            var q = await _db.Oitms
                .Where(o => o.ValidFor == "Y" && o.UFamilia != null && o.UFamilia != "")
                .Select(o => o.UFamilia!)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();
            return Ok(q);
        }

        // GET: /api/Filtros/subfamilias
        [HttpGet("subfamilias")]
        public async Task<ActionResult<IEnumerable<string>>> GetSubFamilias()
        {
            var q = await _db.Oitms
                .Where(o => o.ValidFor == "Y" && o.USubFamilia != null && o.USubFamilia != "")
                .Select(o => o.USubFamilia!)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();
            return Ok(q);
        }

        // GET: /api/Filtros/grupos
        [HttpGet("grupos")]
        public async Task<ActionResult<IEnumerable<string>>> GetGrupos()
        {
            var q = await _db.Oitbs
                .Where(g => g.ItmsGrpNam != null && g.ItmsGrpNam != "")
                .Select(g => g.ItmsGrpNam!)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();
            return Ok(q);
        }

        // GET: /api/Filtros/campanias (lista fija)
        [HttpGet("campanias")]
        public ActionResult<IEnumerable<object>> GetCampanias()
        {
            var lista = new[]
            {
                new { id = 38, nombre = "DIA DEL LIBRO" },
                new { id = 16, nombre = "DIA_MAMA" },
                new { id = 17, nombre = "DIA_NIÑO_" },
                new { id = 18, nombre = "DIA_PAPA" },
                new { id = 21, nombre = "F.PATRIAS" },
                new { id = 22, nombre = "FIN_DE_AÑO" },
                new { id = 25, nombre = "GRADUACION" },
                new { id = 26, nombre = "HALLOWEEN" },
                new { id = 31, nombre = "NAVIDAD" },
                new { id = 33, nombre = "PASCUA_RESUREECCION" },
            };
            return Ok(lista);
        }
    }
}
