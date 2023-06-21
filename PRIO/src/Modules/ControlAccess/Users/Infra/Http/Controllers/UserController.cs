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
            var user = await _context.Users
                .Include(x => x.Group)
                .Include(x => x.UserPermissions)
                .ThenInclude(x => x.UserOperation)
                .FirstOrDefaultAsync(x => x.Id == userId);

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

        [HttpPatch("permissions/{userId}")]
        public async Task<IActionResult> EditPermission([FromBody] InsertUserPermissionViewModel body, [FromRoute] Guid userId)
        {

            if (body.Menus is null)

            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Menus is not found"
                });
            }

            foreach (var menuParent in body.Menus)
            {
                var foundMenuParent = await _context.Menus.Where(x => x.Id == menuParent.MenuId).FirstOrDefaultAsync();
                if (foundMenuParent is null)
                {
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = $"Menu Parent is not found"
                    });
                }
                if (menuParent.Childrens is not null)
                {
                    foreach (var menuChildren in menuParent.Childrens)
                    {
                        var foundMenuChildren = await _context.Menus.Where(x => x.Id == menuChildren.ChildrenId).Include(x => x.Parent).FirstOrDefaultAsync();
                        if (foundMenuChildren is null)
                        {
                            return NotFound(new ErrorResponseDTO
                            {
                                Message = $"Menu Children is not found"
                            });
                        }
                        if (foundMenuChildren.Parent is null)
                        {
                            return NotFound(new ErrorResponseDTO
                            {
                                Message = $"Relation Menu Children is not found"
                            });
                        }
                        string[] parts = foundMenuChildren.Order.Split('.');
                        string numberBeforeDot = parts[0];

                        if (numberBeforeDot != foundMenuParent.Order)
                        {
                            return Conflict(new ErrorResponseDTO
                            {
                                Message = $"This child menu does not belong to the parent menu"
                            });
                        }

                        foreach (var operationsChildrens in menuChildren.Operations)
                        {
                            var foundOperationChildren = await _context.GlobalOperations.Where(x => x.Id == operationsChildrens.OperationId).FirstOrDefaultAsync();
                            if (foundOperationChildren is null)
                            {
                                return NotFound(new ErrorResponseDTO
                                {
                                    Message = $"Operation Children is not found"
                                });
                            }
                        }
                    }
                }
                foreach (var operationsParent in menuParent.Operations)
                {
                    var foundOperationParent = await _context.GlobalOperations.Where(x => x.Id == operationsParent.OperationId).FirstOrDefaultAsync();
                    if (foundOperationParent is null)
                    {
                        return NotFound(new ErrorResponseDTO
                        {
                            Message = $"Operation Parent is not found"
                        });
                    }
                }
            }

            var userWithPermissions = await _context
                .Users
                .Where(x => x.Id == userId)
                .Include(x => x.Group)
                .Include(x => x.UserPermissions!)
                .ThenInclude(x => x.UserOperation)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (userWithPermissions is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });
            }
            if (userWithPermissions.Group is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User no have group"
                });
            }

            var permissionsUser = await _context.UserPermissions.Include(x => x.User).Where(x => x.User.Id == userId).ToListAsync();
            _context.RemoveRange(permissionsUser);

            var operationsUser = await _context.UserOperations.Include(x => x.UserPermission).ThenInclude(x => x.User).Where(x => x.UserPermission.User.Id == userId).ToListAsync();
            _context.RemoveRange(operationsUser);

            foreach (var menu in body.Menus)
            {
                //PROCURANDO PERMISSAO NO GRUPO
                var verifyRelationMenuParent = await _context.GroupPermissions
                    .Include(x => x.Menu)
                    .Include(x => x.Group)
                    .Where(x => x.Menu.Id == menu.MenuId)
                    .Where(x => x.Group.Id == userWithPermissions.Group.Id)
                    .FirstOrDefaultAsync();

                // EXISTE A PERMISSAO NO GRUPO
                if (verifyRelationMenuParent is not null)
                {
                    var createMenuParent = new UserPermission
                    {
                        GroupId = userWithPermissions.Group.Id,
                        GroupName = userWithPermissions.Group.Name,
                        hasParent = false,
                        hasChildren = true,
                        Id = Guid.NewGuid(),
                        MenuIcon = verifyRelationMenuParent.MenuIcon,
                        MenuName = verifyRelationMenuParent.MenuName,
                        MenuOrder = verifyRelationMenuParent.MenuOrder,
                        MenuRoute = verifyRelationMenuParent.MenuRoute,
                        CreatedAt = DateTime.UtcNow,
                        MenuId = verifyRelationMenuParent.Menu.Id,
                        User = userWithPermissions,
                        GroupMenu = verifyRelationMenuParent,
                    };
                    await _context.UserPermissions.AddAsync(createMenuParent);
                    // EXISTE A PERMISSAO NO GRUPO E TEM LISTA DE FILHO
                    if (menu.Childrens is not null && menu.Childrens.Count != 0)
                    {

                        foreach (var children in menu.Childrens)
                        {
                            //PROCURANDO PERMISSAO DO FILHO
                            var verifyRelationMenuChildren = await _context.GroupPermissions
                              .Include(x => x.Menu)
                              .Include(x => x.Group)
                              .Where(x => x.Menu.Id == children.ChildrenId)
                              .Where(x => x.Group.Id == userWithPermissions.Group.Id)
                              .FirstOrDefaultAsync();

                            // EXISTE PERMISSAO DO FILHO NO GRUPO 
                            if (verifyRelationMenuChildren is not null)
                            {
                                // EXISTE PERMISSAO DO FILHO NO GRUPO E TEM LISTA DE OPERAÇÕES
                                if (children.Operations is not null && children.Operations.Count != 0)
                                {
                                    var createMenuChildren = new UserPermission
                                    {
                                        GroupId = userWithPermissions.Group.Id,
                                        GroupName = userWithPermissions.Group.Name,
                                        hasParent = true,
                                        hasChildren = false,
                                        Id = Guid.NewGuid(),
                                        MenuIcon = verifyRelationMenuChildren.MenuIcon,
                                        MenuName = verifyRelationMenuChildren.MenuName,
                                        MenuOrder = verifyRelationMenuChildren.MenuOrder,
                                        MenuRoute = verifyRelationMenuChildren.MenuRoute,
                                        CreatedAt = DateTime.UtcNow,
                                        MenuId = verifyRelationMenuChildren.Menu.Id,
                                        User = userWithPermissions,
                                        GroupMenu = verifyRelationMenuChildren,
                                    };
                                    await _context.UserPermissions.AddAsync(createMenuChildren);

                                    foreach (var operation in children.Operations)
                                    {
                                        //PROCURANDO PERMISSAO DA OPERACAO NO GRUPO
                                        var verifyRelationMenuChildrenWithOperations = await _context.GroupOperations
                                          .Include(x => x.GlobalOperation)
                                          .Include(x => x.GroupPermission)
                                          .Where(x => x.GlobalOperation.Id == operation.OperationId)
                                          .Where(x => x.GroupPermission.Id == verifyRelationMenuChildren.Id)
                                          .FirstOrDefaultAsync();

                                        //EXISTE PERMISSAO DA OPERACAO
                                        if (verifyRelationMenuChildrenWithOperations is not null)
                                        {
                                            var createGroupOperationsInUser = new UserOperation
                                            {
                                                OperationName = verifyRelationMenuChildrenWithOperations.OperationName,
                                                UserPermission = createMenuChildren,
                                                GlobalOperation = verifyRelationMenuChildrenWithOperations.GlobalOperation,
                                                GroupName = verifyRelationMenuChildrenWithOperations.GroupName,
                                                Id = Guid.NewGuid(),
                                            };
                                            await _context.UserOperations.AddAsync(createGroupOperationsInUser);
                                        }
                                        else // NAO EXISTE PERMISSAO DA OPERACAO
                                        {
                                            var verifyRelationMenuChildrenWithOperationsInMaster = await _context.GroupOperations
                                              .Include(x => x.GlobalOperation)
                                              .Include(x => x.GroupPermission)
                                              .Where(x => x.GlobalOperation.Id == operation.OperationId)
                                              .Where(x => x.GroupPermission.GroupName == "Master")
                                              .FirstOrDefaultAsync();

                                            var createGroupOperationsInUser = new UserOperation
                                            {
                                                OperationName = verifyRelationMenuChildrenWithOperationsInMaster.OperationName,
                                                UserPermission = createMenuChildren,
                                                GlobalOperation = verifyRelationMenuChildrenWithOperationsInMaster.GlobalOperation,
                                                GroupName = verifyRelationMenuChildrenWithOperationsInMaster.GroupName,
                                                Id = Guid.NewGuid(),
                                            };
                                            await _context.UserOperations.AddAsync(createGroupOperationsInUser);
                                        }
                                    }
                                }
                            }
                            else // NÃO EXISTE PERMISSAO DO FILHO NO GRUPO 
                            {
                                if (children.Operations is not null && children.Operations.Count != 0)
                                {
                                    //PROCURANDO PERMISSAO PELO MASTER
                                    var verifyRelationMenuChildrenInMaster = await _context.GroupPermissions
                                          .Include(x => x.Menu)
                                          .Include(x => x.Group)
                                          .Where(x => x.Menu.Id == children.ChildrenId)
                                          .Where(x => x.Group.Name == "Master")
                                          .FirstOrDefaultAsync();

                                    var createMenuChildren = new UserPermission
                                    {
                                        GroupId = verifyRelationMenuChildrenInMaster.Group.Id,
                                        GroupName = verifyRelationMenuChildrenInMaster.Group.Name,
                                        hasParent = true,
                                        hasChildren = false,
                                        Id = Guid.NewGuid(),
                                        MenuIcon = verifyRelationMenuChildrenInMaster.MenuIcon,
                                        MenuName = verifyRelationMenuChildrenInMaster.MenuName,
                                        MenuOrder = verifyRelationMenuChildrenInMaster.MenuOrder,
                                        MenuRoute = verifyRelationMenuChildrenInMaster.MenuRoute,
                                        CreatedAt = DateTime.UtcNow,
                                        MenuId = verifyRelationMenuChildrenInMaster.Menu.Id,
                                        User = userWithPermissions,
                                        GroupMenu = verifyRelationMenuChildrenInMaster,
                                    };
                                    await _context.UserPermissions.AddAsync(createMenuChildren);

                                    foreach (var operation in children.Operations)
                                    {
                                        //PROCURANDO PERMISSAO DA OPERACAO PELO MASTER
                                        var verifyRelationMenuChildrenWithOperationsInMaster = await _context.GroupOperations
                                          .Include(x => x.GlobalOperation)
                                          .Include(x => x.GroupPermission)
                                          .Where(x => x.GlobalOperation.Id == operation.OperationId)
                                          .Where(x => x.GroupPermission.GroupName == "Master")
                                          .FirstOrDefaultAsync();

                                        var createGroupOperationsInUser = new UserOperation
                                        {
                                            OperationName = verifyRelationMenuChildrenWithOperationsInMaster.OperationName,
                                            UserPermission = createMenuChildren,
                                            GlobalOperation = verifyRelationMenuChildrenWithOperationsInMaster.GlobalOperation,
                                            GroupName = verifyRelationMenuChildrenWithOperationsInMaster.GroupName,
                                            Id = Guid.NewGuid(),
                                        };
                                        await _context.UserOperations.AddAsync(createGroupOperationsInUser);
                                    }

                                }
                            }
                        }
                    }
                    else // EXISTE A PERMISSAO NO GRUPO E NÃO TEM LISTA DE FILHO
                    {
                        if (menu.Operations is not null && menu.Operations.Count != 0)
                        {
                            foreach (var operation in menu.Operations)
                            {
                                //PROCURANDO PERMISSAO DA OPERACAO NO GRUPO
                                var verifyRelationMenuParentWithOperations = await _context.GroupOperations
                                  .Include(x => x.GlobalOperation)
                                  .Include(x => x.GroupPermission)
                                  .Where(x => x.GlobalOperation.Id == operation.OperationId)
                                  .Where(x => x.GroupPermission.Id == verifyRelationMenuParent.Id)
                                  .FirstOrDefaultAsync();

                                //EXISTE PERMISSAO DA OPERACAO
                                if (verifyRelationMenuParentWithOperations is not null)
                                {
                                    var createGroupOperationsInUser = new UserOperation
                                    {
                                        OperationName = verifyRelationMenuParentWithOperations.OperationName,
                                        UserPermission = createMenuParent,
                                        GlobalOperation = verifyRelationMenuParentWithOperations.GlobalOperation,
                                        GroupName = verifyRelationMenuParentWithOperations.GroupName,
                                        Id = Guid.NewGuid(),
                                    };
                                    await _context.UserOperations.AddAsync(createGroupOperationsInUser);
                                }
                                else // NAO EXISTE PERMISSAO DA OPERACAO
                                {
                                    var verifyRelationMenuChildrenWithOperationsInMaster = await _context.GroupOperations
                                      .Include(x => x.GlobalOperation)
                                      .Include(x => x.GroupPermission)
                                      .Where(x => x.GlobalOperation.Id == operation.OperationId)
                                      .Where(x => x.GroupPermission.GroupName == "Master")
                                      .FirstOrDefaultAsync();

                                    var createGroupOperationsInUser = new UserOperation
                                    {
                                        OperationName = verifyRelationMenuChildrenWithOperationsInMaster.OperationName,
                                        UserPermission = createMenuParent,
                                        GlobalOperation = verifyRelationMenuChildrenWithOperationsInMaster.GlobalOperation,
                                        GroupName = verifyRelationMenuChildrenWithOperationsInMaster.GroupName,
                                        Id = Guid.NewGuid(),
                                    };
                                    await _context.UserOperations.AddAsync(createGroupOperationsInUser);
                                }
                            }
                        }
                    }
                }
                else // NÃO EXISTE A PERMISSAO NO GRUPO
                {
                    var verifyRelationMenuParentInMaster = await _context.GroupPermissions
                                          .Include(x => x.Menu)
                                          .Include(x => x.Group)
                                          .Where(x => x.Menu.Id == menu.MenuId)
                                          .Where(x => x.Group.Name == "Master")
                                          .FirstOrDefaultAsync();

                    var createMenuParent = new UserPermission
                    {
                        GroupId = verifyRelationMenuParentInMaster.Group.Id,
                        GroupName = verifyRelationMenuParentInMaster.Group.Name,
                        hasParent = false,
                        hasChildren = true,
                        Id = Guid.NewGuid(),
                        MenuIcon = verifyRelationMenuParentInMaster.MenuIcon,
                        MenuName = verifyRelationMenuParentInMaster.MenuName,
                        MenuOrder = verifyRelationMenuParentInMaster.MenuOrder,
                        MenuRoute = verifyRelationMenuParentInMaster.MenuRoute,
                        CreatedAt = DateTime.UtcNow,
                        MenuId = verifyRelationMenuParentInMaster.Menu.Id,
                        User = userWithPermissions,
                        GroupMenu = verifyRelationMenuParentInMaster,
                    };
                    await _context.UserPermissions.AddAsync(createMenuParent);

                    if (menu.Childrens is not null && menu.Childrens.Count != 0)
                    {
                        foreach (var children in menu.Childrens)
                        {
                            if (children.Operations is not null && children.Operations.Count != 0)
                            {

                                var verifyRelationMenuChildrenInMaster = await _context.GroupPermissions
                                          .Include(x => x.Menu)
                                          .Include(x => x.Group)
                                          .Where(x => x.Menu.Id == children.ChildrenId)
                                          .Where(x => x.Group.Name == "Master")
                                          .FirstOrDefaultAsync();

                                var createMenuChildren = new UserPermission
                                {
                                    GroupId = verifyRelationMenuChildrenInMaster.Group.Id,
                                    GroupName = verifyRelationMenuChildrenInMaster.Group.Name,
                                    hasParent = true,
                                    hasChildren = false,
                                    Id = Guid.NewGuid(),
                                    MenuIcon = verifyRelationMenuChildrenInMaster.MenuIcon,
                                    MenuName = verifyRelationMenuChildrenInMaster.MenuName,
                                    MenuOrder = verifyRelationMenuChildrenInMaster.MenuOrder,
                                    MenuRoute = verifyRelationMenuChildrenInMaster.MenuRoute,
                                    CreatedAt = DateTime.UtcNow,
                                    MenuId = verifyRelationMenuChildrenInMaster.Menu.Id,
                                    User = userWithPermissions,
                                    GroupMenu = verifyRelationMenuChildrenInMaster,
                                };
                                await _context.UserPermissions.AddAsync(createMenuChildren);

                                foreach (var operation in children.Operations)
                                {
                                    //PROCURANDO PERMISSAO DA OPERACAO PELO MASTER
                                    var verifyRelationMenuParentWithOperationsInMaster = await _context.GroupOperations
                                      .Include(x => x.GlobalOperation)
                                      .Include(x => x.GroupPermission)
                                      .Where(x => x.GlobalOperation.Id == operation.OperationId)
                                      .Where(x => x.GroupPermission.GroupName == "Master")
                                      .FirstOrDefaultAsync();

                                    var createGroupOperationsInUser = new UserOperation
                                    {
                                        OperationName = verifyRelationMenuParentWithOperationsInMaster.OperationName,
                                        UserPermission = createMenuChildren,
                                        GlobalOperation = verifyRelationMenuParentWithOperationsInMaster.GlobalOperation,
                                        GroupName = verifyRelationMenuParentWithOperationsInMaster.GroupName,
                                        Id = Guid.NewGuid(),
                                    };
                                    await _context.UserOperations.AddAsync(createGroupOperationsInUser);
                                }
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            var userDTO = _mapper.Map<User, ProfileDTO>(userWithPermissions);
            return Ok(userDTO);
        }
    }
}