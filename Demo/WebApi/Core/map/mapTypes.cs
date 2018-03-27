using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Core.map
{
    public class mapTypes
    {
        public static MapType GoogleMap = new MapType { Id = 100, Name = "Google Map", Value = 100 };

        public IEnumerable<MapType> GetMapTypes()
        {
            return new[]
            {
                GoogleMap
            };
        }
    }
}
