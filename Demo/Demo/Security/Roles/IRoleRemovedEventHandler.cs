using System.Threading.Tasks;

namespace Demo.Security.Roles
{
    public interface IRoleRemovedEventHandler
    {
        Task RoleRemovedAsync(string roleName);
    }
}