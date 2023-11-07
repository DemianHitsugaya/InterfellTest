using Repository;
using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class User : Entity
{
    public uint UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string UserName { get; set; } = null!;


    public string UserPsw { get; set; } = null!;

    public uint RolId { get; set; }
}
