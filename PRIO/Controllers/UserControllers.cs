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
        private readonly UserServices _userServices;
        private readonly TokenServices _tokenServices;

        public UserControllers(UserServices userService, TokenServices tokenService)
        {
            _userServices = userService;
            _tokenServices = tokenService;

        }

        #region Get
        [HttpGet("users")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        public async Task<IActionResult> Get()
        {
            var userDTOS = await _userServices.RetrieveAllUsersAndMap();
            return Ok(userDTOS);
        }
        #endregion

        #region Create
        [HttpPost("users")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel body, [FromServices] DataContext context)
        {
            var userInDatabase = await context.Users.FirstOrDefaultAsync((x) => x.Email == body.Email || x.Username == body.Username);

            if (userInDatabase != null)
                return StatusCode(409, new { message = "User with this e-mail or username already exists." });

            try
            {
                var user = await _userServices.CreateUserAsync(body);
                return Created($"users/{user.Id}", user);
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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _userServices.GetUserByIdAsync(id);

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
            var user = await _userServices.GetUserByIdAsync(id);

            if (user == null || !user.IsActive)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "User not found or inactive."
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
                user.UpdatedAt = DateTime.UtcNow.ToLocalTime();
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
            var user = await _userServices.GetUserByIdAsync(id);

            if (user == null || !user.IsActive)
            {
                var userError = new ErrorResponse
                {
                    Message = "User not found or inactive."
                };

                return NotFound(userError);
            }

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow.ToLocalTime();
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
        [FromServices] DataContext context)
        {

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
            if (!user.IsActive)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "User not found or inactive"
                };

                return NotFound(errorResponse);
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