using System;
using System.Collections.Generic;

namespace ESP8266.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Clave { get; set; } = null!;
}
