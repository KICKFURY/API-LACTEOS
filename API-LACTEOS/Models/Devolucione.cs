using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Devolucione
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int CantidadDevuelta { get; set; }

    public string RazonDevolucion { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
