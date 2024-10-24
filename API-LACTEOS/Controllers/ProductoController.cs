//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductoController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public ProductoController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<Producto> lista = new();

//            try
//            {
//                lista = _dbcontext.Productos.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idProducto:int}")]
//        public IActionResult Obtener(int idProducto)
//        {
//            Producto oProducto = _dbcontext.Productos.Find(idProducto);

//            if (oProducto == null)
//            {
//                return BadRequest("Producto no encontrado");
//            }

//            try
//            {
//                oProducto = _dbcontext.Productos.Where(p => p.Id == idProducto).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProducto });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oProducto });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] Producto objeto)
//        {
//            try
//            {
//                _dbcontext.Productos.Add(objeto);
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
//        public IActionResult Editar([FromBody] Producto objeto)
//        {
//            Producto oProducto = _dbcontext.Productos.Find(objeto.Id);

//            if (oProducto == null)
//            {
//                return BadRequest("Producto no encontrado");
//            }
//            try
//            {
//                oProducto.NombreProducto = objeto.NombreProducto is null ? oProducto.NombreProducto : objeto.NombreProducto;
//                oProducto.DescripcionProducto = objeto.DescripcionProducto is null ? oProducto.DescripcionProducto : objeto.DescripcionProducto;
//                oProducto.PrecioProducto = objeto.PrecioProducto;
//                oProducto.CantidadProducto = objeto.CantidadProducto;
//                oProducto.MinimoStockProducto = objeto.MinimoStockProducto;
//                oProducto.FechaExpiracionProducto = objeto.FechaExpiracionProducto != default ? objeto.FechaExpiracionProducto : DateTime.MinValue;


//                _dbcontext.Productos.Update(oProducto);
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
//            Producto oProducto = _dbcontext.Productos.Find(Id);

//            if (oProducto == null)
//            {
//                return BadRequest("Producto no encontrado");
//            }

//            try
//            {
//                _dbcontext.Productos.Remove(oProducto);
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
