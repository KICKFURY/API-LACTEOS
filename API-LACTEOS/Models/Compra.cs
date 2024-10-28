using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Compra
{
    public int Id { get; set; }

    public int IdProveedor { get; set; }

    public int IdUsuario { get; set; }

    public decimal TotalCompra { get; set; }

    public DateTime FechaCompra { get; set; }

    public string NumeroFacutura { get; set; } = null!;

    public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
