using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Persona : Entity
{
    public string Identificacion { get; set; } = null!;

    public uint IdPersona { get; set; }

    public uint TipoIdentificacion { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public uint ComunaId { get; set; }
}
