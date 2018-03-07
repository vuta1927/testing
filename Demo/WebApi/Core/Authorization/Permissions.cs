using System.Collections.Generic;
using Demo.Security.Permissions;
using Microsoft.Azure.KeyVault.Models;

namespace WebApi.Core.Authorization
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ViewMapPermission = new Permission(DemoPermissions.ViewMap);

        public static readonly Permission EditMapPermission = new Permission(DemoPermissions.EditMap);

        public static readonly Permission AdminPermission = new Permission(DemoPermissions.Administrator);

        public static readonly Permission Page = new Permission(DemoPermissions.Page, children: new [] { AdminPermission });

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                ViewMapPermission,
                EditMapPermission,
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
                    Name = "Map",
                    Permissions = new[] { ViewMapPermission, EditMapPermission }
                },
                new PermissionStereotype
                {
                    Name = "Gmap",
                    Permissions = new[]{ ViewMapPermission, EditMapPermission }
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
