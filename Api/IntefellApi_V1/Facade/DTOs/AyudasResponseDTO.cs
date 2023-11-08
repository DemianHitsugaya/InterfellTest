using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class AyudasResponseDTO
    {
        public AyudaDTO Ayuda { get; set; }
        public IEnumerable<PersonasDTO>? Personas { get; set; }
        public IEnumerable<ComunaDTO>? Comunas { get; set; }
        public IEnumerable<RegionDTO>? Regiones { get; set; }
    }

    public class PersonaAyudasResponseDTO
    {
        public PersonasDTO Persona { get; set; }

        public IEnumerable<AyudaDTO> Ayudas { get; set;}
    }
}
