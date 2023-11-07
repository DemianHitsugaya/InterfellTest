using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class PersonaAyuda : Entity
{
    public string IdentificacionPersona { get; set; } = null!;

    public uint AyudaId { get; set; }

    public uint? Año { get; set; }
}
