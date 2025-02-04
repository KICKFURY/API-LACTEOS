﻿using API_LACTEOS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ServiceModel.Channels;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        public readonly LacteosBdContext _dbcontext;

        public VentasController(LacteosBdContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("getNumeroFactura")]
        public IActionResult GetNumeroFactura()
        {
            Venta venta = new Venta();
            try
            {
                venta = _dbcontext.Ventas.OrderBy(p => p.Id).Last();
                return StatusCode(StatusCodes.Status200OK, new { Message = "ok", response = venta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { Message = ex.Message, response = venta });
            }
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Venta> lista = new();
            try
            {
                lista = _dbcontext.Ventas.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpGet]
        [Route("ObtenerById/{id:int}")]
        public IActionResult ObtenerById(int id)
        {
            Venta venta = _dbcontext.Ventas.Find(id);

            if (venta == null)
            {
                return BadRequest("Venta no encontrado");
            }
            try
            {
                _dbcontext.Ventas.Update(venta);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = venta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = venta });
            }

        }

        [HttpGet]
        [Route("Obtener/{numeroFactura}")]
        public IActionResult Obtener(string numeroFactura)
        {
            Venta venta = new Venta();

            try
            {
                venta = _dbcontext.Ventas.Where(p => p.NumeroFactura == numeroFactura).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = venta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = venta });
            }
        }

        [HttpPost]
        [Route("Guardar/{rucCliente}&{vendedor}&{totalVenta:int}&{tipoVenta}&{numeroFactura}")]
        public IActionResult GuardarV1(string rucCliente, string vendedor, int totalVenta, string tipoVenta, string numeroFactura)
        {
            try
            {
                if (totalVenta < 0)
                {
                    return BadRequest(new { mensaje = "Datos de entrada no válidos" });
                }

                using (var transaction = _dbcontext.Database.BeginTransaction())
                {

                    try
                    {
                        if (_dbcontext.Ventas.OrderBy(p => p.NumeroFactura).Last().NumeroFactura != numeroFactura)
                        {
                            var venta = new Venta
                            {
                                IdCliente = _dbcontext.Clientes.Where(p => p.Ruc == rucCliente).FirstOrDefault().Id,
                                IdUsuario = _dbcontext.Usuarios.Where(p => p.NombreUsuario == vendedor).FirstOrDefault().Id,
                                TotalVenta = totalVenta,
                                TipoVenta = tipoVenta,
                                NumeroFactura = numeroFactura,
                                FechaVenta = DateTime.Now
                            };

                            _dbcontext.Ventas.Add(venta);
                            _dbcontext.SaveChanges();
                        }
                    }
                    catch (Exception error)
                    {
                        var venta = new Venta
                        {
                            IdCliente = _dbcontext.Clientes.Where(p => p.Ruc == rucCliente).FirstOrDefault().Id,
                            IdUsuario = _dbcontext.Usuarios.Where(p => p.NombreUsuario == vendedor).FirstOrDefault().Id,
                            TotalVenta = totalVenta,
                            TipoVenta = tipoVenta,
                            NumeroFactura = numeroFactura,
                            FechaVenta = DateTime.Now
                        };

                        _dbcontext.Ventas.Add(venta);
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
        public IActionResult Editar([FromBody] Venta objeto)
        {
            Venta oVenta = _dbcontext.Ventas.Find(objeto.Id);

            if (oVenta == null)
            {
                return BadRequest("Venta no encontrado");
            }
            try
            {
                oVenta.FechaVenta = objeto.FechaVenta != default ? objeto.FechaVenta : DateTime.MinValue;
                oVenta.TipoVenta = objeto.TipoVenta is null ? oVenta.TipoVenta : objeto.TipoVenta;
                //oVenta.TotalVenta = objeto.TotalVenta ?? oVenta.TotalVenta;

                _dbcontext.Ventas.Update(oVenta);
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
            Venta oVenta = _dbcontext.Ventas.Find(Id);
            if (oVenta == null)
            {
                return BadRequest("Venta no encontrada");
            }
            try
            {
                _dbcontext.Ventas.Remove(oVenta);
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
