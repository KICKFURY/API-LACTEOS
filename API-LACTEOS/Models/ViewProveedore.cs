using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class ViewProveedore
{
    public int Id { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string TelefonoProveedor { get; set; } = null!;

    public string RucProveedor { get; set; } = null!;

    public string NombreEstado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }
}
