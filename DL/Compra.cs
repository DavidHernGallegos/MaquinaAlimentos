using System;
using System.Collections.Generic;

namespace DL;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdAlimento { get; set; }

    public decimal? Total { get; set; }

    public decimal? Cambio { get; set; }

    public virtual Alimento? IdAlimentoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
