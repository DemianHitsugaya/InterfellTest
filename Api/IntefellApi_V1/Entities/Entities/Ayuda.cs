using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Ayuda:Entity
{
    public uint IdAyuda { get; set; }

    public string NomAyuda { get; set; } = null!;

    public bool? EsRegional { get; set; }

    public bool? EsComunal { get; set; }

    public uint? Valor { get; set; }

    public string? Moneda { get; set; }
}
