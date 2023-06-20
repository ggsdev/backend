using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.Http.Filters;
using PRIO.src.Shared.SystemHistories.Dtos.UserDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.Http.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserController(DataContext context, IMapper mapper)

        {
            _context = context;
            _mapper = mapper;
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

        [HttpGet("profile")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        public async Task<IActionResult> Profile([FromRoute] Guid id)
        {
            var userId = (Guid)HttpContext.Items["Id"];
            var user = await _context.Users.Include(x => x.UserPermissions).ThenInclude(x => x.UserOperation).FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "User Not found."
                };

                return NotFound(errorResponse);
            }

            if (userId.ToString() != user.Id.ToString())
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Message = "User don't have permission to do that."
                };

                return StatusCode(403, errorResponse);
            }


            user.UserPermissions = user.UserPermissions.OrderBy(x => x.MenuOrder).ToList();
            var userDTO = _mapper.Map<User, ProfileDTO>(user);
            var parentElements = new List<UserPermissionParentDTO>();
            foreach (var permission in userDTO.UserPermissions)
            {
                var menuName = permission.MenuName;
                var menuRoute = permission.MenuRoute;

                if (permission.hasParent == false && permission.hasChildren == true)
                {
                    permission.Children = new List<UserPermissionChildrenDTO>();
                    parentElements.Add(permission);
                }
                else if (permission.hasParent == true && permission.hasChildren == false)
                {
                    var parentOrder = permission.MenuOrder.Split('.')[0];
                    var parentElement = parentElements.FirstOrDefault(x => x.MenuOrder.StartsWith(parentOrder));
                    if (parentElement != null)
                    {
                        var childrenDTO = _mapper.Map<UserPermissionParentDTO, UserPermissionChildrenDTO>(permission);
                        parentElement.Children.Add(childrenDTO);
                        parentElements.Remove(permission);
                    }
                }

                foreach (var operation in permission.UserOperation)
                {
                    var operationName = operation.OperationName;
                }
            }

            userDTO.UserPermissions.RemoveAll(permission => permission.MenuOrder.Contains("."));


            return Ok(userDTO);
        }


        #region Get By Id
        [HttpGet("users/{id}")]
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
        [HttpPatch("users/{id}")]
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

            var updatedProperties = UpdateFields.CompareAndUpdateUser(user, body);

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
        [HttpDelete("users/{id}")]
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