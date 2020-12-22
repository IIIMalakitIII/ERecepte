using EReceipt.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EReceipt.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case BusinessLogicException ex:
                    await WriteErrorAsync(context, ex.Message, HttpStatusCode.BadRequest);
                    _logger.LogError(exception, ex.Message);
                    break;

                default:
                    await WriteErrorAsync(context, exception.InnerException?.Message ?? exception.Message, HttpStatusCode.InternalServerError);
                    _logger.LogError(exception, exception.Message);
                    break;
            }

        }

        private static async Task WriteErrorAsync(HttpContext context, string error, HttpStatusCode statusCode)
        {
            var result = JsonConvert.SerializeObject(new { error });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(result);
        }
    }
}
