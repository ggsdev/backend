using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.Http.Controllers
{
    [ApiController]
    [Route("groups")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class GroupController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GroupController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupViewModel body)
        {


            if (body.Menus is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Menus is not found"
                });
            }

            var foundGroup = await _context.Groups.Where(x => x.Name == body.GroupName).FirstOrDefaultAsync();
            if (foundGroup != null)
            {
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Group Name is already exists."
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

            var groupId = Guid.NewGuid();
            var group = new Group
            {
                Id = groupId,
                Name = body.GroupName,
                CreatedAt = DateTime.UtcNow,
                Description = body.Description is null ? null : body.Description,
            };
            await _context.Groups.AddAsync(group);

            foreach (var menuParent in body.Menus)
            {
                var groupPermissionParentId = Guid.NewGuid();

                var foundMenuParent = await _context.Menus.Where(x => x.Id == menuParent.MenuId).FirstOrDefaultAsync();

                var addDotInOrder = foundMenuParent.Order + ".";
                var foundMenusChildrensInParent = await _context.Menus.Where(x => x.Order.Contains(addDotInOrder)).ToListAsync();
                var createGroupPermissionParent = new GroupPermission
                {
                    Id = groupPermissionParentId,
                    MenuIcon = foundMenuParent.Icon,
                    MenuName = foundMenuParent.Name,
                    MenuOrder = foundMenuParent.Order,
                    MenuRoute = foundMenuParent.Route,
                    CreatedAt = DateTime.UtcNow,
                    Menu = foundMenuParent,
                    GroupName = body.GroupName,
                    hasChildren = foundMenusChildrensInParent.Count == 0 ? false : true,
                    hasParent = foundMenuParent.Parent is null ? false : true,
                    Group = group,
                };
                await _context.GroupPermissions.AddAsync(createGroupPermissionParent);

                if (menuParent.Childrens is not null)
                {
                    if (menuParent.Childrens.Count != 0)
                    {
                        foreach (var menuChildren in menuParent.Childrens)
                        {
                            var groupPermissionChildrenId = Guid.NewGuid();

                            var foundMenuChildren = await _context.Menus.Where(x => x.Id == menuChildren.ChildrenId).FirstOrDefaultAsync();

                            var addDotInOrderChildren = foundMenuChildren.Order + ".";
                            var foundMenusChildrensInChildren = await _context.Menus.Where(x => x.Order.Contains(addDotInOrder)).ToListAsync();

                            var createGroupPermissionChildren = new GroupPermission
                            {
                                Id = groupPermissionChildrenId,
                                MenuIcon = foundMenuChildren.Icon,
                                MenuName = foundMenuChildren.Name,
                                MenuOrder = foundMenuChildren.Order,
                                MenuRoute = foundMenuChildren.Route,
                                CreatedAt = DateTime.UtcNow,
                                Menu = foundMenuChildren,
                                GroupName = body.GroupName,
                                hasChildren = foundMenusChildrensInChildren.Count == 0 ? false : true,
                                hasParent = foundMenuChildren.Parent is null ? false : true,
                                Group = group,
                            };
                            await _context.GroupPermissions.AddAsync(createGroupPermissionChildren);

                            foreach (var operationsChildren in menuChildren.Operations)
                            {
                                var groupOperationChildrenId = Guid.NewGuid();
                                var foundOperation = await _context.GlobalOperations.Where(x => x.Id == operationsChildren.OperationId).FirstOrDefaultAsync();
                                var createGroupOperationChildren = new GroupOperation
                                {
                                    Id = groupOperationChildrenId,
                                    GlobalOperation = foundOperation,
                                    GroupPermission = createGroupPermissionChildren,
                                    OperationName = foundOperation.Method
                                };
                                await _context.GroupOperations.AddAsync(createGroupOperationChildren);
                            }
                        }
                    }
                }
                if (createGroupPermissionParent.hasChildren == false)
                {
                    foreach (var operationsParent in menuParent.Operations)
                    {
                        var groupOperationParentId = Guid.NewGuid();
                        var foundOperation = await _context.GlobalOperations.Where(x => x.Id == operationsParent.OperationId).FirstOrDefaultAsync();
                        var createGroupOperationParent = new GroupOperation
                        {
                            Id = groupOperationParentId,
                            GlobalOperation = foundOperation,
                            GroupPermission = createGroupPermissionParent,
                            OperationName = foundOperation.Method
                        };
                        await _context.GroupOperations.AddAsync(createGroupOperationParent);
                    }
                }
            }

            await _context.SaveChangesAsync();

            var returnGroup = await _context.Groups.Where(x => x.Id == groupId).Include(x => x.GroupPermissions).ThenInclude(x => x.Operations).FirstOrDefaultAsync();
            var returnGroupDTO = _mapper.Map<Group, GroupWithMenusDTO>(returnGroup!);
            return Ok(returnGroupDTO);
        }

        [HttpPost("{groupId}/user/{userId}")]
        public async Task<IActionResult> InsertUser([FromRoute] Guid groupId, [FromRoute] Guid userId)
        {
            var group = await _context.Groups.Where(x => x.Id == groupId).FirstOrDefaultAsync();
            if (group == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Group is not found."
                });
            }

            var userHasGroup = await _context.Users.Where(x => x.Id == userId).Include(x => x.Group).Include(x => x.UserPermissions).FirstOrDefaultAsync();
            if (userHasGroup == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found."
                });
            }
            if (userHasGroup.Group != null)
            {
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"User is already a group"
                });
            }

            var groupPermissions = await _context.GroupPermissions.Include(x => x.Operations).Include(x => x.Menu).Include(x => x.Group).Where(x => x.Group.Id == groupId).ToListAsync();
            var groupPermissionsDTO = _mapper.Map<List<GroupPermission>, List<GroupPermissionsDTO>>(groupPermissions);
            try
            {
                foreach (var permission in groupPermissionsDTO)
                {
                    var groupPermission = await _context.GroupPermissions.Where(x => x.Id == permission.Id).FirstOrDefaultAsync();
                    var userPermission = new UserPermission
                    {
                        Id = Guid.NewGuid(),
                        hasChildren = permission.hasChildren,
                        hasParent = permission.hasParent,
                        MenuIcon = permission.MenuIcon,
                        MenuName = permission.MenuName,
                        MenuOrder = permission.MenuOrder,
                        MenuRoute = permission.MenuRoute,
                        GroupId = permission.Group.Id,
                        GroupName = permission.Group.Name,
                        MenuId = Guid.Parse(permission.Menu.Id),
                        User = userHasGroup,
                        GroupMenu = groupPermission,
                        CreatedAt = DateTime.Now,
                    };
                    await _context.UserPermissions.AddAsync(userPermission);

                    foreach (var operation in permission.Operations)
                    {
                        var foundOperation = await _context.GlobalOperations.Where(x => x.Method == operation.OperationName).FirstOrDefaultAsync();
                        var userOperation = new UserOperation
                        {
                            Id = Guid.NewGuid(),
                            OperationName = operation.OperationName,
                            UserPermission = userPermission,
                            GlobalOperation = foundOperation
                        };
                        await _context.UserOperations.AddAsync(userOperation);
                    }

                }

                userHasGroup.Group = group;
                await _context.SaveChangesAsync();
                var UserDTO = _mapper.Map<User, UserGroupDTO>(userHasGroup);
                return Ok(UserDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
