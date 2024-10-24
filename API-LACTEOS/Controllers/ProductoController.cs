using API_LACTEOS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public ProductoController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Producto> lista = new();
            try
            {
                lista = _dbcontext.Productos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{nombreProducto}")]
        public IActionResult Obtener(string nombreProducto)
        {
            Producto oProducto = new Producto();
            try
            {
                oProducto = _dbcontext.Productos.Where(p => p.NombreProducto == nombreProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oProducto });
            }
        }

        [HttpPost]
        [Route("Guardar/{nombre}&{descripcion}&{precio}&{cantidad}&{idTipoProducto}&{minimoStock}&{idUnidadMedida}&{fechaExpiracion}")]
        public IActionResult Guardar(string nombre, string descripcion, int precio, int cantidad, int idTipoProducto, int minimoStock, int idUnidadMedida, DateTime fechaExpiracion)
        {
            Producto producto = new Producto();
            try
            {
                producto.NombreProducto = nombre;
                producto.DescripcionProducto = descripcion;
                producto.PrecioProducto = precio;
                producto.CantidadProducto = cantidad;
                producto.IdTipoProducto = idTipoProducto;
                producto.MinimoStockProducto = minimoStock;
                producto.IdUnidadMedida = idUnidadMedida;
                producto.FechaExpiracionProducto = fechaExpiracion;
                _dbcontext.Productos.Add(producto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{nombre}&{descripcion}&{precio}&{cantidad}&{idTipoProducto}&{minimoStock}&{idUnidadMedida}&{fechaExpiracion}")]
        public IActionResult Editar(string nombre, string descripcion, int precio, int cantidad, int idTipoProducto, int minimoStock, int idUnidadMedida, DateTime fechaExpiracion)
        {
            Producto producto = new Producto();
            try
            {
                producto = _dbcontext.Productos.Where(p => p.NombreProducto == nombre).FirstOrDefault();
                producto.NombreProducto = nombre;
                producto.DescripcionProducto = descripcion;
                producto.PrecioProducto = precio;
                producto.CantidadProducto = cantidad;
                producto.IdTipoProducto = idTipoProducto;
                producto.MinimoStockProducto = minimoStock;
                producto.IdUnidadMedida = idUnidadMedida;
                producto.FechaExpiracionProducto = fechaExpiracion;
                _dbcontext.Productos.Update(producto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{nombreProducto}")]
        public IActionResult Eliminar(string nombreProducto)
        {
            Producto oProducto = new Producto();

            try
            {
                oProducto = _dbcontext.Productos.Where(p => p.NombreProducto == nombreProducto).FirstOrDefault();
                _dbcontext.Productos.Remove(oProducto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
