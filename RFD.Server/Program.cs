using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.DataAccess.EntityFramework;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RFD.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
          

            var host = CreateHostBuilder(args).Build();
            host.Services.InitializeDb();
            await host.Services.SeedDbAsync();
            //ILogger logger = host.Services.GetService<ILogger<Program>>();
            //var config = host.Services.GetService<IConfiguration>();
            //var trackerUrl = "Tracker Image Url:" + config.GetValue<string>("Kestrel:Endpoints:Tracker:Url") + "/images/tracker.jpg";
            //var webUIUrl = "Web UI Url:" + config.GetValue<string>("Kestrel:Endpoints:WebUI:Url") + "?ApiKey=" + config.GetValue<string>("ApiKey");

            //logger.LogInformation(trackerUrl);
            //logger.LogInformation(webUIUrl);
            host.Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.ConfigureLogging(logging =>
            //{
            //    logging.AddConsole();

            //})

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();

                });
    }


    public interface IDbInitializer
    {
        void Initialize();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<RFDContext>())
                {
                    context.Database.Migrate();

                    context.Database.EnsureCreated();
                }
            }
        }
    }

    public static class DbContextOptionsExtensions
    {
        public static void InitializeDb(this IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitialize.Initialize();
            }
        }
        public static async Task SeedDbAsync(this IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                await context.UserService.CreateUserAsync(new Entities.DTO.User()
                {
                    Username = "SbrArge",


                }, "SbrArge2022!");
                await context.Complete();
            }
        }
    }
}

