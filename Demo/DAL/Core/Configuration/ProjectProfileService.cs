using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.Security;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace DAL.Core.Configuration
{
    public class ProjectProfileService: IProfileService
    {
        protected UserManager<User> _userManager;
        public ProjectProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = _userManager.GetUserAsync(context.Subject).Result;
            var roles = _userManager.GetRolesAsync(user).Result.ToList();
            var roleStr = "";
            foreach (var role in roles)
            {
                roleStr += role + ',';
            }
            var claims = new List<Claim>
            {
                new Claim("username", user.UserName),
            };
            context.IssuedClaims.InsertRange(0, claims);


            //>Return
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = _userManager.GetUserAsync(context.Subject).Result;

            context.IsActive = (user != null) && user.IsActive;

            //>Return
            return Task.FromResult(0);
        }
    }
}