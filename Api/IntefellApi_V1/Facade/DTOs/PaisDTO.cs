using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class PaisDTO
    {
        public string NomPais { get; set; } = null!;

        public string? SiglaPais { get; set; }

        public string Moneda { get; set; } = null!;
    }
}
