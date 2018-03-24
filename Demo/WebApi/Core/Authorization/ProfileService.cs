﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Demo.Security;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebApi.Model;
namespace WebApi.Core.Authorization
{
    public class ProfileService: ProfileService<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DemoContext _context;

        public ProfileService(
            UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            RoleManager<Role> roleManager, DemoContext context)
            : base(userManager, claimsFactory)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);

            var user = await _userManager.GetUserAsync(context.Subject);

            var roleNames = await _userManager.GetRolesAsync(user);
            if (roleNames.IsNullOrEmpty()) return;


            foreach (var roleName in roleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null) continue;
                var permissions = _context.PermissionRoles.Where(p => p.RoleId == role.Id)
                    .Include(p => p.Permission).Select(p => p.Permission).Select(p=>p.Name);
                var roleClaims = new List<Claim>
                {
                    new Claim("Roles", roleName)
                };
                foreach (var permission in permissions)
                    roleClaims.Add(new Claim("Permission", permission));
                context.IssuedClaims.AddRange(roleClaims);
            }
        }
    }
}