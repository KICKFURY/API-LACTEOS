using API_LACTEOS.Models;
using API_LACTEOS.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public PagoController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {

            List<Pago> lista = new();

            try
            {
                lista = _dbcontext.Pagos.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpGet]
        [Route("Obtener/{idVenta:int}")]
        public IActionResult Obtener(int idVenta)
        {
            Pago pago = new Pago();

            try
            {
                pago = _dbcontext.Pagos.Where(p => p.IdVenta == idVenta).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = pago });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = pago });
            }
        }

        [HttpPost]
        [Route("Guardar/{saldoPendiente:int}&{plazo:int}&{fechaPago}")]
        public IActionResult Guardar(int saldoPendiente, int plazo, DateTime fechaPago)
        {
            Pago pago = new Pago();
            try
            {
                pago.IdVenta = _dbcontext.Ventas.OrderBy(p => p.Id).Last().Id;
                pago.TotalPago = saldoPendiente;
                pago.Plazo = plazo;
                pago.FechaPago = fechaPago;
                _dbcontext.Pagos.Add(pago);
                _dbcontext.SaveChanges(); 

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{idVenta}&{saldoPendiente:decimal}&{fechaPago:datetime}")]
        public IActionResult Editar(int idVenta, decimal saldoPendiente, DateTime fechaPago)
        {
            Pago pago = new Pago();

            try
            {
                pago = _dbcontext.Pagos.Where(p => p.IdVenta == idVenta).FirstOrDefault();
                pago.TotalPago = saldoPendiente;
                pago.FechaPago = fechaPago;
                _dbcontext.Pagos.Update(pago);
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
            Pago oPago = _dbcontext.Pagos.Find(Id);

            if (oPago == null)
            {
                return BadRequest("Pago no encontrado");
            }

            try
            {
                _dbcontext.Pagos.Remove(oPago);
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
