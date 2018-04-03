using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Map
    {
        public int Id { get; set; }
        public int MapTypeId { get; set; }
        public virtual MapType MapType { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public virtual List<GoogleRoad> GoogleRoads { get; set; }
        public virtual List<CommentIcon> CommentIcons { get; set; }
    }
}
