using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LACTEOS.Models;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesCompraController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public DetallesCompraController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {

            List<DetallesCompra> lista = new();

            try
            {
                lista = _dbcontext.DetallesCompras.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpGet]
        [Route("Obtener/{idDetallesCompra:int}")]
        public IActionResult Obtener(int idDetallesCompra)
        {
            DetallesCompra oDetallesCompra = _dbcontext.DetallesCompras.Find(idDetallesCompra);

            if (oDetallesCompra == null)
            {
                return BadRequest("Detalles de Compra no encontrada");
            }

            try
            {
                oDetallesCompra = _dbcontext.DetallesCompras.Where(p => p.Id == idDetallesCompra).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oDetallesCompra });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oDetallesCompra });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] DetallesCompra objeto)
        {
            try
            {
                _dbcontext.DetallesCompras.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] DetallesCompra objeto)
        {
            DetallesCompra oDetallesCompra = _dbcontext.DetallesCompras.Find(objeto.Id);

            if (oDetallesCompra == null)
            {
                return BadRequest("Compra no encontrada");
            }

            try
            {

                //oDetallesCompra.NumeroDetallesCompra = objeto.NumeroDetallesCompra;
                //oDetallesCompra.Cantidad = objeto.Cantidad;
                //oDetallesCompra.Costo = objeto.Costo;
                //oDetallesCompra.Subtotal = objeto.Subtotal;
                oDetallesCompra.IdCompraNavigation = objeto.IdCompraNavigation ?? oDetallesCompra.IdCompraNavigation;
                oDetallesCompra.IdProductoNavigation = objeto.IdProductoNavigation ?? oDetallesCompra.IdProductoNavigation;

                _dbcontext.DetallesCompras.Update(oDetallesCompra);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }



        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public IActionResult Eliminar(int Id)
        {
            DetallesCompra oDetallesCompra = _dbcontext.DetallesCompras.Find(Id);

            if (oDetallesCompra == null)
            {
                return BadRequest("Detalles Compra no encontrada");
            }

            try
            {
                _dbcontext.DetallesCompras.Remove(oDetallesCompra);
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
