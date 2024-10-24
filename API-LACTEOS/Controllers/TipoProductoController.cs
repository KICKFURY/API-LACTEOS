//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TipoProductoController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public TipoProductoController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<TipoProducto> lista = new();

//            try
//            {
//                lista = _dbcontext.TipoProductos.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idTipoProducto:int}")]
//        public IActionResult Obtener(int idTipoProducto)
//        {
//            TipoProducto oTipoProducto = _dbcontext.TipoProductos.Find(idTipoProducto);

//            if (oTipoProducto == null)
//            {
//                return BadRequest("Tipo de producto no encontrado");
//            }

//            try
//            {
//                oTipoProducto = _dbcontext.TipoProductos.Where(p => p.Id == idTipoProducto).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oTipoProducto });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oTipoProducto });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] TipoProducto objeto)
//        {
//            try
//            {
//                _dbcontext.TipoProductos.Add(objeto);
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
//        public IActionResult Editar([FromBody] TipoProducto objeto)
//        {
//            TipoProducto oTipoProducto = _dbcontext.TipoProductos.Find(objeto.Id);

//            if (oTipoProducto == null)
//            {
//                return BadRequest("Tipo de producto no encontrado");
//            }
//            try
//            {
//                oTipoProducto.NombreTipoProducto = objeto.NombreTipoProducto is null ? oTipoProducto.NombreTipoProducto : objeto.NombreTipoProducto;

//                _dbcontext.TipoProductos.Update(oTipoProducto);
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
//            TipoProducto oTipoProducto = _dbcontext.TipoProductos.Find(Id);

//            if (oTipoProducto == null)
//            {
//                return BadRequest("Tipo de producto no encontrado");
//            }

//            try
//            {
//                _dbcontext.TipoProductos.Remove(oTipoProducto);
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
