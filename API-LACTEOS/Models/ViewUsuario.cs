using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class ViewUsuario
{
    public int Id { get; set; }

    public string Correo { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Contra { get; set; } = null!;

    public string NombreRol { get; set; } = null!;

    public string NombreEstado { get; set; } = null!;
}
