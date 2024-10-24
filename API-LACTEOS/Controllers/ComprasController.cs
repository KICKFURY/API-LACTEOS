//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using API_LACTEOS.Models;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ComprasController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public ComprasController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<Compra> lista = new();

//            try
//            {
//                lista = _dbcontext.Compras.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idCompra:int}")]
//        public IActionResult Obtener(int idCompra)
//        {
//            Compra oCompra = _dbcontext.Compras.Find(idCompra);

//            if (oCompra == null)
//            {
//                return BadRequest("Compra no encontrada");
//            }

//            try
//            {
//                oCompra = _dbcontext.Compras.Where(p => p.Id == idCompra).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oCompra });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oCompra });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] Compra objeto)
//        {
//            try
//            {
//                _dbcontext.Compras.Add(objeto);
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
//        public IActionResult Editar([FromBody] Compra objeto)
//        {
//            Compra oCompra = _dbcontext.Compras.Find(objeto.Id);

//            if (oCompra == null)
//            {
//                return BadRequest("Compra no encontrada");
//            }

//            try
//            {
//                oCompra.FechaCompra = objeto.FechaCompra != default ? objeto.FechaCompra : DateTime.MinValue;
//                oCompra.TotalCompra = objeto.TotalCompra;


//                _dbcontext.Compras.Update(oCompra);
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
//            Compra oCompra = _dbcontext.Compras.Find(Id);

//            if (oCompra == null)
//            {
//                return BadRequest("Compra no encontrada");
//            }

//            try
//            {
//                _dbcontext.Compras.Remove(oCompra);
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
