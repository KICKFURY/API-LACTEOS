using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class DetallesVentum
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int CantidadVendida { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
