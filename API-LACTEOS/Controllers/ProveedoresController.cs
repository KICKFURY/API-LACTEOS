using API_LACTEOS.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API_LACTEOS.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public ProveedoresController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Proveedore> lista = new();
            try
            {
                lista = _dbcontext.Proveedores.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{ruc}")]
        public IActionResult Obtener(string ruc)
        {
            Proveedore oProveedore = new Proveedore();
            try
            {
                oProveedore = _dbcontext.Proveedores.Where(p => p.RucProveedor == ruc).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProveedore });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oProveedore });
            }
        }

        [HttpPost]
        [Route("Guardar/{nombre}&{telefono}&{ruc}")]
        public IActionResult ValidarLogin(string nombre, string telefono, string ruc)
        {
            Proveedore proveedore = new Proveedore();
            try
            {
                proveedore.NombreProveedor = nombre;
                proveedore.TelefonoProveedor = telefono;
                proveedore.RucProveedor = ruc;
                proveedore.FechaCreacion = DateTime.Now;
                _dbcontext.Proveedores.Add(proveedore);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{nombre}&{ruc}&{telefono}&{idEstado:int}")]
        public IActionResult Editar(string nombre, string ruc, string telefono, int idEstado)
        {
            Proveedore oProveedore = new Proveedore();
            try
            {
                oProveedore = _dbcontext.Proveedores.Where(p => p.RucProveedor == ruc).FirstOrDefault();
                oProveedore.NombreProveedor = nombre;
                oProveedore.RucProveedor = ruc;
                oProveedore.TelefonoProveedor = telefono;
                oProveedore.IdEstado = idEstado;
                _dbcontext.Proveedores.Update(oProveedore);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{ruc}")]
        public IActionResult Eliminar1(string ruc)
        {
            Proveedore oProveedore = new Proveedore();
            try
            {
                oProveedore = _dbcontext.Proveedores.Where(p => p.RucProveedor == ruc).FirstOrDefault();
                _dbcontext.Proveedores.Remove(oProveedore);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
