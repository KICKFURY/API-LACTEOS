//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using API_LACTEOS.Models;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PerdidasController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public PerdidasController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<Perdida> lista = new();

//            try
//            {
//                lista = _dbcontext.Perdidas.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idPerdida:int}")]
//        public IActionResult Obtener(int idPerdida)
//        {
//            Perdida oPerdida = _dbcontext.Perdidas.Find(idPerdida);

//            if (oPerdida == null)
//            {
//                return BadRequest("Perdida no encontrada");
//            }

//            try
//            {
//                oPerdida = _dbcontext.Perdidas.Where(p => p.Id == idPerdida).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oPerdida });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oPerdida });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] Perdida objeto)
//        {
//            try
//            {
//                _dbcontext.Perdidas.Add(objeto);
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
//        public IActionResult Editar([FromBody] Perdida objeto)
//        {
//            Perdida oPerdida = _dbcontext.Perdidas.Find(objeto.Id);

//            if (oPerdida == null)
//            {
//                return BadRequest("Perdida no encontrada");
//            }
//            try
//            {
//                oPerdida.MotivoDevolucion = objeto.MotivoDevolucion is null ? oPerdida.MotivoDevolucion : objeto.MotivoDevolucion;
//                oPerdida.CantidadProducto = objeto.CantidadProducto;
           
//                _dbcontext.Perdidas.Update(oPerdida);
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
//            Perdida oPerdida = _dbcontext.Perdidas.Find(Id);

//            if (oPerdida == null)
//            {
//                return BadRequest("Perdida no encontrado");
//            }

//            try
//            {
//                _dbcontext.Perdidas.Remove(oPerdida);
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
