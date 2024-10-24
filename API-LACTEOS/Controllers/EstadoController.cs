//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EstadoController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public EstadoController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<Estado> lista = new();

//            try
//            {
//                lista = _dbcontext.Estados.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idEstado:int}")]
//        public IActionResult Obtener(int idEstado)
//        {
//            Estado oEstado = _dbcontext.Estados.Find(idEstado);

//            if (oEstado == null)
//            {
//                return BadRequest("Estado no encontrado");
//            }

//            try
//            {
//                oEstado = _dbcontext.Estados.Where(p => p.Id == idEstado).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oEstado });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oEstado });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] Estado objeto)
//        {
//            try
//            {
//                _dbcontext.Estados.Add(objeto);
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
//        public IActionResult Editar([FromBody] Estado objeto)
//        {
//            Estado oEstado = _dbcontext.Estados.Find(objeto.Id);

//            if (oEstado == null)
//            {
//                return BadRequest("Estado no encontrado");
//            }

//            try
//            {
//                oEstado.Nombre = objeto.Nombre is null ? oEstado.Nombre : objeto.Nombre;


//                _dbcontext.Estados.Update(oEstado);
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
//            Estado oEstado = _dbcontext.Estados.Find(Id);

//            if (oEstado == null)
//            {
//                return BadRequest("Estado no encontrado");
//            }

//            try
//            {
//                _dbcontext.Estados.Remove(oEstado);
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
