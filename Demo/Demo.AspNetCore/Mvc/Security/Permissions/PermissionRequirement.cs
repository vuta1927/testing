using Demo.Helpers.Exception;
using Demo.Security.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Demo.AspNetCore.Mvc.Security.Permissions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(Permission permission)
        {
            Throw.IfArgumentNull(permission, nameof(permission));
            Permission = permission;
        }

        public Permission Permission { get; set; }
    }
}