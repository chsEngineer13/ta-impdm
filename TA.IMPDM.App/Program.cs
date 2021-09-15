using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TA.IMPDM.Service;

namespace TA.IMPDM.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cts = new TimedHostedServiceCancellationTokenSource();
            var host = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables("DOTNET_");
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var env = context.HostingEnvironment;
                    builder.SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices((c, s) => ConfigureServices(c, s, cts));

            try
            {
                await host.RunConsoleAsync(cts.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
            loggingBuilder.AddConsole();
        }

        private static void ConfigureServices(HostBuilderContext context, 
            IServiceCollection services, 
            TimedHostedServiceCancellationTokenSource hostedServiceCancellationToken)
        {
            ServiceConfiguration.ConfigureServices(context, services, hostedServiceCancellationToken);
        }
    }
}
