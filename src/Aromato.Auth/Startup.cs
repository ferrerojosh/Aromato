using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using Aromato.Infrastructure.PostgreSQL;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NpgsqlTypes;
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

            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {"message", new RenderedMessageColumnWriter() },
                {"message_template", new MessageTemplateColumnWriter() },
                {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                {"raise_date", new TimeStampColumnWriter() },
                {"exception", new ExceptionColumnWriter() },
                {"properties", new PropertiesColumnWriter(NpgsqlDbType.Json) },
                {"machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.Raw) }
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.PostgreSQL(connectionStr, logTable, columnWriters)
                .WriteTo.LiterateConsole()
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(Console.Error);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

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
                options.AllowRefreshTokenFlow();

                // Register a new ephemeral key, that is discarded when the application
                // shuts down. Tokens signed using this key are automatically invalidated.
                // This method should only be used during development.
                options.AddEphemeralSigningKey();

                // On production, using a X.509 certificate stored in the machine store is recommended.
                // You can generate a self-signed certificate using Pluralsight's self-cert utility:
                // https://s3.amazonaws.com/pluralsight-free/keith-brown/samples/SelfCert.zip
                //
                // options.AddSigningCertificate("8AEDD04D044891326BA935EA65D9819B0E887E8F");
                //
                // Alternatively, you can also store the certificate as an embedded .pfx resource
                // directly in this assembly or in a file published alongside this project:
                //
                //options.AddSigningCertificate(
                //   assembly: typeof(Startup).GetTypeInfo().Assembly,
                //   resource: "aromato.dev.pfx",
                //   password: "developer");

                options.UseJsonWebTokens();

                // During development, you can disable the HTTPS requirement.
                options.DisableHttpsRequirement();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://192.168.143.180:4200", "http://localhost:4200");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

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

                // Map validation parameters to OpenId standard
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = OpenIdConnectConstants.Claims.Name,
                    RoleClaimType = OpenIdConnectConstants.Claims.Role
                }
            });

            // Register the OpenIddict middleware.
            app.UseOpenIddict();
            app.UseMvcWithDefaultRoute();

            InitializeAsync(app, CancellationToken.None).GetAwaiter().GetResult();
        }

        private async Task InitializeAsync(IApplicationBuilder app, CancellationToken cancellationToken)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DbContext>();
                await context.Database.EnsureCreatedAsync();

                // Note: when using a custom entity or a custom key type, replace OpenIddictApplication by the appropriate type.
                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

                if (await manager.FindByClientIdAsync("aromato-web", cancellationToken) == null)
                {
                    var application = new OpenIddictApplication
                    {
                        ClientId = "aromato-web",
                        RedirectUri = "http://localhost:4200/",
                        LogoutRedirectUri = "http://localhost:4200/"
                    };

                    await manager.CreateAsync(application, cancellationToken);
                }
            }
        }
    }
}
