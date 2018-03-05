using System.Threading.Tasks;
using Demo.Security;
using Demo.Security.Roles;
using Microsoft.AspNetCore.Identity;

namespace Demo.AspNetCore.Mvc.Security
{
    public class UserRoleRemovedEventHandler : IRoleRemovedEventHandler
    {
        private readonly UserManager<User> _userManager;

        public UserRoleRemovedEventHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task RoleRemovedAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);

            foreach (var user in users)
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
        }
    }
}