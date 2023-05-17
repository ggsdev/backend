using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Files._039;
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

            if (user is null)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "User Not found."
                };

                return NotFound(errorResponse);
            }

            var userLoggedId = HttpContext.Items?["Id"]?.ToString();

            if (userLoggedId != user.Id.ToString())
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "User don't have permission to do that."
                };

                return StatusCode(403, errorResponse);
            }

            var userDTO = _userServices.MapToDTO(user);
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

                user.UpdatedAt = DateTime.UtcNow.ToLocalTime();

                context.Users.Update(user);
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

            if (user is null)
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
                .FirstOrDefaultAsync(x => x.Email == body.Email && x.IsActive);

            if (user is null)
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

            var userHttpAgent = Request.Headers["User-Agent"].ToString();
            var token = await _tokenServices.CreateSessionAndToken(user, userHttpAgent);

            return Ok(new LoginDTO
            {
                Token = token,
            });
        }
        #endregion

        [HttpPost("xml")]
        [AllowAnonymous]
        public ActionResult XMLTeste()
        {


            string pathXml;
            string pathSchema;
            string basePath = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\_039";
            //string xmlPathABL = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\_039\\ABL\\039ABL.xml";
            //string schemaAbl039 = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\_039\\ABL\\Schema.xsd";

            //string xmlPathFrade = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\_039\\Frade\\039FRADE.xml";
            //string schemaFrade039 = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\_039\\Frade\\Schema.xsd";

            pathXml = basePath + "\\Bravo\\Mock.xml";
            //pathSchema = basePath + "\\Schema.xsd";
            pathSchema = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\_039\\Schema.xsd";
            Functions.IsRightFormat(pathXml, pathSchema);


            return Ok(new { Message = "Deu certo" });


        }

        public class ValidationDTO
        {
            public string? ValidationErrors { get; set; }
        }

    }
}