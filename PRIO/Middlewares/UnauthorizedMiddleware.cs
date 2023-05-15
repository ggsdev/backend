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

            if (authenticateResult.Succeeded && authenticateResult.Principal?.Identity is ClaimsIdentity claimsIdentity)
            {
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                context.Items["Id"] = userId;
            }

            await _next(context);

            if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
            {
                authenticateResult = await context.AuthenticateAsync();
                string? message;

                switch (authenticateResult.Failure)
                {
                    case null:
                        message = "Token is missing";
                        break;
                    case Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException:
                        message = "Token has already expired";
                        break;
                    default:
                        message = "Invalid JWT format for token";
                        break;
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync($"{{\"message\": \"{message}\"}}");

            }
        }

    }
}
