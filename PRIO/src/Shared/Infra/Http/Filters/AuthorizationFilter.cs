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

            var requestMethod = context.HttpContext.Request.Method;
            //var clientUrl = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString}";
            //var frontendUrl = context.HttpContext.Request.Headers["Custom"];
            var frontendUrl2 = context.HttpContext.Request.Headers["User-Agent"];


            var frontendUrl = context.HttpContext.Request.Headers.Location;

            Console.WriteLine(frontendUrl2);
            foreach (var item in frontendUrl2)
            {
            }


            if (userId is not null)
            {
                var user = await _context.Users
                    .Include(x => x.UserPermissions)
                    .ThenInclude(x => x.UserOperation)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user is null)
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorResponseDTO
                    {
                        Message = "User not identified, please login first"
                    });

                    return;

                }

                //foreach (var permission in user.UserPermissions)
                //{
                //    //Console.WriteLine("request" + requestMethod);

                //    foreach (var ope in permission.UserOperation)
                //    {
                //        //Console.WriteLine("operation" + ope.OperationName);

                //    }

                //    var hasPermission = permission.UserOperation
                //        .Any(x => x.OperationName == requestMethod);

                //    //Console.WriteLine(hasPermission);

                //    //if (hasPermission is false)
                //    //{
                //    //    context.Result = new UnauthorizedObjectResult(new ErrorResponseDTO
                //    //    {
                //    //        Message = "Usuário não tem permissão para essa operação"
                //    //    });

                //    //    return;
                //    //}

                //}

                context.HttpContext.Items["User"] = user;
            }

            await next();
        }
    }
}
