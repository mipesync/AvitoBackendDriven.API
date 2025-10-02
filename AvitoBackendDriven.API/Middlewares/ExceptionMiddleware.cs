using System.Net;
using AvitoBackendDriven.Domain.Constants;
using AvitoBackendDriven.Domain.Exceptions;
using AvitoBackendDriven.Domain.Models;
using Newtonsoft.Json;

namespace AvitoBackendDriven.API.Middlewares;

/// <summary>
/// Middleware для обработки исключений
/// </summary>
public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
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
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            var message = exception.Message;

            var statusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                BadRequestException => (int)HttpStatusCode.BadRequest,
                InternalException => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var errors = !message.Contains(StringConstants.Separator) ? [message] : message.Split(StringConstants.Separator).ToList(); 
            
            var result = JsonConvert.SerializeObject(new ErrorModel
            {
                StatusCode = statusCode,
                Errors = errors,
                ErrorsCount = errors.Count
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            Console.WriteLine($"Message: {exception.Message}\nInnerMessage: {exception.InnerException}");
            
            return context.Response.WriteAsync(result);
        }
    }