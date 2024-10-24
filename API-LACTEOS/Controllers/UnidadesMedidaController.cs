//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UnidadesMedidaController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public UnidadesMedidaController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<UnidadesMedida> lista = new();

//            try
//            {
//                lista = _dbcontext.UnidadesMedidas.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idUnidadesMedida:int}")]
//        public IActionResult Obtener(int idUnidadesMedida)
//        {
//            UnidadesMedida oUnidadesMedida = _dbcontext.UnidadesMedidas.Find(idUnidadesMedida);

//            if (oUnidadesMedida == null)
//            {
//                return BadRequest("Unidades de medida no encontrado");
//            }

//            try
//            {
//                oUnidadesMedida = _dbcontext.UnidadesMedidas.Where(p => p.Id == idUnidadesMedida).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oUnidadesMedida });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oUnidadesMedida });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] UnidadesMedida objeto)
//        {
//            try
//            {
//                _dbcontext.UnidadesMedidas.Add(objeto);
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
//        public IActionResult Editar([FromBody] UnidadesMedida objeto)
//        {
//            UnidadesMedida oUnidadesMedida = _dbcontext.UnidadesMedidas.Find(objeto.Id);

//            if (oUnidadesMedida == null)
//            {
//                return BadRequest("Unidades de medida no encontrado");
//            }
//            try
//            {
//                oUnidadesMedida.NombreUnidadMedida = objeto.NombreUnidadMedida is null ? oUnidadesMedida.NombreUnidadMedida : objeto.NombreUnidadMedida;
//                oUnidadesMedida.DescripcionUnidadMedida = objeto.DescripcionUnidadMedida is null ? oUnidadesMedida.DescripcionUnidadMedida : objeto.DescripcionUnidadMedida;

//                _dbcontext.UnidadesMedidas.Update(oUnidadesMedida);
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
//            UnidadesMedida oUnidadesMedida = _dbcontext.UnidadesMedidas.Find(Id);

//            if (oUnidadesMedida == null)
//            {
//                return BadRequest("Unidades de medida no encontrado");
//            }

//            try
//            {
//                _dbcontext.UnidadesMedidas.Remove(oUnidadesMedida);
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
