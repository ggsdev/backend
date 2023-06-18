using Newtonsoft.Json;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Exceptions;

namespace PRIO.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //try
            //{
            //    await _next(context);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    context.Response.StatusCode = 500;
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponseDTO { Message = "Internal Server Error" }));
            //}

            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(context, ex);
            }
            catch (BadRequestException ex)
            {
                await HandleBadRequestExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleInternalServerErrorAsync(context, ex);
            }
        }

        private async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException ex)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponseDTO { Message = ex.Message }));
        }

        private async Task HandleBadRequestExceptionAsync(HttpContext context, BadRequestException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponseDTO { Message = ex.Message }));
        }

        private async Task HandleInternalServerErrorAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponseDTO { Message = "Internal Server Error" }));
        }
    }
}