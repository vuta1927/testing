using System.Collections.Generic;
using Demo.Security.Permissions;

namespace DAL.Core.Authorization
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission MapViewPermission = new Permission
        {
            Name = DemoPermissions.ViewMap, DisplayName = "View", Description = "Allow view map", Category = "MAP"
        };

        public static readonly Permission MapEditPermission = new Permission{Name = DemoPermissions.EditMap, DisplayName = "Edit", Description = "Allow to edit components on Map", Category = "MAP"};

        public static readonly Permission MapAddPermission = new Permission{Name = DemoPermissions.AddMap, DisplayName = "Add", Description = "Allow to add new components on Map", Category = "MAP"};

        public static readonly Permission MapDeletePermission = new Permission{Name = DemoPermissions.DeleteMap, DisplayName = "Delete", Description = "Allow to delete components on Map", Category = "MAP"};

        public static readonly Permission ViewUserPermission = new Permission{Name = DemoPermissions.ViewUser, DisplayName = "View", Description = "Allow view users", Category = "USER"};

        public static readonly Permission EditUserPermission = new Permission{Name = DemoPermissions.EditUser, DisplayName = "Edit", Description = "Allow to edit user profile", Category = "USER"};

        public static readonly Permission AddUserPermission = new Permission{Name = DemoPermissions.AddUser, DisplayName = "Add", Description = "Allow to add create user", Category = "USER"};

        public static readonly Permission DeleteUserPermission = new Permission{Name = DemoPermissions.DeleteUser, DisplayName = "Delete", Description = "Allow to delete user", Category = "USER"};

        public static readonly Permission ViewRolePermission = new Permission{Name = DemoPermissions.ViewRole, DisplayName = "View", Description = "Allow to view roles", Category = "ROLE"};

        public static readonly Permission EditRolePermission = new Permission { Name = DemoPermissions.EditRole, DisplayName = "Edit", Description = "Allow to edit role", Category = "ROLE" };

        public static readonly Permission AddRolePermission = new Permission { Name = DemoPermissions.AddRole, DisplayName = "Add", Description = "Allow to add role", Category = "ROLE" };

        public static readonly Permission DeleteRolePermission = new Permission { Name = DemoPermissions.DeleteRole, DisplayName = "Delete", Description = "Allow to delete role", Category = "ROLE" };

        public static readonly Permission MapManagePermissions = new Permission { Name = DemoPermissions.MapManage, DisplayName = "Map mangement", Description = "Allow to manage maps", Category = "MAP" };

        public static readonly Permission AdministratorPermissions = new Permission { Name = DemoPermissions.Administrator, DisplayName = "Administrator", Description = "Administrator permission", Category = "ADMIN" };

        //public static readonly Permission AdminPermission = new Permission(DemoPermissions.Administrator);

        //public static readonly Permission Page = new Permission(DemoPermissions.Page, children: new [] { AdminPermission });

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                AdministratorPermissions,

                MapManagePermissions,
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
                    Name = "Map view group",
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
