using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Demo.AspNetCore;
using Demo.Helpers.Extensions;
using Demo.IdentityServer4;
using Demo.IdentityServer4.EntityFrameworkCore;
using Demo.Storage.EntityFrameworkCore;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using Microsoft.EntityFrameworkCore;
using Demo.Validation.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Model;
using System.Collections;
using IdentityServer4.Services;
using WebApi.Controllers.map;
using WebApi.Controllers.user;
using WebApi.Core.Configuration;

namespace WebApi
{
    public class Startup
    {
        private static string _defaultCorsPolicyName = "http://localhost:4200";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDomain(options =>
            {
                options.DefaultNameOrConnectionString = Configuration.GetConnectionString("Default");
                options.BackgroundJobs.IsJobExecutionEnabled = false;
                // Configure storage
                options.Storage.UseEntityFrameworkCore(c =>
                {
                    c.AddDbContext<DemoContext>(config =>
                            config.DbContextOptions.UseSqlServer(Configuration.GetConnectionString("Default")));
                });

                // Configure validation
                options.Validation.UseDataAnnotations();
            });

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                .AddAppPersistedGrants<IPersistedGrantDbContext>()
                .AddAppIdentityServer();

            ConfigureAuthService(services);
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //}).AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = Configuration.GetSection("IdentityServer")["Authority"];
            //    options.RequireHttpsMetadata = Configuration.GetSection("IdentityServer")["RequireHttpsMetadata"].To<bool>();

            //    options.ApiName = "client";
            //    options.ApiSecret = "secret";
            //});
        }


        private void ConfigureAuthService(IServiceCollection services)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var identityUrl = Configuration["IdentityServer:Authority"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // my API name as defined in Config.cs - new ApiResource... or in DB ApiResources table
                // options.Audience = "";
                // URL of Auth server(API and Auth are separate projects/applications),
                // so for local testing this is http://localhost:5000 if you followed ID4 tutorials
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    // Scopes supported by API as defined in Config.cs - new ApiResource... or in DB ApiScopes table
                    ValidAudiences = new List<string> {
                        "default-api",
                        "openid",
                        "profile",
                        "offline-access"
                    },
                    ValidateIssuer = true
                };
            });

            // Configure CORS for angular5 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            Configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePreFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                )
            );
            services.AddTransient<UserController>();
            services.AddTransient<MapsController>();
            services.AddTransient<RoadsController>();
            services.AddTransient<GoogleRoadIconsController>();
            //services.AddTransient<IProfileService, ProjectProfileService>();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            // app.UseAuthentication(); // not needed, since UseIdentityServer adds the authentication middleware
            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
