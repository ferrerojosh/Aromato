using System.Collections.Generic;
using Aromato.Application;
using Aromato.Application.Web;
using Aromato.Domain.Employee;
using Aromato.Domain.Inventory;
using Aromato.Infrastructure;
using Aromato.Infrastructure.Events;
using Aromato.Infrastructure.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            var scopes = new List<string>()
            {
                "employee.read", "employee.write",
                "inventory.read", "inventory.write",
            };

            services.AddAuthorization(options =>
            {
                scopes.ForEach(scope => options.AddPolicy(scope, policy => policy.RequireClaim("scope", scope)));
            });

            services.AddDbContext<PostgresUnitOfWork>();

            services.AddScoped<IEmployeeService, EmployeeWebService>();
            services.AddScoped<IInventoryService, InventoryWebService>();

            services.AddScoped<IEmployeeRepository, PostgresEmployeeRepository>();
            services.AddScoped<IInventoryRepository, PostgresInventoryRepository>();

            services.AddMvc();
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

            // Ensure any buffered events are sent at shutdown
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,
                ApiName = "aromato",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseAromato(loggerFactory, new AutoFacEventDispatcher());

            app.UseMvc();
        }
    }
}
