using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Role : Entity
{
    public uint IdRol { get; set; }

    public string NomRol { get; set; } = null!;

    public string SiglaRol { get; set; } = null!;
}
