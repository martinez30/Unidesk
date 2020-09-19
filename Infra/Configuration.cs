using Microsoft.Extensions.Configuration;
using System;

namespace Infra
{
    public static class Configuration
    {
        public static IConfigurationRoot _configuration;
        public static string ConnectionString { get; set; }

        public static void Build(string path)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var build = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = build.Build();
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

    }
}
