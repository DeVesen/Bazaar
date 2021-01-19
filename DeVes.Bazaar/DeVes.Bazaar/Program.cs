using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.IO;

namespace DeVes.Bazaar
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var contentRoot = Directory.GetCurrentDirectory();
            var configuration = GetConfiguration(contentRoot);

            InitializeTraceListeners(configuration);

            CreateHostBuilder(contentRoot, args)
                .Build()
                .Run();
        }


        private static IHostBuilder CreateHostBuilder(string contentRoot, string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                        .UseContentRoot(contentRoot)
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                });
        }

        private static IConfiguration GetConfiguration(string basePath)
        {
            return new ConfigurationBuilder().SetBasePath(basePath)
                .AddEnvironmentVariables()
                .Build();
        }

        private static void InitializeTraceListeners(IConfiguration configuration)
        {
            var cfgDefaultValue    = configuration.GetSection("Logging")
                                         ?.GetSection("LogLevel")
                                         ?.GetValue<string>("Default") ?? "Information";
            var cfgConsoleValue    = configuration.GetSection("Logging")
                                         ?.GetSection("LogLevel")
                                         ?.GetValue<string>("Console") ?? cfgDefaultValue;

            if (Enum.TryParse(cfgConsoleValue, out SourceLevels sourceTraceLevelConsole) && sourceTraceLevelConsole != SourceLevels.Off)
            {
                Trace.Listeners.Add(new TraceListener.ConsoleTraceListener { Filter = new EventTypeFilter(sourceTraceLevelConsole) });
            }
        }
    }
}
