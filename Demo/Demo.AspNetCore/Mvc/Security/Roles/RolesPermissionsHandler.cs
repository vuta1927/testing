﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.AspNetCore.Mvc.Security.Permissions;
using Demo.Security;
using Demo.Security.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Demo.AspNetCore.Mvc.Security.Roles
{
    /// <summary>
    /// This authorization handler ensures that implied permissions are checked.
    /// </summary>
    public class RolesPermissionsHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly RoleManager<Role> _roleManager;

        public RolesPermissionsHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.HasSucceeded)
            {
                // This handler is not revoking any pre-existing grants.
                return;
            }

            // Determine which set of permissions would satisfy the access check
            var grantingNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            PermissionNames(requirement.Permission, grantingNames);

            // Determine what set of roles should be examined by the access check
            var rolesToExamine = new List<string> { "Anonymous" };

            if (context.User.Identity.IsAuthenticated)
            {
                rolesToExamine.Add("Authenticated");
                // Add roles from the user
                foreach (var claim in context.User.Claims)
                {
                    if (claim.Type == ClaimTypes.Role)
                    {
                        rolesToExamine.Add(claim.Value);
                    }
                }
            }

            foreach (var roleName in rolesToExamine)
            {
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role != null)
                {
                    foreach (var claim in role.RoleClaims)
                    {
                        if (!string.Equals(claim.ClaimType, Permission.ClaimType, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        string permissionName = claim.ClaimValue;

                        if (grantingNames.Contains(permissionName))
                        {
                            context.Succeed(requirement);
                            return;
                        }
                    }
                }
            }
        }

        private static void PermissionNames(Permission permission, HashSet<string> stack)
        {
            // The given name is tested
            stack.Add(permission.Name);

            // Iterate implied permissions to grant, it present
            if (permission.Children != null && permission.Children.Any())
            {
                foreach (var child in permission.Children)
                {
                    // Avoid potential recursion
                    if (stack.Contains(child.Name))
                    {
                        continue;
                    }

                    // Otherwise accumulate the implied permission names recursively
                    PermissionNames(child, stack);
                }
            }

            // SiteOwner permission grants them all
            stack.Add(StandardPermissions.SiteOwner.Name);
        }
    }
}