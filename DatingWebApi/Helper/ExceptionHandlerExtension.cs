using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebApi.Helper
{
    public static class ExceptionHandlerExtension
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("ApplicationError", message);
            response.Headers.Add("Access-Control-Expose-Headers", "ApplicationError");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
