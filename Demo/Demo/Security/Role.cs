using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Demo.Domain.Entities.Auditing;

namespace Demo.Security
{
    public class Role : FullAuditedEntity<int>, IFullAudited<User>
    {
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }

        public string Descriptions { get; set; }

        [ForeignKey("RoleId")]
        public ICollection<RoleClaim> RoleClaims { get; set; }

        public Role()
        {
            if (RoleClaims == null)
                RoleClaims = new List<RoleClaim>();
        }

        public virtual User CreatorUser { get; set; }
        public virtual User LastModifierUser { get; set; }
        public virtual User DeleterUser { get; set; }
    }
}