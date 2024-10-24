//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DevolucionesController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public DevolucionesController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<Devolucione> lista = new();

//            try
//            {
//                lista = _dbcontext.Devoluciones.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idDevoluciones:int}")]
//        public IActionResult Obtener(int idDevoluciones)
//        {
//            Devolucione oDevoluciones = _dbcontext.Devoluciones.Find(idDevoluciones);

//            if (oDevoluciones == null)
//            {
//                return BadRequest("Devolucion no encontrada");
//            }

//            try
//            {
//                oDevoluciones = _dbcontext.Devoluciones.Where(p => p.Id == idDevoluciones).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oDevoluciones });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oDevoluciones });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] Devolucione objeto)
//        {
//            try
//            {
//                _dbcontext.Devoluciones.Add(objeto);
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
//        public IActionResult Editar([FromBody] Devolucione objeto)
//        {
//            Devolucione oDevoluciones = _dbcontext.Devoluciones.Find(objeto.Id);

//            if (oDevoluciones == null)
//            {
//                return BadRequest("Devolucion no encontrada");
//            }

//            try
//            {
//                oDevoluciones.FechaDevolucion = objeto.FechaDevolucion != default ? objeto.FechaDevolucion : DateTime.MinValue;
//                oDevoluciones.Total = objeto.Total;
                

//                _dbcontext.Devoluciones.Update(oDevoluciones);
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
//            Devolucione oDevoluciones = _dbcontext.Devoluciones.Find(Id);

//            if (oDevoluciones == null)
//            {
//                return BadRequest("Devolucion no encontrada");
//            }

//            try
//            {
//                _dbcontext.Devoluciones.Remove(oDevoluciones);
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
