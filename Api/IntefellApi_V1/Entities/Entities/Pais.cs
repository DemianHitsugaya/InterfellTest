using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Pais : Entity
{
    public uint IdPais { get; set; }

    public string NomPais { get; set; } = null!;

    public string? SiglaPais { get; set; }

    public string Moneda { get; set; } = null!;
}
