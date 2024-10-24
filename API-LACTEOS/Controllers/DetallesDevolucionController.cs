//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DetallesDevolucionController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public DetallesDevolucionController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<DetallesDevolucion> lista = new();

//            try
//            {
//                lista = _dbcontext.DetallesDevolucions.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idDetallesDevolucion:int}")]
//        public IActionResult Obtener(int idDetallesDevolucion)
//        {
//            DetallesDevolucion oDetallesDevolucion = _dbcontext.DetallesDevolucions.Find(idDetallesDevolucion);

//            if (oDetallesDevolucion == null)
//            {
//                return BadRequest("Detalles de la Devolucion no encontrada");
//            }

//            try
//            {
//                oDetallesDevolucion = _dbcontext.DetallesDevolucions.Where(p => p.Id == idDetallesDevolucion).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oDetallesDevolucion });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oDetallesDevolucion });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] DetallesDevolucion objeto)
//        {
//            try
//            {
//                _dbcontext.DetallesDevolucions.Add(objeto);
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
//        public IActionResult Editar([FromBody] DetallesDevolucion objeto)
//        {
//            DetallesDevolucion oDetallesDevolucion = _dbcontext.DetallesDevolucions.Find(objeto.Id);

//            if (oDetallesDevolucion == null)
//            {
//                return BadRequest("Detalles de la devolucion no encontrada");
//            }

//            try
//            {

//                oDetallesDevolucion.NumeroDetalleDevolucion = objeto.NumeroDetalleDevolucion;
//                oDetallesDevolucion.Motivo = objeto.Motivo is null ? oDetallesDevolucion.Motivo : objeto.Motivo;
//                oDetallesDevolucion.Cantidad = objeto.Cantidad;
//                oDetallesDevolucion.Estado = objeto.Estado is null ? oDetallesDevolucion.Estado : objeto.Estado;

//                _dbcontext.DetallesDevolucions.Update(oDetallesDevolucion);
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
//            DetallesDevolucion oDetallesDevolucion = _dbcontext.DetallesDevolucions.Find(Id);

//            if (oDetallesDevolucion == null)
//            {
//                return BadRequest("Detalles de la devolucion no encontrada");
//            }

//            try
//            {
//                _dbcontext.DetallesDevolucions.Remove(oDetallesDevolucion);
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
