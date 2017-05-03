using Aromato.Application;
using Aromato.Application.Web;
using Aromato.Domain.Employee;
using Aromato.Domain.Inventory;
using Aromato.Infrastructure.Crosscutting;
using Aromato.Infrastructure.Crosscutting.AutoMapper;
using Aromato.Infrastructure.Crosscutting.Extension;
using Aromato.Infrastructure.PostgreSQL;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Aromato.WebApi
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

            env.ConfigureNLog("nlog.config");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PostgresUnitOfWork>();
            services.AddAutoMapper();
            services.UseAutoMapperTypeAdapter();

            // Add framework services.
            services.AddMvc(options =>
            {
            });

            // Register services.
            services.AddScoped<IEmployeeRepository, PostgresEmployeeRepository>();
            services.AddScoped<IInventoryRepository, PostgresInventoryRepository>();

            services.AddScoped<IInventoryService, InventoryWebService>();
            services.AddScoped<IEmployeeService, EmployeeWebService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddNLog();
            loggerFactory.AddDebug();

            app.UseMvc();
            app.AddNLogWeb();
        }

    }
}
