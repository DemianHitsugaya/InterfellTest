using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum Entidades
    {
        Ayudas = 1,
        Ayudas_Comuna,
        Ayudas_Region,
        Comuna,
        Logger,
        Pais,
        Persona,
        Persona_Ayudas,
        Region,
        Roles,
        Users
    }

    public enum Acciones
    {
        Create = 1,
        CreateRange,
        Update,
        Delete,
        DeleteRange,
        ReadOne,
        ReadAll,
        Login
    }
}
