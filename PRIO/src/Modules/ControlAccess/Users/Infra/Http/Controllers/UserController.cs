using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.Http.Controllers
{
    [ApiController]
    [Route("users")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)

        {
            _service = service;
        }

        #region Get
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        public async Task<IActionResult> Get()
        {
            var usersDTO = await _service.GetUsers();
            return Ok(usersDTO);
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var userDTO = await _service.CreateUserAsync(body, user);
            return Created($"users/{userDTO.Id}", userDTO);
        }
        #endregion

        #region Profile
        [HttpGet("profile")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public async Task<IActionResult> Profile()
        {
            var id = (Guid)HttpContext.Items["Id"]!;
            var profileDTO = await _service.ProfileAsync(id);
            return Ok(profileDTO);
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var userDTO = await _service.GetUserByIdAsync(id);
            return Ok(userDTO);
        }
        #endregion

        #region Update
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> UpdatePartialAsync([FromRoute] Guid id, [FromBody] UpdateUserViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            try
            {
                var userDTO = await _service.UpdateUserByIdAsync(id, body, user);
                return Ok(userDTO);
            }
            catch (DbUpdateException e)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Soft Delete
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponseDTO))]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {

            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            var userOperationId = (Guid)HttpContext.Items["Id"]!;
            await _service.DeleteUserByIdAsync(id, userOperationId, user);
            return NoContent();
        }
        #endregion

        #region Restore
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            var userOperationId = (Guid)HttpContext.Items["Id"]!;
            var userDTO = await _service.RestoreUserByIdAsync(id, userOperationId, user);
            return Ok(userDTO);
        }
        #endregion

        #region Edit Permission User
        [HttpPatch("{id}/permissions")]
        public async Task<IActionResult> EditPermission([FromBody] InsertUserPermissionViewModel body, [FromRoute] Guid id)
        {
            var userDTO = await _service.EditPermissionUserByIdAsync(body, id);
            return Ok(userDTO);
        }
        #endregion

        #region Refresh Permission User
        [HttpPatch("{id}/permissions/refresh")]
        public async Task<IActionResult> RefreshPermission([FromRoute] Guid id)
        {
            var userDTO = await _service.RefreshPermissionUserByIdAsync(id);
            return Ok(userDTO);
        }
        #endregion
    }
}