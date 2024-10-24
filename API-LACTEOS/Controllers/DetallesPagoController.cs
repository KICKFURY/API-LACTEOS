//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using API_LACTEOS.Models;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DetallesPagoController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public DetallesPagoController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<DetallesPago> lista = new();

//            try
//            {
//                lista = _dbcontext.DetallesPagos.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idDetallesPago:int}")]
//        public IActionResult Obtener(int idDetallesPago)
//        {
//            DetallesPago oDetallesPago = _dbcontext.DetallesPagos.Find(idDetallesPago);

//            if (oDetallesPago == null)
//            {
//                return BadRequest("Detalles de Pago no encontrado");
//            }

//            try
//            {
//                oDetallesPago = _dbcontext.DetallesPagos.Where(p => p.Id == idDetallesPago).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oDetallesPago });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oDetallesPago });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] DetallesPago objeto)
//        {
//            try
//            {
//                _dbcontext.DetallesPagos.Add(objeto);
//                _dbcontext.SaveChanges();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
//            }
//        }

//        [HttpPut]
//        [Route("Editar")]
//        public IActionResult Editar([FromBody] DetallesPago objeto)
//        {
//            DetallesPago oDetallesPago = _dbcontext.DetallesPagos.Find(objeto.Id);

//            if (oDetallesPago == null)
//            {
//                return BadRequest("Detalles de pago no encontrado");
//            }

//            try
//            {

//                oDetallesPago.NumeroDetallesPago = objeto.NumeroDetallesPago;
//                oDetallesPago.FechaDetallesPago = objeto.FechaDetallesPago != default ? objeto.FechaDetallesPago : DateTime.MinValue;
//                oDetallesPago.CantidadDetallesPago = objeto.CantidadDetallesPago;
//                oDetallesPago.SaldoRestante = objeto.SaldoRestante;
//                oDetallesPago.DiaPago = objeto.DiaPago != default ? objeto.DiaPago : DateTime.MinValue;
//                oDetallesPago.FechaActualizacionDetallesPago = objeto.FechaActualizacionDetallesPago != default ? objeto.FechaActualizacionDetallesPago : DateTime.MinValue;


//                _dbcontext.DetallesPagos.Update(oDetallesPago);
//                _dbcontext.SaveChanges();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });


//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
//            }



//        }

//        [HttpDelete]
//        [Route("Eliminar/{id:int}")]
//        public IActionResult Eliminar(int Id)
//        {
//            DetallesPago oDetallesPago = _dbcontext.DetallesPagos.Find(Id);

//            if (oDetallesPago == null)
//            {
//                return BadRequest("Detalles de Pago no encontrada");
//            }

//            try
//            {
//                _dbcontext.DetallesPagos.Remove(oDetallesPago);
//                _dbcontext.SaveChanges();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
//            }
//        }
//    }
//}
