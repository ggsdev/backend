using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.src.Shared;
using PRIO.src.Shared.Infra.Http.Services;
using PRIO.ViewModels.Users;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.Http.Controllers
{
    public class SessionController : BaseApiController
    {
        private TokenServices _tokenServices;

        public SessionController(DataContext context, IMapper mapper, TokenServices tokenServices)
    : base(context, mapper)
        {
            _tokenServices = tokenServices;
        }

        #region Login
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> Login(
        [FromBody] LoginViewModel body)
        {
            var user = await _context
                .Users
                .Include(u => u.Session)
                .FirstOrDefaultAsync(x => x.Email == body.Email && x.IsActive);

            if (user is null)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "E-mail or password invalid"
                };

                return Unauthorized(errorResponse);
            }

            var passwordMatch = BCrypt.Net.BCrypt.Verify(body.Password, user.Password);
            if (!passwordMatch)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "E-mail or password invalid"
                };

                return Unauthorized(errorResponse);
            }

            var userHttpAgent = Request.Headers["User-Agent"].ToString();
            var token = await _tokenServices.CreateSessionAndToken(user, userHttpAgent);


            return Ok(new LoginDTO
            {
                Token = token,
            });
        }
        #endregion
    }
}
