using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RFD.WebUI.Infrastructer.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Middlewares
{
    public class RouteControlMiddleware
    {
        private readonly RequestDelegate _next;
        private IConfiguration _configuration;
        private readonly ILogger _logger;

        public RouteControlMiddleware(RequestDelegate next,
            IConfiguration configuration,
            IRFDStarterExtension starterExtension, ILogger<TrackerMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            //var serverUrl = _configuration.GetValue<string>("Kestrel:Endpoints:Server:Url");
            //var webUIUrl = _configuration.GetValue<string>("Kestrel:Endpoints:WebUI:Url"); ;
            //var trackerUrl = _configuration.GetValue<string>("Kestrel:Endpoints:Tracker:Url"); ;

            //if (httpContext.Request.Path.Equals("/images/tracker.jpg") && !httpContext.Request.Host.Equals(trackerUrl))
            //{
            //    throw new Exception("Belirtilen yol bulunamadı.");
            //}

            //else if (httpContext.Request.Path.Equals("api") && !httpContext.Request.Host.Equals(serverUrl))
            //{
            //    throw new Exception("Belirtilen yol bulunamadı.");
            //}


            //    var controllerActionDescriptor = httpContext
            //.GetEndpoint()?
            //.Metadata?
            //.GetMetadata<ControllerActionDescriptor>();

            //    var controllerName = controllerActionDescriptor?.ControllerName;
            //    var actionName = controllerActionDescriptor?.ActionName;




            
            await _next.Invoke(httpContext);
        }
    }


}
