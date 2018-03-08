using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Security;

namespace WebApi.Model
{
    public class MapRole
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public virtual Map Map { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
