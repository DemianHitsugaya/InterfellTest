using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class AyudaDTO
    {
        public int? IdAyuda { get; set; }

        public string NomAyuda { get; set; } = null!;

        public bool? EsRegional { get; set; }

        public int? RegionID {  get; set; }

        public bool? EsComunal { get; set; }
        
        public int? ComunaId { get; set; }

        public uint? Valor { get; set; }

        public string? Moneda { get; set; }
    }

    public class AyudaRegionDTO
    {
        public uint RegionId { get; set; }

        public uint AyudaId { get; set; }
    }

    public class AyudaComunaDTO
    {
        public uint ComunaId { get; set; }

        public uint AyudaId { get; set; }
    }
}
