using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Logger : Entity
{
    public uint IdLogger { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public string Accion { get; set; } = null!;

    public uint UserId { get; set; }

    public string Entidad { get; set; } = null!;

    public string PrevData { get; set; } = null!;

    public string? PostData { get; set; }
}
