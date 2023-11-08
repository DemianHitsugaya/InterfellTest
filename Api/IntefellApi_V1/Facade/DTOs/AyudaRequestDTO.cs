using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class AyudaRequestDTO
    {
        public int IdAyuda { get; set; }
        public string AyudaNombre { get; set; }
        public bool LoadPersonas { get; set; }
        public bool LoadRegiones { get; set; }
        public bool LoadComunas { get; set; }

    }
}
