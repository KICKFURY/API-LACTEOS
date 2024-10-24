using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Venta
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public decimal TotalVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public string TipoVenta { get; set; } = null!;

    public string NumeroFactura { get; set; } = null!;

    public virtual ICollection<DetallesVentum> DetallesVenta { get; set; } = new List<DetallesVentum>();

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
