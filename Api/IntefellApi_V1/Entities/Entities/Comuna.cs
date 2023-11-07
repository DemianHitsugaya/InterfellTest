using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Comuna : Entity
{
    public uint IdComuna { get; set; }

    public uint CodRegion { get; set; }

    public string NomComuna { get; set; } = null!;
}
