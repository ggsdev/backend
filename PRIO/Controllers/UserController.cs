using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Models.Users;
using PRIO.Services;
using PRIO.ViewModels.Users;

namespace PRIO.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly TokenServices _tokenServices;
        private DataContext _context;
        private IMapper _mapper;

        public UserController(UserServices userService, TokenServices tokenService, IMapper mapper, DataContext context)
        {
            _userServices = userService;
            _tokenServices = tokenService;
            _mapper = mapper;
            _context = context;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel body)
        {
            var userInDatabase = await _context.Users.FirstOrDefaultAsync((x) => x.Email == body.Email || x.Username == body.Username);

            if (userInDatabase != null)
                return StatusCode(409, new { message = "User with this e-mail or username already exists." });

            try
            {
                var user = await _userServices.CreateUserAsync(body);
                return Created($"users/{user.Id}", user);
            }
            catch (DbUpdateException e)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = $"Error validating fields: {e.Message}"
                };

                return BadRequest(errorResponse);
            }
        }
        #endregion

        #region Get By Id
        [HttpGet("users/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _userServices.GetUserByIdAsync(id);

            if (user is null)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "User Not found."
                };

                return NotFound(errorResponse);
            }

            var userLoggedId = HttpContext.Items?["Id"]?.ToString();

            if (userLoggedId != user.Id.ToString())
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "User don't have permission to do that."
                };

                return StatusCode(403, errorResponse);
            }

            var userDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(userDTO);
        }
        #endregion

        #region Update
        [HttpPatch("users/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> UpdatePartialAsync([FromRoute] Guid id, [FromBody] UpdateUserViewModel body)
        {
            var user = await _userServices.GetUserByIdAsync(id);


            if (user == null || !user.IsActive)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "User not found or inactive."
                };

                return NotFound(errorResponse);
            }

            try
            {
                user.Name = body.Name is not null ? body.Name : user.Name;
                user.Password = body.Password is not null ? BCrypt.Net.BCrypt.HashPassword(body.Password) : user.Password;
                user.Email = body.Email is not null ? body.Email : user.Email;
                user.Username = body.Username is not null ? body.Username : user.Username;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

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
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var user = await _userServices.GetUserByIdAsync(id);

            if (user is null)
            {
                var userError = new ErrorResponseDTO
                {
                    Message = "User not found or inactive."
                };

                return NotFound(userError);
            }
            user.DeletedAt = DateTime.UtcNow;
            user.IsActive = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

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