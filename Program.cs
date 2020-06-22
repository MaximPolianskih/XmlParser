using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using XmlParser.Factories;
using XmlParser.Models;

namespace XmlParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();
            var factory = serviceProvider.GetService<IParserFactory>();
            var sw = new Stopwatch();
            sw.Start();
            await factory.GetParser(typeof(LicenseBroadcasting)).RunAsync();
            sw.Stop();
            Console.WriteLine($"Время работы: {sw.ElapsedMilliseconds} ms.");
        }

        public static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            var startup = new Startup();

            startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
