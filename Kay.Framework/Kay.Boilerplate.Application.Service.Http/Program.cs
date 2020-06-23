using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Kay.Boilerplate.Application.Service.Http
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args);
            var hostUrls = GetHostUrls();
            if (hostUrls != null && hostUrls.Any())
            {
                webHostBuilder = webHostBuilder.UseUrls(hostUrls);
            }

            webHostBuilder = webHostBuilder.UseStartup<Startup>();
            return webHostBuilder;
        }

        private static string[] GetHostUrls()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("host.json").Build();
            var hostUrls = builder.GetSection("HostUrls").Value;
            if (string.IsNullOrWhiteSpace(hostUrls))
            {
                return new string[0];
            }

            var urls = hostUrls.Replace(";", ",")
                .Split(",").Distinct()
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .ToArray();
            return urls;
        }
    }
}
