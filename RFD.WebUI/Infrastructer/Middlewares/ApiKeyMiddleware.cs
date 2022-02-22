using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RFD.Entities.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private IConfiguration _configuration;
       // private ILogger _logger;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration
           // , ILogger<ApiKeyMiddleware> logger/
           )
        {
            _next = next;
            _configuration = configuration;
            //_logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var apiKey = _configuration.GetValue<string>("ApiKey");

            var isApiKey = httpContext.Request.Headers.TryGetValue("ApiKey", out var headerValue);


            var queryKey = httpContext.Request.Query.SelectMany(x => x.Value, (col,value) => new KeyValuePair<string, string>(col.Key, value)).ToList();

          


            if (apiKey.Equals(headerValue) || queryKey.Any(x=>x.Key=="ApiKey" && x.Value==apiKey))
            {
 await _next.Invoke(httpContext);
               
                //httpContext.Response.StatusCode = 401;

                //var responseModel = ApiResponse<string>.Fail("");

                //var result = JsonSerializer.Serialize(responseModel);
                //await httpContext.Response.WriteAsync(result);
                //return;
            }
 string message = $"{httpContext.Connection.RemoteIpAddress} ten geçersiz api anahtarı ile sahip istek yapıldı.";
               // _logger.LogWarning(message);
                throw new KeyNotFoundException("Geçerli Api anahtarına sahip değilsiniz.");
           
        }

    }
}
