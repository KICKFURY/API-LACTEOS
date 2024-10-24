//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProgresoController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public ProgresoController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<Progreso> lista = new();

//            try
//            {
//                lista = _dbcontext.Progresos.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idProgreso:int}")]
//        public IActionResult Obtener(int idProgreso)
//        {
//            Progreso oProgreso = _dbcontext.Progresos.Find(idProgreso);

//            if (oProgreso == null)
//            {
//                return BadRequest("Progreso no encontrado");
//            }

//            try
//            {
//                oProgreso = _dbcontext.Progresos.Where(p => p.Id == idProgreso).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProgreso });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oProgreso });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] Progreso objeto)
//        {
//            try
//            {
//                _dbcontext.Progresos.Add(objeto);
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
//        public IActionResult Editar([FromBody] Progreso objeto)
//        {
//            Progreso oProgreso = _dbcontext.Progresos.Find(objeto.Id);

//            if (oProgreso == null)
//            {
//                return BadRequest("Progreso no encontrado");
//            }
//            try
//            {
//                oProgreso.NombreProgreso = objeto.NombreProgreso is null ? oProgreso.NombreProgreso : objeto.NombreProgreso;

//                _dbcontext.Progresos.Update(oProgreso);
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
//            Progreso oProgreso = _dbcontext.Progresos.Find(Id);

//            if (oProgreso == null)
//            {
//                return BadRequest("Progreso no encontrado");
//            }

//            try
//            {
//                _dbcontext.Progresos.Remove(oProgreso);
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
