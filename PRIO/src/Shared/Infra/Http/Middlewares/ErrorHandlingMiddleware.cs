﻿using Newtonsoft.Json;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Shared.Infra.Http.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Dictionary<Type, int> exceptionStatusCodes = new()
        {
            { typeof(NotFoundException), 404 },
            { typeof(BadRequestException), 400 },
            { typeof(ConflictException), 409 },
        };
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (exceptionStatusCodes.TryGetValue(ex.GetType(), out int statusCode))
                {
                    await HandleExceptionAsync(context, ex, statusCode, ex.Message);
                }
                else
                {
                    Console.WriteLine(ex);
                    await HandleExceptionAsync(context, ex, 500, "Internal Server Error");
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode, string errorMessage)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            if (ex is BadRequestException badRequestException && badRequestException.Errors != null)
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new XlsErrorImportDTO { Message = errorMessage, Errors = badRequestException.Errors }));
            }
            else
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponseDTO { Message = errorMessage }));
            }
        }

    }
}