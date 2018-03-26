using System.Collections.Generic;
using Demo.Security.Permissions;
using Microsoft.Azure.KeyVault.Models;

namespace WebApi.Core.Authorization
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission MapViewPermission = new Permission(DemoPermissions.ViewMap);

        public static readonly Permission MapEditPermission = new Permission(DemoPermissions.EditMap);

        public static readonly Permission MapAddPermission = new Permission(DemoPermissions.AddMap);

        public static readonly Permission MapDeletePermission = new Permission(DemoPermissions.DeleteMap);

        public static readonly Permission ViewUserPermission = new Permission(DemoPermissions.ViewUser);

        public static readonly Permission EditUserPermission = new Permission(DemoPermissions.EditUser);

        public static readonly Permission AddUserPermission = new Permission(DemoPermissions.AddUser);

        public static readonly Permission DeleteUserPermission = new Permission(DemoPermissions.DeleteUser);

        public static readonly Permission ViewRolePermission = new Permission(DemoPermissions.ViewRole);

        public static readonly Permission EditRolePermission = new Permission(DemoPermissions.EditRole);

        public static readonly Permission AddRolePermission = new Permission(DemoPermissions.AddRole);

        public static readonly Permission DeleteRolePermission = new Permission(DemoPermissions.DeleteRole);



        //public static readonly Permission AdminPermission = new Permission(DemoPermissions.Administrator);

        //public static readonly Permission Page = new Permission(DemoPermissions.Page, children: new [] { AdminPermission });

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                MapViewPermission,
                MapEditPermission,
                MapAddPermission,
                MapDeletePermission,

                AddUserPermission,
                EditUserPermission,
                DeleteUserPermission,
                ViewUserPermission,

                AddRolePermission,
                EditRolePermission,
                DeleteRolePermission,
                ViewRolePermission,
                //Page,
                //AdminPermission
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Hanoi",
                    Permissions = new[] { MapViewPermission }
                },
                //new PermissionStereotype
                //{
                //    Name = "Administrator",
                //    Permissions = new []{Page, AdminPermission, ViewMapPermission, EditMapPermission }
                //}
            };
        }
    }
}
