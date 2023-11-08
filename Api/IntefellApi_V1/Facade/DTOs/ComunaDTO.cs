using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class ComunaDTO
    {
        public int IdComuna;
        public string NomComuna { get; set; } = null!;

        public RegionDTO? Region { get; set; }
    }
}
