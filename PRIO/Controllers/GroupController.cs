using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.ControlAccessDTOS;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Controllers
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
        public async Task<IActionResult> Create()
        {
            return View();
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
                    Console.WriteLine("oi");
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
                    if (permission.Operations.Count > 0)
                    {
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
                            await _context.UserPermissions.AddAsync(userPermission);
                        }
                    }
                }

                userHasGroup.Group = group;
                await _context.SaveChangesAsync();
                var UserDTO = _mapper.Map<User, UserGroupDTO>(userHasGroup);
                return Ok(UserDTO);
            }
            catch (Exception ex) { }

            return Ok(groupPermissionsDTO);
        }
    }
}
