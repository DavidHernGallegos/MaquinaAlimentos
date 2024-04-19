using System;
using System.Collections.Generic;

namespace DL;

public partial class Monedum
{
    public int IdMoneda { get; set; }

    public int? Denominacion50 { get; set; }

    public int? Denominacion10 { get; set; }

    public int? Denominacion100 { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
