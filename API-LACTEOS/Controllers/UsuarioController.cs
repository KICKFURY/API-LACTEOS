using API_LACTEOS.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace API_LACTEOS.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public UsuarioController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Usuario> lista = new();

            try
            {
                lista = _dbcontext.Usuarios.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpGet]
        [Route("Obtener/{username}")]
        public IActionResult Obtener(string username)
        {
            Usuario oUsuario = new Usuario();

            try
            {
                oUsuario = _dbcontext.Usuarios.Where(p => p.NombreUsuario == username).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oUsuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oUsuario });
            }
        }

        [HttpPost]
        [Route("Guardar/{correo}&&{nombreUsuario}&&{contra}&&{idRole:int}")]
        public IActionResult Guardar(string correo, string nombreUsuario, string contra, int idRole)
        {
            Usuario usuario = new Usuario();
            try
            {
                usuario.Correo = correo;
                usuario.NombreUsuario = nombreUsuario;
                usuario.Contra = contra;
                usuario.IdRole = idRole;
                usuario.FechaCreacion = DateTime.Now;
                _dbcontext.Usuarios.Add(usuario);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{correo}&&{nombreUsuario}&&{contra}&&{idRole:int}&&{idEstado:int}")]
        public IActionResult Editar(string correo, string nombreUsuario, string contra, int idRole, int idEstado)
        {
            Usuario oUsuario = new Usuario();

            try
            {
                oUsuario = _dbcontext.Usuarios.Where(p => p.NombreUsuario == nombreUsuario).FirstOrDefault();
                oUsuario.Correo = correo;
                oUsuario.NombreUsuario = nombreUsuario;
                oUsuario.Contra = contra;
                oUsuario.IdRole = idRole;
                oUsuario.IdEstado = idEstado;
                _dbcontext.Usuarios.Update(oUsuario);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{username}")]
        public IActionResult Eliminar(string username)
        {
            Usuario oUsuario = new Usuario();

            try
            {
                oUsuario = _dbcontext.Usuarios.Where(p => p.NombreUsuario == username).FirstOrDefault();
                _dbcontext.Usuarios.Remove(oUsuario);
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