using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_LACTEOS.Models;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public ComprasController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {

            List<Compra> lista = new();

            try
            {
                lista = _dbcontext.Compras.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("ultimaFactura")]
        public IActionResult NumeroFactura()
        {
            try
            {
                try
                {
                    var nf = _dbcontext.Compras.OrderBy(p => p.NumeroFacutura).Last().NumeroFacutura;
                    return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = nf });
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = 1 });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


        [HttpGet]
        [Route("Obtener/{idCompra:int}")]
        public IActionResult Obtener(int idCompra)
        {
            Compra oCompra = _dbcontext.Compras.Find(idCompra);

            if (oCompra == null)
            {
                return BadRequest("Compra no encontrada");
            }

            try
            {
                oCompra = _dbcontext.Compras.Where(p => p.Id == idCompra).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oCompra });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oCompra });
            }
        }

        [HttpPost]
        [Route("Guardar/{rucProveedor}&{nombreUsuario}&{totalCompra:int}&{numeroFactura}")]
        public IActionResult Guardar(string rucProveedor, string nombreUsuario, int totalCompra, string numeroFactura)
        {
            try
            {
                if (totalCompra < 0)
                {
                    return BadRequest(new { mensaje = "Datos de entrada no válidos" });
                }

                using (var transaction = _dbcontext.Database.BeginTransaction())
                {
                    try
                    {
                        if (_dbcontext.Compras.OrderBy(p => p.NumeroFacutura).Last().NumeroFacutura != numeroFactura)
                        {
                            var compra = new Compra
                            {
                                IdProveedor = _dbcontext.Proveedores.Where(p => p.RucProveedor == rucProveedor).FirstOrDefault().Id,
                                IdUsuario = _dbcontext.Usuarios.Where(p => p.NombreUsuario == nombreUsuario).FirstOrDefault().Id,
                                TotalCompra = totalCompra,
                                NumeroFacutura = numeroFactura,
                                FechaCompra = DateTime.Now
                            };

                            _dbcontext.Compras.Add(compra);
                            _dbcontext.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        var compra = new Compra
                        {
                            IdProveedor = _dbcontext.Proveedores.Where(p => p.RucProveedor == rucProveedor).FirstOrDefault().Id,
                            IdUsuario = _dbcontext.Usuarios.Where(p => p.NombreUsuario == nombreUsuario).FirstOrDefault().Id,
                            TotalCompra = totalCompra,
                            NumeroFacutura = numeroFactura,
                            FechaCompra = DateTime.Now
                        };

                        _dbcontext.Compras.Add(compra);
                        _dbcontext.SaveChanges();
                    }

                    transaction.Commit();
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Compra objeto)
        {
            Compra oCompra = _dbcontext.Compras.Find(objeto.Id);

            if (oCompra == null)
            {
                return BadRequest("Compra no encontrada");
            }

            try
            {
                oCompra.FechaCompra = objeto.FechaCompra != default ? objeto.FechaCompra : DateTime.MinValue;
                oCompra.TotalCompra = objeto.TotalCompra;
                _dbcontext.Compras.Update(oCompra);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public IActionResult Eliminar(int Id)
        {
            Compra oCompra = _dbcontext.Compras.Find(Id);

            if (oCompra == null)
            {
                return BadRequest("Compra no encontrada");
            }

            try
            {
                _dbcontext.Compras.Remove(oCompra);
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