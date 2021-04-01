using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swapi.Models;

namespace Swapi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.Message}");
                await HandleGlobalExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            var errorDetail = new ErrorDetail();
            if (exception is HttpException httpException)
            {
                errorDetail.StatusCode = httpException.StatusCode;
                errorDetail.ErrorMessage = httpException.Message;
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var json = JsonConvert.SerializeObject(errorDetail);
            await context.Response.WriteAsync(json);
        }
    }
}