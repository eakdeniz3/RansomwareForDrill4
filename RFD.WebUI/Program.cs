
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RFD.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();        
            ILogger logger = host.Services.GetService<ILogger<Program>>();
            var config = host.Services.GetService<IConfiguration>();
            var trackerUrl = "Tracker Image Url:" + config.GetValue<string>("Kestrel:Endpoints:Tracker:Url") + "/images/tracker.jpg";
            var webUIUrl = "Web UI Url:" + config.GetValue<string>("Kestrel:Endpoints:WebUI:Url") + "?ApiKey=" + config.GetValue<string>("ApiKey");

            logger.LogInformation(trackerUrl);
            logger.LogInformation(webUIUrl);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              .ConfigureLogging(logging =>
              {
                  logging.ClearProviders();
                  logging.AddConsole();
              })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
                });
    }


}
