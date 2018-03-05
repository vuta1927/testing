using System.Collections.Generic;

namespace Demo.Security.Permissions
{
    public interface IPermissionProviderService
    {
        IEnumerable<Permission> GetPermissions();
        Permission GetPermissionBy(string name);
    }
}