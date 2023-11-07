using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade.DTOs
{
    public class UserDTO
    {
        public string FullName { get; set; } = null!;

        public string UserName { get; set; } = null!;


        public string UserPsw { get; set; } = null!;

        public uint RolId { get; set; } = 2;
    }
}
