using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Map
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public bool EditMode { get; set; }
        public virtual List<Road> Roads { get; set; }
    }
}
