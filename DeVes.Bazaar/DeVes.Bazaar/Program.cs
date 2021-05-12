using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace DeVes.Bazaar
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var contentRoot   = Directory.GetCurrentDirectory();

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
    }
}