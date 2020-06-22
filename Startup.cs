using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using XmlParser.DataReader;
using XmlParser.Factories;
using XmlParser.Models;
using XmlParser.Parsers;
using XmlParser.Repositories;
using XmlParser.Settings;

namespace XmlParser
{
    class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(GetEnvironmentConfig())
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.Configure<AppSettings>(x => Configuration.GetSection("AppSettings").Bind(x));
            services.AddScoped<DataReader.IDataReader, FileReader>();
            services.AddScoped<IParserFactory, ParserFactory>();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetSection("AppSettings")["DbConnection"]));
        }

        private string GetEnvironmentConfig()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return $"appsettings{(string.IsNullOrEmpty(environment) ? "" : $".{environment}")}.json";
        }
    }
}
