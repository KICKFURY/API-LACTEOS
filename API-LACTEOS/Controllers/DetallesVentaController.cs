using API_LACTEOS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesVentaController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public DetallesVentaController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<DetallesVentum> lista = new();
            try
            {
                lista = _dbcontext.DetallesVenta.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{idVenta:int}")]
        public IActionResult Obtener(int idVenta)
        {
            DetallesVentum oDetallesVenta = new DetallesVentum();
            try
            {
                oDetallesVenta = _dbcontext.DetallesVenta.Where(p => p.IdVenta == idVenta).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oDetallesVenta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oDetallesVenta });
            }
        }

        [HttpPost]
        [Route("Guardar/{nombreProducto}&{cantidadVendida:int}&{precioUnitario:int}")]
        public IActionResult Guardar(string nombreProducto, int cantidadVendida, int precioUnitario)
        {
            try
            {
                if (cantidadVendida <= 0 || precioUnitario < 0)
                {
                    return BadRequest(new { mensaje = "Datos de entrada no válidos." });
                }

                using (var transaction = _dbcontext.Database.BeginTransaction())
                {
                    var producto = _dbcontext.Productos.FirstOrDefault(p => p.NombreProducto == nombreProducto);
                    if (producto == null)
                    {
                        return NotFound(new { mensaje = "Producto no encontrado." });
                    }

                    var detallesVentum = new DetallesVentum
                    {
                        IdProducto = producto.Id,
                        CantidadVendida = cantidadVendida,
                        PrecioUnitario = precioUnitario,
                        IdVenta = _dbcontext.Ventas.OrderBy(p => p.Id).Last().Id
                    };

                    _dbcontext.DetallesVenta.Add(detallesVentum);
                    _dbcontext.SaveChanges();

                    transaction.Commit();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
