using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Correo { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Contra { get; set; } = null!;

    public int IdRole { get; set; }

    public int IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
