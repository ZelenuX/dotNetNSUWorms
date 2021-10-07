using System;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Utils.Generators;

namespace dotNetWorms
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<WorldService>();

                    services.AddSingleton<World.World>();
                    services.AddSingleton<ICoordsGenerator>(new NormalCoordsGenerator(0, 5));
                    services.AddSingleton<UniqueNamesGenerator>(new UniqueNamesGenerator("BobTheWorm_"));
                    services.AddSingleton<WormStrategyProviderService>();
                    services.AddSingleton<ReportWriterService>(new ReportWriterService("./out.txt"));
                });
        }
    }
}
