using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LACTEOS.Models;
using API_LACTEOS.Servicios;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        public readonly Database _dbcontext;

        public MantenimientoController(Database _context)
        {
            _dbcontext = _context;
        }

        [HttpPost]
        [Route("Backup/{path}")]
        public IActionResult CrearBackup(string path)
        {
            try
            {
                string pathfile = @"C:\backup\" + path;
                _dbcontext.CrearBackup(pathfile);

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Restore/{path}")]
        public IActionResult RestaurarBackup(string path)
        {
            try
            {
                string pathfile = @"C:\backup\" + path;
                _dbcontext.RestaurarBackup(pathfile);

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
            }
        }
    }
}
