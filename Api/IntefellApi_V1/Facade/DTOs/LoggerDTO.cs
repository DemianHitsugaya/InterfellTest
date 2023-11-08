using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class LoggerDTO
    {
        public DateOnly Fecha { get; set; }

        public TimeOnly Hora { get; set; }

        public string Accion { get; set; } = null!;

        public uint UserId { get; set; }

        public string Entidad { get; set; } = null!;

        public string PrevData { get; set; } = null!;

        public string? PostData { get; set; }
    }
}
