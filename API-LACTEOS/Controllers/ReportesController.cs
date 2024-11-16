//using API_LACTEOS.Models;
using API_LACTEOS.Servicios;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API_LACTEOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ServiciosBD _dbContext;

        public ReportesController(ServiciosBD dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("usuarios")]
        public IActionResult ObtenerUsuarios()
        {
            var dt = _dbContext.ObtenerDatosUsuarios();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "Usuarios.rdlc");

            LocalReport report = new LocalReport(path);
            report.AddDataSource("dsUsuario", dt);

            var result = report.Execute(RenderType.Pdf);

            Response.Headers.Add("content-disposition", "inline; filename=reporte-usuarios.pdf");

            return File(result.MainStream, "application/pdf");
        }

        [HttpGet("proveedores")]
        public IActionResult ObtenerProveedores()
        {
            var dt = _dbContext.ObtenerDatosProveedores();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "Proveedores.rdlc");

            LocalReport report = new LocalReport(path);
            report.AddDataSource("dsProveedor", dt);

            var result = report.Execute(RenderType.Pdf);

            Response.Headers.Add("content-disposition", "inline; filename=reporte-usuarios.pdf");

            return File(result.MainStream, "application/pdf");
        }

        [HttpGet("productos")]
        public IActionResult ObtenerProdutos()
        {
            var dt = _dbContext.ObtenerDatosProductos();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "Productos.rdlc");

            LocalReport report = new LocalReport(path);
            report.AddDataSource("dsProducto", dt);

            var result = report.Execute(RenderType.Pdf);

            Response.Headers.Add("content-disposition", "inline; filename=reporte-productos.pdf");

            return File(result.MainStream, "application/pdf");
        }

        [HttpGet("clientes")]
        public IActionResult ObtenerClientes()
        {
            var dt = _dbContext.ObtenerDatosClientes();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "Clientes.rdlc");

            LocalReport report = new LocalReport(path);
            report.AddDataSource("dsCliente", dt);

            var result = report.Execute(RenderType.Pdf);

            Response.Headers.Add("content-disposition", "inline; filename=reporte-cliente.pdf");

            return File(result.MainStream, "application/pdf");
        }

        [HttpGet("reporte-pdf/{numeroFactura}")]
        public IActionResult GetReportePdf(string numeroFactura)
        {
            DataTable datos = _dbContext.ObtenerFacturaPorNumero(numeroFactura);

            string pathRDLC = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "FacturaContado.rdlc");

            LocalReport report = new LocalReport(pathRDLC);
            report.AddDataSource("dsVenta", datos);

            var result = report.Execute(RenderType.Pdf);

            Response.Headers.Add("content-disposition", "inline; filename=Factura.pdf");

            return File(result.MainStream, "application/pdf");
        }

        [HttpGet("facturaFecha/{fechaInicial}")]
        public IActionResult GetReporteFacturaFecha(string fechaInicial)
        {
            DataTable datos = _dbContext.ObtenerFacturaPorFechas(fechaInicial);

            string pathRDLC = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "FacturaFecha.rdlc");

            LocalReport report = new LocalReport(pathRDLC);
            report.AddDataSource("dsVenta", datos);

            var result = report.Execute(RenderType.Pdf);

            Response.Headers.Add("content-disposition", "inline; filename=FacturaFecha.pdf");

            return File(result.MainStream, "application/pdf");
        }
    }
}