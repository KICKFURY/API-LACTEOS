using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LACTEOS.Models;
using System.Runtime.CompilerServices;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public ClientesController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {

            List<Cliente> lista = new();

            try
            {
                lista = _dbcontext.Clientes.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public IActionResult ObtenerClienteById(int id)
        {
            Cliente cliente = _dbcontext.Clientes.Find(id);

            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { message = "", response = cliente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = cliente });
            }
        }

        [HttpGet]
        [Route("Obtener/{ruc}")]
        public IActionResult Obtener(string ruc)
        {
            Cliente oCliente = new Cliente();
            try
            {
                oCliente = _dbcontext.Clientes.Where(p => p.Ruc == ruc).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oCliente });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oCliente });
            }
        }

        [HttpPost]
        [Route("Guardar/{nombre}&{apellido}&{ruc}")]
        public IActionResult Guardar(string nombre, string apellido, string ruc, string direccion = "", string telefono = "")
        {
            Cliente cliente = new Cliente();
            try
            {
                cliente.NombreCliente = nombre;
                cliente.ApellidoCliente = apellido;
                cliente.Ruc = ruc;
                cliente.Direccion = direccion;
                cliente.Telefono = telefono;
                cliente.FechaCreacion = DateTime.Now;
                _dbcontext.Clientes.Add(cliente);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{nombre}&{apellido}&{ruc}")]
        public IActionResult Editar(string nombre, string apellido, string ruc, string direccion = "", string telefono = "")
        {
            Cliente Ocliente = new Cliente();
            try
            {
                Ocliente = _dbcontext.Clientes.Where(p => p.Ruc == ruc).FirstOrDefault();
                Ocliente.NombreCliente = nombre;
                Ocliente.ApellidoCliente = apellido;
                Ocliente.Ruc = ruc;
                Ocliente.Direccion = direccion;
                Ocliente.Telefono = telefono;
                _dbcontext.Clientes.Update(Ocliente);
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
        public IActionResult Eliminar(string ruc)
        {
            Cliente Ocliente = new Cliente();
            try
            {
                Ocliente = _dbcontext.Clientes.Where(p => p.Ruc == ruc).FirstOrDefault();
                _dbcontext.Clientes.Remove(Ocliente);
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
