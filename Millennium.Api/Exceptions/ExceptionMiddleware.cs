using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Millennium.Domain.DTO;
using Millennium.Domain.Exceptions;
using Newtonsoft.Json;

namespace Millennium.Api.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is DomainException)
            {
                var body = new ExceptionDto((int)HttpStatusCode.BadRequest, exception.GetType().Name, exception.Message);
                
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return context.Response.WriteAsync(JsonConvert.SerializeObject(body));
            }
            else
            {
                var body = new ExceptionDto((int)HttpStatusCode.InternalServerError, exception.GetType().Name, exception.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync(JsonConvert.SerializeObject(body));
            }
        }
    }
}