//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VentasController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public VentasController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<Venta> lista = new();

//            try
//            {
//                lista = _dbcontext.Ventas.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idVenta:int}")]
//        public IActionResult Obtener(int idVenta)
//        {
//            Venta oVenta = _dbcontext.Ventas.Find(idVenta);

//            if (oVenta == null)
//            {
//                return BadRequest("Venta no encontrada");
//            }

//            try
//            {
//                oVenta = _dbcontext.Ventas.Where(p => p.Id == idVenta).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oVenta });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oVenta });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] Venta objeto)
//        {
//            try
//            {
//                _dbcontext.Ventas.Add(objeto);
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
//        public IActionResult Editar([FromBody] Venta objeto)
//        {
//            Venta oVenta = _dbcontext.Ventas.Find(objeto.Id);

//            if (oVenta == null)
//            {
//                return BadRequest("Venta no encontrado");
//            }
//            try
//            {
//                oVenta.FechaVenta = objeto.FechaVenta != default ? objeto.FechaVenta : DateTime.MinValue;
//                oVenta.TipoVenta = objeto.TipoVenta is null ? oVenta.TipoVenta : objeto.TipoVenta;
//                oVenta.TotalVenta = objeto.TotalVenta ?? oVenta.TotalVenta;


//                _dbcontext.Ventas.Update(oVenta);
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
//            Venta oVenta = _dbcontext.Ventas.Find(Id);

//            if (oVenta == null)
//            {
//                return BadRequest("Venta no encontrada");
//            }

//            try
//            {
//                _dbcontext.Ventas.Remove(oVenta);
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
