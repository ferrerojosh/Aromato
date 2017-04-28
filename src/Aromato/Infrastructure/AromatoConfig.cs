using System.IO;
using Microsoft.Extensions.Configuration;

namespace Aromato.Infrastructure
{
    public class AromatoConfig
    {
        private readonly IConfigurationRoot _configuration;

        public static AromatoConfig Instance { get; } = new AromatoConfig();

        private AromatoConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public string this[string index] => _configuration[index];
    }
}