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
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.Http.Controllers
{
    public class SessionController : ControllerBase
    {
        private readonly TokenServices _tokenServices;
        private readonly DataContext _context;
        private readonly SystemHistoryService _systemHistoryService;

        public SessionController(DataContext context, TokenServices tokenServices, SystemHistoryService systemHistoryService)

        {
            _tokenServices = tokenServices;
            _context = context;
            _systemHistoryService = systemHistoryService;
        }

        #region Login
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> Login(
        [FromBody] LoginViewModel body)
        {
            var envVars = DotEnv.Read();

            var secretKey = envVars["SECRET_KEY"];
            if ((Decrypt.TryParseBase64String(body.Username, out byte[]? encriptedBytes) && Decrypt.TryParseBase64String(body.Password, out byte[]? encryptedBytes2)) is false)
                return BadRequest(new ErrorResponseDTO { Message = "Username and password not encrypted." });

            var username = Decrypt
                .DecryptAes(body.Username, secretKey);

            var password = Decrypt
            .DecryptAes(body.Password, secretKey);

            var credentialsValid = ActiveDirectory
                .VerifyCredentialsWithActiveDirectory(username, password);

            if (credentialsValid is false)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "Usuário ou senha inválida."
                });

            var treatedUsername = username.Split('@')[0];

            var user = await _context
                .Users
                .Include(u => u.Session)
                .FirstOrDefaultAsync(x => x.Username == treatedUsername && x.IsActive);

            string token;
            var userHttpAgent = Request.Headers["User-Agent"].ToString();

            if (user is null)
            {
                var userId = Guid.NewGuid();
                var createUser = new User
                {
                    Id = userId,
                    Username = treatedUsername,
                };

                await _context.Users.AddAsync(createUser);

                //await _systemHistoryService
                //    .Create<User, UserHistoryDTO>(HistoryColumns.TableUsers, createUser, userId, createUser);

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
}
#endregion
