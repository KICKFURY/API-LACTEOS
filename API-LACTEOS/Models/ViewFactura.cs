using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class ViewFactura
{
    public string NombreUsuario { get; set; } = null!;

    public string NombreCliente { get; set; } = null!;

    public string ApellidoCliente { get; set; } = null!;

    public decimal TotalVenta { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public int CantidadVendida { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string TipoVenta { get; set; } = null!;
}
