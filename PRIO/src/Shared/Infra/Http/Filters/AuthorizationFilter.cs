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
