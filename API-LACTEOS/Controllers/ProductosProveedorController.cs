//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductosProveedorController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public ProductosProveedorController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<ProductosProveedore> lista = new();

//            try
//            {
//                lista = _dbcontext.ProductosProveedores.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idProductosProveedore:int}")]
//        public IActionResult Obtener(int idProductosProveedore)
//        {
//            ProductosProveedore oProductosProveedore = _dbcontext.ProductosProveedores.Find(idProductosProveedore);

//            if (oProductosProveedore == null)
//            {
//                return BadRequest("Producto de proveedor no encontrado");
//            }

//            try
//            {
//                oProductosProveedore = _dbcontext.ProductosProveedores.Where(p => p.Id == idProductosProveedore).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProductosProveedore });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oProductosProveedore });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] ProductosProveedore objeto)
//        {
//            try
//            {
//                _dbcontext.ProductosProveedores.Add(objeto);
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
//        public IActionResult Editar([FromBody] ProductosProveedore objeto)
//        {
//            ProductosProveedore oProductosProveedore = _dbcontext.ProductosProveedores.Find(objeto.Id);

//            if (oProductosProveedore == null)
//            {
//                return BadRequest("Producto de proveedores no encontrado");
//            }
//            try
//            {

//                _dbcontext.ProductosProveedores.Update(oProductosProveedore);
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
//            ProductosProveedore oProductosProveedore = _dbcontext.ProductosProveedores.Find(Id);

//            if (oProductosProveedore == null)
//            {
//                return BadRequest("Producto de proveedor no encontrado");
//            }

//            try
//            {
//                _dbcontext.ProductosProveedores.Remove(oProductosProveedore);
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
