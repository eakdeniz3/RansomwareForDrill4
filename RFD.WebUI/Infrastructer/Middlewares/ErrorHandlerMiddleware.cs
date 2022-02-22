using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using RFD.Entities.Common.Model;
using RFD.Infrastructer.Extentions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RFD.WebUI.Infrastructer.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;
        public ErrorHandlerMiddleware(RequestDelegate next
            //ILogger<ErrorHandlerMiddleware> logger
            )
        {
            _next = next;
            //_logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                context.Request.Headers.TryGetValue("Accept", out StringValues type);
               // _logger.LogError(error.Message);
                var response = context.Response;
                switch (error)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = 401;
                        break;
                    default:
                        response.StatusCode = 400;
                        break;
                }
                var responseModel = ApiResponse<string>.Fail(error.Message);
                if (type == "application/xml")
                {
                    response.ContentType = "application/xml";
                    var result = XmlConverter.Serialize<ApiResponse<string>>(responseModel);
                    await response.WriteAsync(result);
                }
                else
                {
                    response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(responseModel);
                    await response.WriteAsync(result);
                }








                //response.StatusCode = StatusCodes.Status400BadRequest;


            }
        }
    }
}
