using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _context;

        public GroupRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Group?> GetGroupByIdAsync(Guid id)
        {
            return await _context.Groups.Include(x => x.GroupPermissions).ThenInclude(x => x.Operations).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Group?> GetGroupByNameAsync(string groupName)
        {

            return await _context.Groups.FirstOrDefaultAsync(x => x.Name == groupName);
        }
        public async Task<List<Group>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task ValidateMenu(InsertGroupPermission body)
        {
            await MenuErrors.ValidateMenu(_context, body);
        }
        public async Task<Group?> GetGroupWithPermissionsAndOperationsByIdAsync(Guid groupId)
        {
            return await _context.Groups
                .Where(x => x.Id == groupId)
                .Include(x => x.GroupPermissions!)
                .ThenInclude(x => x.Operations)
                .FirstOrDefaultAsync();
        }
        public async Task<Group> CreateGroupAsync(CreateGroupViewModel body)
        {
            await MenuErrors.ValidateMenu(_context, body);

            var groupId = Guid.NewGuid();
            var group = new Group
            {
                Id = groupId,
                Name = body.GroupName,
                CreatedAt = DateTime.UtcNow.AddHours(-3),
                Description = body.Description is null ? null : body.Description,
            };
            await _context.Groups.AddAsync(group);

            foreach (var menuParent in body.Menus)
            {
                var groupPermissionParentId = Guid.NewGuid();

                var foundMenuParent = await _context.Menus
                    .Where(x => x.Id == menuParent.MenuId)
                    .FirstOrDefaultAsync();

                var addDotInOrder = foundMenuParent.Order + ".";
                var foundMenusChildrensInParent = await _context.Menus
                    .Where(x => x.Order.Contains(addDotInOrder))
                    .ToListAsync();

                var createGroupPermissionParent = new GroupPermission
                {
                    Id = groupPermissionParentId,
                    MenuIcon = foundMenuParent.Icon,
                    MenuName = foundMenuParent.Name,
                    MenuOrder = foundMenuParent.Order,
                    MenuRoute = foundMenuParent.Route,
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
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
                            var foundMenusChildrensInChildren = await _context.Menus.Where(x => x.Order.Contains(addDotInOrderChildren)).ToListAsync();

                            var createGroupPermissionChildren = new GroupPermission
                            {
                                Id = groupPermissionChildrenId,
                                MenuIcon = foundMenuChildren.Icon,
                                MenuName = foundMenuChildren.Name,
                                MenuOrder = foundMenuChildren.Order,
                                MenuRoute = foundMenuChildren.Route,
                                CreatedAt = DateTime.UtcNow.AddHours(-3),
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
                                    GroupName = body.GroupName,
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
                            GroupName = body.GroupName,
                            OperationName = foundOperation.Method
                        };
                        await _context.GroupOperations.AddAsync(createGroupOperationParent);
                    }
                }
            }


            return group;
        }
        public async Task<Group> NewGroupPermissionsAsync(Group group, InsertGroupPermission body)
        {
            await MenuErrors.ValidateMenu(_context, body);

            foreach (var menuParent in body.Menus)
            {
                var groupPermissionParentId = Guid.NewGuid();

                var foundMenuParent = await _context.Menus
                    .Where(x => x.Id == menuParent.MenuId)
                    .FirstOrDefaultAsync();

                var addDotInOrder = foundMenuParent.Order + ".";
                var foundMenusChildrensInParent = await _context.Menus
                    .Where(x => x.Order.Contains(addDotInOrder))
                    .ToListAsync();

                var createGroupPermissionParent = new GroupPermission
                {
                    Id = groupPermissionParentId,
                    MenuIcon = foundMenuParent.Icon,
                    MenuName = foundMenuParent.Name,
                    MenuOrder = foundMenuParent.Order,
                    MenuRoute = foundMenuParent.Route,
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
                    Menu = foundMenuParent,
                    GroupName = group.Name,
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
                            var foundMenusChildrensInChildren = await _context.Menus.Where(x => x.Order.Contains(addDotInOrderChildren)).ToListAsync();

                            var createGroupPermissionChildren = new GroupPermission
                            {
                                Id = groupPermissionChildrenId,
                                MenuIcon = foundMenuChildren.Icon,
                                MenuName = foundMenuChildren.Name,
                                MenuOrder = foundMenuChildren.Order,
                                MenuRoute = foundMenuChildren.Route,
                                CreatedAt = DateTime.UtcNow.AddHours(-3),
                                Menu = foundMenuChildren,
                                GroupName = group.Name,
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
                                    GroupName = group.Name,
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
                            GroupName = group.Name,
                            OperationName = foundOperation.Method
                        };
                        await _context.GroupOperations.AddAsync(createGroupOperationParent);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return group;
        }
        public async Task InsertUserInGroupAsync(Group group, User userHasGroup, List<GroupPermissionsDTO> groupPermissionsDTO)
        {
            foreach (var permission in groupPermissionsDTO)
            {
                var groupPermission = await _context.GroupPermissions
                    .Where(x => x.Id == permission.Id)
                    .FirstOrDefaultAsync();

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
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
                };

                await _context.UserPermissions.AddAsync(userPermission);

                foreach (var operation in permission.Operations)
                {
                    var foundOperation = await _context.GlobalOperations
                        .Where(x => x.Method == operation.OperationName)
                        .FirstOrDefaultAsync();

                    var userOperation = new UserOperation
                    {
                        Id = Guid.NewGuid(),
                        OperationName = operation.OperationName,
                        UserPermission = userPermission,
                        GlobalOperation = foundOperation,
                        GroupName = group.Name
                    };

                    await _context.UserOperations.AddAsync(userOperation);
                }
            }
        }
        public void UpdateUser(User user)
        {
            _context.Update(user);
        }
        public void UpdateGroup(Group group)
        {
            _context.Groups.Update(group);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
