using dotenv.net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.Http.Services;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.Http.Controllers
{
    public class SessionController : ControllerBase
    {
        private readonly TokenServices _tokenServices;
        private readonly DataContext _context;

        public SessionController(DataContext context, TokenServices tokenServices)

        {
            _tokenServices = tokenServices;
            _context = context;
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

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponseDTO))]
        [HttpPost("loginAd")]
        public async Task<IActionResult> LoginAD([FromBody] LoginAdViewModel body)
        {
            var envVars = DotEnv.Read();
            var secretKey = envVars["SECRET_KEY"];

            var username = Decrypt.DecryptAes(body.Username, secretKey);
            var password = Decrypt.DecryptAes(body.Password, secretKey);

            var credentialsValid = ActiveDirectory
                .VerifyCredentialsWithActiveDirectory(username, password);

            if (credentialsValid is false)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "Username or password invalid"
                });

            var user = await _context
                .Users
                .Include(u => u.Session)
                .FirstOrDefaultAsync(x => x.Username == body.Username);

            string token;
            var userHttpAgent = Request.Headers["User-Agent"].ToString();

            if (user is null)
            {
                var userId = Guid.NewGuid();
                var createUser = new User
                {
                    Id = userId,
                    Username = body.Username,
                };

                await _context.Users.AddAsync(createUser);

                var history = new SystemHistory
                {
                    TypeOperation = HistoryColumns.Create,
                    Table = HistoryColumns.TableUsers,
                    TableItemId = userId,
                    CurrentData = createUser,
                    CreatedBy = userId,
                };
                await _context.SystemHistories.AddAsync(history);
                await _context.SaveChangesAsync();

                token = await _tokenServices.CreateSessionAndToken(createUser, userHttpAgent);
            }
            else
            {
                token = await _tokenServices.CreateSessionAndToken(user, userHttpAgent);

            }

            return Ok(new LoginDTO
            {
                Token = token,
            });
        }
    }
    #endregion
}
