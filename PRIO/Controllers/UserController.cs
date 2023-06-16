using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.Models;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Users;

namespace PRIO.Controllers
{
    [ApiController]
    public class UserController : BaseApiController
    {
        public UserController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        #region Get
        [HttpGet("users")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users.ToListAsync();
            var userDTOS = _mapper.Map<List<User>, List<UserDTO>>(users);
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
                var userId = Guid.NewGuid();
                var user = new User
                {
                    Id = userId,
                    Name = body.Name,
                    Username = body.Username,
                    Email = body.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(body.Password),
                    Description = body.Description is not null ? body.Description : null,
                };
                await _context.AddAsync(user);

                var currentData = _mapper.Map<User, UserHistoryDTO>(user);
                currentData.createdAt = DateTime.UtcNow;
                currentData.updatedAt = DateTime.UtcNow;

                var history = new SystemHistory
                {
                    Table = HistoryColumns.TableUsers,
                    TypeOperation = HistoryColumns.Create,
                    CreatedBy = user?.Id,
                    TableItemId = userId,
                    CurrentData = currentData,
                };

                await _context.SystemHistories.AddAsync(history);
                await _context.SaveChangesAsync();

                var userDTO = _mapper.Map<User, UserDTO>(user);
                return Created($"users/{user.Id}", userDTO);
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

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
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user is null || user.IsActive is false)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "User not found or inactive."
                };

                return NotFound(errorResponse);
            }

            var beforeChangesUser = _mapper.Map<UserHistoryDTO>(user);

            var updatedProperties = ControllerUtils.CompareAndUpdateUser(user, body);

            //var userId = (Guid)HttpContext.Items["Id"]!;
            //var userOperation = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            //if (userOperation is null)
            //    return NotFound(new ErrorResponseDTO
            //    {
            //        Message = $"User is not found"
            //    });

            try
            {

                user.Name = body.Name is not null ? body.Name : user.Name;
                user.Password = body.Password is not null ? BCrypt.Net.BCrypt.HashPassword(body.Password) : user.Password;
                user.Email = body.Email is not null ? body.Email : user.Email;
                user.Username = body.Username is not null ? body.Username : user.Username;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                var userDTO = _mapper.Map<User, UserDTO>(user);
                return Ok(userDTO);
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                var userError = new ErrorResponseDTO
                {
                    Message = "User not found or inactive."
                };

                return NotFound(userError);
            }

            var userId = (Guid)HttpContext.Items["Id"]!;
            var userOperation = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (userOperation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            user.DeletedAt = DateTime.UtcNow;
            user.IsActive = false;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("users/{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                var userError = new ErrorResponseDTO
                {
                    Message = "User not found or inactive."
                };

                return NotFound(userError);
            }

            var userId = (Guid)HttpContext.Items["Id"]!;
            var userOperation = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (userOperation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            user.IsActive = true;
            user.DeletedAt = null;

            _context.Update(user);
            await _context.SaveChangesAsync();

            var UserDTO = _mapper.Map<User, UserDTO>(user);
            return Ok(UserDTO);
        }

        //[HttpGet("users/{id}/history")]
        //public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        //    if (user is null)
        //    {
        //        var userError = new ErrorResponseDTO
        //        {
        //            Message = "User not found or inactive."
        //        };

        //        return NotFound(userError);
        //    }

        //    var userHistories = await _context.UserHistories
        //                                            .Include(x => x.User)
        //                                            .Where(x => x.User.Id == id)
        //                                            .OrderByDescending(x => x.CreatedAt)
        //                                            .ToListAsync();

        //    var userHistoriesDTO = _mapper.Map<List<UserHistory>, List<UserHistoryDTO>>(userHistories);

        //    return Ok(userHistoriesDTO);
        //}
        #endregion
    }
}