using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Perdida
{
    public int Id { get; set; }

    public int IdProducto { get; set; }

    public int CantidadPerdida { get; set; }

    public string RazonPerdida { get; set; } = null!;

    public DateTime FechaPerdida { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
