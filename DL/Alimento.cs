using System;
using System.Collections.Generic;

namespace DL;

public partial class Alimento
{
    public int IdAlimento { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; }

    public decimal? Precio { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
