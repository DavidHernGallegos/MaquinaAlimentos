using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
