using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class PersonasDTO
    {

        public int? Id { get; set; }
        public string Identificacion { get; set; } = null!;

        public uint TipoIdentificacion { get; set; }

        public string PrimerNombre { get; set; } = null!;

        public string? SegundoNombre { get; set; }

        public string PrimerApellido { get; set; } = null!;

        public string? SegundoApellido { get; set; }

        public uint ComunaId { get; set; }
    }
}
