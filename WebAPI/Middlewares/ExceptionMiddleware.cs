using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI.Errors;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env)
        {
            this.env = env;
            this.logger = logger;
            this.next = next;

        }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            ApiError response;
            HttpStatusCode statuscode = HttpStatusCode.InternalServerError;
            String message;
            var exceptionType = ex.GetType();
            if(exceptionType ==  typeof(UnauthorizedAccessException))
            {
                statuscode = HttpStatusCode.Forbidden;
                message = "You are not authorized";
            }
            else
            {
                statuscode = HttpStatusCode.InternalServerError;
                message = "Some unknown error occurred";    
            }

            if(env.IsDevelopment()){
                response = new ApiError((int)statuscode, ex.Message, ex.StackTrace.ToString());
            }else
            {
                response = new ApiError((int)statuscode, message);
            }
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)statuscode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response.ToString());
        }
    }
}
}