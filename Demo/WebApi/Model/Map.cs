using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class Map
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public virtual List<GoogleRoad> GoogleRoads { get; set; }
        public virtual List<CommentIcon> CommentIcons { get; set; }
    }
}
