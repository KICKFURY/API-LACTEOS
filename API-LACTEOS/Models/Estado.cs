using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Estado
{
    public int Id { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
