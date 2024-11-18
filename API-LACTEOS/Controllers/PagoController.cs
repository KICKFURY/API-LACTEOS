using API_LACTEOS.Models;
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
        [Route("Obtener/{idPago:int}")]
        public IActionResult Obtener(int idPago)
        {
            Pago oPago = _dbcontext.Pagos.Find(idPago);

            if (oPago == null)
            {
                return BadRequest("Pago no encontrado");
            }

            try
            {
                oPago = _dbcontext.Pagos.Where(p => p.Id == idPago).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oPago });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oPago });
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
        [Route("Editar")]
        public IActionResult Editar([FromBody] Pago objeto)
        {
            Pago oPago = _dbcontext.Pagos.Find(objeto.Id);

            if (oPago == null)
            {
                return BadRequest("Pago no encontrado");
            }
            try
            {
                _dbcontext.Pagos.Update(oPago);
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
