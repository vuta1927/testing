using System.Collections.Generic;
using Demo.Security.Permissions;
using Microsoft.Azure.KeyVault.Models;

namespace WebApi.Core.Authorization
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ViewMapPermission = new Permission(DemoPermissions.ViewMap);

        public static readonly Permission EditMapPermission = new Permission(DemoPermissions.MapEdit);

        public static readonly Permission MapAddPermission = new Permission(DemoPermissions.MapAdd);

        public static readonly Permission MapDeletePermission = new Permission(DemoPermissions.MapDelete);

        public static readonly Permission AdminPermission = new Permission(DemoPermissions.Administrator);

        public static readonly Permission Page = new Permission(DemoPermissions.Page, children: new [] { AdminPermission });

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                ViewMapPermission,
                EditMapPermission,
                MapAddPermission,
                MapDeletePermission,
                Page,
                AdminPermission
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Hanoi",
                    Permissions = new[] { ViewMapPermission, EditMapPermission, MapAddPermission, MapDeletePermission }
                },
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new []{Page, AdminPermission, ViewMapPermission, EditMapPermission }
                }
            };
        }
    }
}
