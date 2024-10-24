using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class ProductosProveedore
{
    public int Id { get; set; }

    public int IdProveedor { get; set; }

    public int IdProducto { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}
