using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class MapComponent
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public virtual Map Map { get; set; }
        public virtual ICollection<GoogleRoad> Roads { get; set; }
    }
}
