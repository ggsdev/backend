using Newtonsoft.Json;
using PRIO.DTOS;

namespace PRIO
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
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse { Message = "Internal Server Error" }));
            }
        }
    }
}