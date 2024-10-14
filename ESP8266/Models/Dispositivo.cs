using System;
using System.Collections.Generic;

namespace ESP8266.Models;

public partial class Dispositivo
{
    public int IdDispositivo { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public string? Observacion { get; set; }
}
