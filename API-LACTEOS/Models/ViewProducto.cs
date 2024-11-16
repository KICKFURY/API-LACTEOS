using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class ViewProducto
{
    public string NombreProducto { get; set; } = null!;

    public string? DescripcionProducto { get; set; }

    public decimal PrecioProducto { get; set; }

    public int CantidadProducto { get; set; }

    public DateTime? FechaExpiracionProducto { get; set; }
}
