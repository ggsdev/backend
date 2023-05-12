using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace PRIO.Middlewares
{
    public class UnauthorizedCaptureMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedCaptureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authenticateResult = await context.AuthenticateAsync();

            if (authenticateResult.Succeeded)
            {
                var claimsIdentity = authenticateResult?.Principal?.Identity as ClaimsIdentity;
                if (claimsIdentity is not null)
                {
                    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    context.Items["Id"] = userId;
                }

            }
            await _next(context);

            if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
            {
                authenticateResult = await context.AuthenticateAsync();
                string? message;

                if (authenticateResult.Failure is null)
                {
                    message = "Token is missing";
                }
                else if (authenticateResult.Failure.GetType() == typeof(Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException))
                {
                    message = "Token has already expired";
                }
                else
                {
                    message = "Invalid JWT format for token";
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync($"{{\"message\": \"{message}\"}}");
            }
        }

    }
}
