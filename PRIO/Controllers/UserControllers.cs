using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.Services;
using PRIO.ViewModels;

namespace PRIO.Controllers
{
    [ApiController]
    public class UserControllers : ControllerBase
    {
        #region Get
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var users = await context.Users.ToListAsync();
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                if (!user.IsActive)
                    continue;

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };
                userDTOs.Add(userDTO);
            }

            return Ok(userDTOs);

        }
        #endregion

        #region Create
        [HttpPost("users")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel body, [FromServices] DataContext context)
        {
            var userInDatabase = await context.Users.FirstOrDefaultAsync((x) => x.Email == body.Email);

            if (userInDatabase != null)
                return StatusCode(409, new { message = "User with this e-mail already exists." });

            try
            {
                var user = new User
                {
                    Name = body.Name,
                    Username = body.Username,
                    Email = body.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(body.Password),
                    CreatedAt = DateTime.Now,

                };

                await context.AddAsync(user);
                await context.SaveChangesAsync();

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                };


                return Created($"users/{user.Id}", userDTO);
            }
            catch (DbUpdateException e)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = $"Error validating fields: {e.Message}"
                };

                Console.WriteLine(e);
                return BadRequest(errorResponse);
            }
        }
        #endregion

        #region Get By Id
        [HttpGet("users/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetById([FromRoute] Guid id, [FromServices] DataContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == id);

            if (user == null)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "User Not found."
                };

                return NotFound(errorResponse);
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };


            return Ok(userDTO);
        }
        #endregion

        #region Update
        [HttpPatch("users/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdatePartialAsync([FromRoute] Guid id, [FromBody] UpdateUserViewModel body, [FromServices] DataContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == id);

            if (user == null)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "User Not found."
                };

                return NotFound(errorResponse);
            }

            try
            {
                if (body.Name != null) user.Name = body.Name;

                if (body.Password != null) user.Password = BCrypt.Net.BCrypt.HashPassword(body.Password);

                if (body.Email != null) user.Email = body.Email;

                if (body.Username != null) user.Username = body.Username;


                context.Users.Update(user);
                user.UpdatedAt = DateTime.Now;
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        #endregion

        #region Soft Delete
        [HttpDelete("users/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, [FromServices] DataContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == id);

            if (user == null || !user.IsActive)
            {
                var userError = new ErrorResponse
                {
                    Message = "User Not found."
                };

                return NotFound(userError);
            }

            user.IsActive = false;
            user.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Login
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Login(
        [FromBody] LoginViewModel body,
        [FromServices] DataContext context,
        [FromServices] IHttpContextAccessor httpContextAccessor,
        [FromServices] TokenService tokenService)
        {
            //string userAgent = Request.Headers["User-Agent"].ToString();
            //Console.WriteLine(userAgent);

            var user = await context
                .Users
                .Include(u => u.Session)
                .FirstOrDefaultAsync(x => x.Email == body.Email);

            if (user == null)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "E-mail or password invalid"
                };

                return Unauthorized(errorResponse);
            }
            var passwordMatch = BCrypt.Net.BCrypt.Verify(body.Password, user.Password);

            if (!passwordMatch)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "E-mail or password invalid"
                };

                return Unauthorized(errorResponse);
            }

            var token = tokenService.GenerateToken(user);

            if (user.Session == null)
            {
                var session = new Session
                {
                    Token = token,
                    User = user,
                };

                await context.Sessions.AddAsync(session);
                await context.SaveChangesAsync();

                return Ok(new LoginDTO
                {
                    Token = token,
                });

            }

            if (user.Session.ExpiresIn < DateTime.Now)
            {
                var updatedSession = new Session
                {
                    Token = token,
                    ExpiresIn = DateTime.UtcNow.AddDays(5).ToLocalTime(),
                    User = user,
                };

                user.Session.Token = updatedSession.Token;
                user.Session.ExpiresIn = updatedSession.ExpiresIn;
                user.Session.User = user;

                context.Sessions.Update(updatedSession);
                await context.SaveChangesAsync();

                return Ok(new LoginDTO
                {
                    Token = updatedSession.Token,
                });

            }

            return Ok(new LoginDTO
            {
                Token = user.Session.Token,
            });

        }
        #endregion
    }
}