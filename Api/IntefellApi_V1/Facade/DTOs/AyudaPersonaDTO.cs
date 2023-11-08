using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class AyudaPersonaAddDTO
    {
        public string IdentificacionPersona { get; set; } = null!;

        public uint AyudaId { get; set; }

    }

    public class AyudaPersonaDTO
    {
        public PersonasDTO? persona{ get; set; } = null!;

        public AyudaDTO? Ayuda{ get; set; }

        public uint? Año { get; set; }
    }
}
