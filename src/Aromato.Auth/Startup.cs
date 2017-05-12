using System;
using System.Threading;
using System.Threading.Tasks;
using Aromato.Infrastructure.PostgreSQL;
using CryptoHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Core;
using OpenIddict.Models;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace Aromato.Auth
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
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<PostgresUnitOfWork>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Aromato"));
            });

            services.AddDbContext<DbContext>(options =>
            {
                // Configure the context to use an in-memory store.
                options.UseInMemoryDatabase();
                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            services.AddOpenIddict(options =>
            {
                // Register the Entity Framework stores.
                options.AddEntityFrameworkCoreStores<DbContext>();
                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                options.AddMvcBinders();

                // Enable the authorization, logout, userinfo, and introspection endpoints.
                options.EnableAuthorizationEndpoint("/connect/authorize")
                    .EnableLogoutEndpoint("/connect/logout")
                    .EnableTokenEndpoint("/connect/token")
                    .EnableUserinfoEndpoint("/api/userinfo");

                // Enable the password flow.
                options.AllowPasswordFlow();

                // Register a new ephemeral key, that is discarded when the application
                // shuts down. Tokens signed using this key are automatically invalidated.
                // This method should only be used during development.
                options.AddEphemeralSigningKey();

                // On production, using a X.509 certificate stored in the machine store is recommended.
                // You can generate a self-signed certificate using Pluralsight's self-cert utility:
                // https://s3.amazonaws.com/pluralsight-free/keith-brown/samples/SelfCert.zip
                //
                // options.AddSigningCertificate("7D2A741FE34CC2C7369237A5F2078988E17A6A75");
                //
                // Alternatively, you can also store the certificate as an embedded .pfx resource
                // directly in this assembly or in a file published alongside this project:
                //
                // options.AddSigningCertificate(
                //     assembly: typeof(Startup).GetTypeInfo().Assembly,
                //     resource: "AuthorizationServer.Certificate.pfx",
                //     password: "OpenIddict");
                options.UseJsonWebTokens();

                // During development, you can disable the HTTPS requirement.
                options.DisableHttpsRequirement();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            // Register the OpenIddict middleware.
            app.UseOpenIddict();
            app.UseMvcWithDefaultRoute();
        }
    }
}
