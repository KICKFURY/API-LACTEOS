//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DetallesVentaController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public DetallesVentaController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<DetallesVentum> lista = new();

//            try
//            {
//                lista = _dbcontext.DetallesVenta.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idDetallesVenta:int}")]
//        public IActionResult Obtener(int idDetallesVenta)
//        {
//            DetallesVentum oDetallesVenta = _dbcontext.DetallesVenta.Find(idDetallesVenta);

//            if (oDetallesVenta == null)
//            {
//                return BadRequest("Detalles de Venta no encontrado");
//            }

//            try
//            {
//                oDetallesVenta = _dbcontext.DetallesVenta.Where(p => p.Id == idDetallesVenta).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oDetallesVenta });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oDetallesVenta });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] DetallesVentum objeto)
//        {
//            try
//            {
//                _dbcontext.DetallesVenta.Add(objeto);
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
//        public IActionResult Editar([FromBody] DetallesVentum objeto)
//        {
//            DetallesVentum oDetallesVenta = _dbcontext.DetallesVenta.Find(objeto.Id);

//            if (oDetallesVenta == null)
//            {
//                return BadRequest("Detalles de venta no encontrado");
//            }

//            try
//            {

//                oDetallesVenta.NumeroDetallesVenta = objeto.NumeroDetallesVenta;
//                oDetallesVenta.Cantidad = objeto.Cantidad;
//                oDetallesVenta.Precio = objeto.Precio;
//                oDetallesVenta.Subtotal = objeto.Subtotal ?? oDetallesVenta.Subtotal;
//                oDetallesVenta.Estado = objeto.Estado is null ? oDetallesVenta.Estado : objeto.Estado;


//                _dbcontext.DetallesVenta.Update(oDetallesVenta);
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
//            DetallesVentum oDetallesVenta = _dbcontext.DetallesVenta.Find(Id);

//            if (oDetallesVenta == null)
//            {
//                return BadRequest("Detalles de Venta no encontrada");
//            }

//            try
//            {
//                _dbcontext.DetallesVenta.Remove(oDetallesVenta);
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
