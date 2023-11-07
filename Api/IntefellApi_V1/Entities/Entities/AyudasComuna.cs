using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class AyudasComuna : Entity
{
    public uint ComunaId { get; set; }

    public uint AyudaId { get; set; }
}
