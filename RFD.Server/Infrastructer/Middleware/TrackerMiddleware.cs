using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.DTO;
using RFD.Server.Controllers;
using RFD.Server.Infrastructer.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RFD.Server.Infrastructer.Middleware
{
    public class TrackerMiddleware
    {
        private readonly RequestDelegate _next;
        private IConfiguration _configuration;
        private IRFDStarterExtension _starterExtension;
       // private readonly ILogger _logger;

        public TrackerMiddleware(RequestDelegate next,
            IConfiguration configuration,
            IRFDStarterExtension starterExtension
            //, ILogger logger
            )
        {
            _next = next;
            _configuration = configuration;
            _starterExtension = starterExtension;
            //_logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            if (httpContext.Request.Path.Equals("/images/tracker.jpg"))
            {
                try
                {
                    IPAddress ipAddress = httpContext.Connection.RemoteIpAddress;
                    IPHostEntry getIpHost = Dns.GetHostEntry(ipAddress);
                    string computerName = getIpHost?.HostName?.Split('.')[0];
                    if (!string.IsNullOrEmpty(computerName))
                    {
                       Task.Run(()=> _starterExtension.Start(computerName, Entities.Enum.ApplicationType.Phising));                     
                    }
                }
                catch (Exception ex)
                {
                  //  _logger.LogError(ex.Message);
                }
            }
            await _next.Invoke(httpContext);
        }
    }

    public static class Tracker
    {
        public static IApplicationBuilder UseTracker(this IApplicationBuilder app)
        {
            app.UseMiddleware<TrackerMiddleware>();
            return app;
        }
    }
}
