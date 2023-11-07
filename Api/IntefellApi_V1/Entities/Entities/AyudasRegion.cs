using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class AyudasRegion : Entity
{
    public uint RegionId { get; set; }

    public uint AyudaId { get; set; }
}
