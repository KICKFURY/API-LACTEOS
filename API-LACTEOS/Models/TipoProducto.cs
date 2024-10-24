using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class TipoProducto
{
    public int Id { get; set; }

    public string NombreTipoProducto { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
