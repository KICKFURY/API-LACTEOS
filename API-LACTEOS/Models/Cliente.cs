using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string ApellidoCliente { get; set; } = null!;

    public string Ruc { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
