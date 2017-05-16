using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Aromato.Application;
using Aromato.Application.Web;
using Aromato.Domain.EmployeeAgg;
using Aromato.Domain.InventoryAgg;
using Aromato.Domain.RoleAgg;
using Aromato.Infrastructure;
using Aromato.Infrastructure.Events;
using Aromato.Infrastructure.PostgreSQL;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Core;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace Aromato.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            var connectionStr = Configuration.GetConnectionString("Aromato");
            var logTable = Configuration["LogsTable"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.PostgreSqlServer(connectionStr, logTable)
                .WriteTo.LiterateConsole()
                .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PostgresUnitOfWork>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Aromato"));
            });

            services.AddScoped<IEmployeeService, EmployeeWebService>();
            services.AddScoped<IInventoryService, InventoryWebService>();
            services.AddScoped<IRoleService, RoleWebService>();

            services.AddScoped<IEmployeeRepository, PostgresEmployeeRepository>();
            services.AddScoped<IInventoryRepository, PostgresInventoryRepository>();
            services.AddScoped<IRoleRepository, PostgresRoleRepository>();

            InitializePolicies(services);

            services.AddMvc();
        }

        private void InitializePolicies(IServiceCollection services)
        {
            var scopeFactory = services
                .BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>();

            services.AddAuthorization(options =>
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var provider = scope.ServiceProvider;
                    using (var dbContext = provider.GetRequiredService<PostgresUnitOfWork>())
                    {
                        var permissions = dbContext.Permissions
                            .Include("Roles.Role")
                            .AsEnumerable();

                        foreach(var permission in permissions)
                        {
                            options.AddPolicy(permission.Name, policy =>
                            {
                                policy.RequireClaim(OpenIdConnectConstants.Claims.Scope, permission.Name);
                            });
                        }

                        // Add openid scopes
                        options.AddPolicy(OpenIddictConstants.Scopes.Roles, policy =>
                        {
                            policy.RequireClaim(OpenIdConnectConstants.Claims.Scope, OpenIddictConstants.Scopes.Roles);
                        });
                    }
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            // Use serilog as logging implementation
            loggerFactory.AddSerilog();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            // Ensure any buffered events are sent at shutdown
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            // Disable the automatic JWT -> WS-Federation claims mapping used by the JWT middleware:
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            // Authenticate users on a separate server
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Audience = "aromato",
                Authority = "http://localhost:5000/",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = OpenIdConnectConstants.Claims.Name,
                    RoleClaimType = OpenIdConnectConstants.Claims.Role
                }
            });

            app.UseAromato(loggerFactory, new AutoFacEventDispatcher());

            app.UseMvc();
        }
    }
}
