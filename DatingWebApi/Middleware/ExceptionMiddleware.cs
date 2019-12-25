using DatingWebApi.Helper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DatingWebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            //_logger = factory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                if (context.Response.HasStarted)
                {
                   //_logger.LogInformation("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //var error = context.Features.Get<IExceptionHandlerFeature>();                
                if(ex != null)
                {
                    context.Response.AddApplicationError(ex.Message);
                    await context.Response.WriteAsync(ex.Message);                    
                    return;
                }
            }
        }
    }
}
