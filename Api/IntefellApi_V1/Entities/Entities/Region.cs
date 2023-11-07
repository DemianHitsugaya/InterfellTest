using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Region : Entity
{
    public uint IdRegion { get; set; }

    public uint PaisId { get; set; }

    public string NomRegion { get; set; } = null!;

    public string? SiglaRegion { get; set; }
}
