using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class UnidadesMedida
{
    public int Id { get; set; }

    public string NombreUnidadMedida { get; set; } = null!;

    public string DescripcionUnidadMedida { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
