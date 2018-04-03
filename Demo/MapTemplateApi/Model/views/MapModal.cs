using System.Collections.Generic;

namespace DAL.Model.views
{
    public static class MapModel
    {
        public class MapBase
        {
            public int Id { get; set; }
            public int Type { get; set; }
            public string Name { get; set; }
            public string Descriptions { get; set; }
        }

        public class RoleMap
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleDisplayName { get; set; }
            public bool IsAssigned { get; set; }
        }

        public class MapView : MapUpdateEdit
        {
            public new MapType Type { get; set; }
            public string TypeName { get; set; }
            public List<RoleMap> Roles { get; set; }
        }

        public class MapUpdateEdit : MapBase
        {
            public string[] RolesAssigned { get; set; }
        }
    }
}