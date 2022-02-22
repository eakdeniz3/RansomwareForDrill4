using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RFD.Entities.Common.Model;
using RFD.Infrastructer.Extentions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RFD.Server.Infrastructer.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
       // private readonly ILogger _logger;
        public ErrorHandlerMiddleware(RequestDelegate next
           // ILogger logger
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
               // _logger.LogError(error.Message);
                var response = context.Response;
                response.ContentType = "application/xml";

                switch (error)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = 401;
                        break;
                    default:
                        response.StatusCode = 400;
                        break;
                }



                //response.StatusCode = StatusCodes.Status400BadRequest;
                var responseModel = ApiResponse<string>.Fail(error.Message);
               
                var result = XmlConverter.Serialize<ApiResponse<string>>(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
