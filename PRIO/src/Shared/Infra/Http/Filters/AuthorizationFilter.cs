using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Shared.Infra.Http.Filters
{
    public class AuthorizationFilter : IAsyncActionFilter
    {
        private readonly DataContext _context;

        public AuthorizationFilter(DataContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.Items["Id"] as Guid?;

            if (userId is not null)
            {
                var user = await _context.Users
                    .Include(x => x.Group)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user is null)
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorResponseDTO
                    {
                        Message = "User not identified, please login first"
                    });

                    return;
                }

                context.HttpContext.Items["User"] = user;
            }

            await next();
        }
    }
}
