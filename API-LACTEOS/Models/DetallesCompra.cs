using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class DetallesCompra
{
    public int Id { get; set; }

    public int IdCompra { get; set; }

    public int IdProducto { get; set; }

    public int CantidadComprada { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Compra IdCompraNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
