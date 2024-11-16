using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? DescripcionProducto { get; set; }

    public decimal PrecioProducto { get; set; }

    public int CantidadProducto { get; set; }

    public int MinimoStockProducto { get; set; }

    public DateTime? FechaExpiracionProducto { get; set; }

    public virtual ICollection<DetallesCompra> DetallesCompras { get; set; } = new List<DetallesCompra>();

    public virtual ICollection<DetallesVentum> DetallesVenta { get; set; } = new List<DetallesVentum>();

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual ICollection<Perdida> Perdida { get; set; } = new List<Perdida>();
}
