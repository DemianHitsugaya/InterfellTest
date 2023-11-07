using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class RegionDTO
    {
        public string NomRegion { get; set; } = null!;

        public string? SiglaRegion { get; set; }

        public PaisDTO? Pais{ get; set; }
    }
}
