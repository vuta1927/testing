using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model.views
{
    public static class UserModel
    {
        public class UserView
        {
            public long Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string RoleName { get; set; }
            public int AcessFailedCount { get; set; }
            public bool IsLockoutEnabled { get; set; }
            public DateTime? LockoutEndDateUtc { get; set; }
            public bool IsActive { get; set; }
            public bool EmailConfirmed { get; set; }
            public DateTime? LastLoginTime { get; set; }
            public DateTime? CreationTime { get; set; }
        }

        public class UserEdit {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Username { get; set; }
            public string EmailAddress { get; set; }
            public string Password { get; set; }
            public bool IsActive { get; set; }
            public bool ShouldChangePasswordOnNextLogin{ get; set; }
        }

        public class UserRole
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleDisplayName { get; set; }
            public bool IsAssigned { get; set; }
        }
        public class UserForCreateOrEdit
        {
            public int AssignedRoleCount { get; set; }
            public bool IsEditMode { get; set; }
            public UserEdit User { get; set; }
            public List<UserRole> Roles { get; set; }
        }

        public class UserForCreate : UserEdit
        {
            public string PasswordRepeat { get; set; }
            public string[] AssignedRoleNames { get; set; }
            public bool SendActivationEmail { get; set; }
        }
    }

    


}
