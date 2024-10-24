using System;
using System.Collections.Generic;

namespace API_LACTEOS.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public decimal TotalPago { get; set; }

    public int Plazo { get; set; }

    public DateTime FechaPago { get; set; }

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
