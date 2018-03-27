using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Demo.Security;
using Demo.Security.Permissions;
using Demo.Storage.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.Core.Authorization;
using WebApi.Core.map;
using Polly;

namespace WebApi.Model
{
    class DemoContextSeed : IDbContextSeed
    {
        private readonly ILogger<DemoContextSeed> _logger;
        private readonly DemoContext _ctx;
        public DemoContextSeed(ILogger<DemoContextSeed> logger, DemoContext context)
        {
            _logger = logger;
            _ctx = context;
        }

        public Type ContextType => typeof(DemoContext);
        public async Task SeedAsync()
        {
            var policy = CreatePolicy(nameof(DemoContext));

            await policy.ExecuteAsync(async () =>
            {
                await AddUser();
            });
        }

        private async Task AddUser()
        {
            using (_ctx)
            {
                _ctx.Database.Migrate();
                if (!_ctx.Users.Any(x => x.Email == "admin@demo.com"))
                {
                    // Add 'administrator' role
                    var adminRole = await _ctx.Roles.FirstOrDefaultAsync(r => r.RoleName == "Administrator");
                    if (adminRole == null)
                    {
                        adminRole = new Role
                        {
                            RoleName = "Administrator",
                            NormalizedRoleName = "ADMINISTRATOR"
                        };
                        _ctx.Roles.Add(adminRole);
                        await _ctx.SaveChangesAsync();
                    }

                    // Create admin user
                    var adminUser = _ctx.Users.FirstOrDefault(u => u.UserName == "admin");
                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            UserName = "admin",
                            NormalizedUserName = "ADMIN",
                            Name = "admin",
                            Surname = "admin",
                            Email = "admin@demo.com",
                            NormalizedEmail = "ADMIN@DEMO.COM",
                            IsActive = true,
                            EmailConfirmed = true,
                            PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                        };

                        _ctx.Users.Add(adminUser);

                        _ctx.SaveChanges();

                        _ctx.UserRoles.Add(new UserRole(adminUser.Id, adminRole.Id));

                        _ctx.SaveChanges();

                       
                    }
                }

                await AddPermision(_ctx);

                AddMapType(_ctx);
            }
        }

        private async Task AddPermision(DemoContext _ctx)
        {
                var adminRole = await _ctx.Roles.FirstOrDefaultAsync(r => r.RoleName == "Administrator");
                if (adminRole != null)
                {
                    var mangementPermissions = new Permissions();
                    var permissions = mangementPermissions.GetPermissions();
                    foreach (var permission in permissions)
                    {
                        if (!_ctx.Permission.Any(x => x.Name == permission.Name))
                        {
                            var newPermission = new Permission
                            {
                                Name = permission.Name,
                                Category = permission.Category,
                                Description = permission.Description,
                                DisplayName = permission.DisplayName
                            };

                            _ctx.Permission.Add(newPermission);

                            _ctx.SaveChanges();

                            _ctx.PermissionRoles.Add(new PermissionRole { PermissionId = newPermission.Id, RoleId = adminRole.Id });

                            _ctx.SaveChanges();
                        }
                    }
                
            }
        }

        private void AddMapType(DemoContext _ctx)
        {
                var mapTypeMange = new mapTypes();
                var mapTypes = mapTypeMange.GetMapTypes();
                foreach (var type in mapTypes)
                {
                    var mapType = _ctx.MapTypes.SingleOrDefault(x => x.Id == type.Id);
                    if (mapType==null)
                    {
                        var newType = new MapType
                        {
                            Id = type.Id,
                            Name = type.Name,
                            Value = type.Value
                        };
                        _ctx.MapTypes.Add(newType);
                        _ctx.SaveChanges();
                    }
                }
            
        }

        private Policy CreatePolicy(string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        _logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}

