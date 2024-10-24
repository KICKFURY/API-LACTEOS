using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Proveedore
{
    public int Id { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string TelefonoProveedor { get; set; } = null!;

    public string RucProveedor { get; set; } = null!;

    public int IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<ProductosProveedore> ProductosProveedores { get; set; } = new List<ProductosProveedore>();
}
