using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.SystemHistories.Dtos.UserDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.Http.Services
{
    public class GroupService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GroupService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GroupWithMenusDTO> CreateGroup(CreateGroupViewModel body)
        {

            var foundGroup = await _context.Groups
                .Where(x => x.Name == body.GroupName)
                .FirstOrDefaultAsync();

            if (foundGroup != null)
                throw new ConflictException("Group Name is already exists.");

            await MenuErrors.ValidateMenu(_context, body);

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
                            var foundMenusChildrensInChildren = await _context.Menus.Where(x => x.Order.Contains(addDotInOrderChildren)).ToListAsync();

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

            await _context.SaveChangesAsync();

            var returnGroup = await _context.Groups
                .Where(x => x.Id == groupId)
                .Include(x => x.GroupPermissions)
                .ThenInclude(x => x.Operations)
                .FirstOrDefaultAsync();

            var returnGroupDTO = _mapper.Map<Group, GroupWithMenusDTO>(returnGroup!);

            return returnGroupDTO;
        }

        public async Task<UserGroupDTO> InsertUserInGroup(Guid groupId, Guid userId, User userLoggedIn)
        {
            var group = await _context.Groups
                .Where(x => x.Id == groupId)
                .FirstOrDefaultAsync();
            if (group == null)
                throw new NotFoundException("Group is not found.");

            var userHasGroup = await _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.Group)
                .Include(x => x.UserPermissions)
                .FirstOrDefaultAsync();

            if (userHasGroup == null)
                throw new NotFoundException("User is not found.");

            if (userHasGroup.Group != null)
                throw new ConflictException("User is already a group");

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
                        CreatedAt = DateTime.UtcNow,
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
                            GlobalOperation = foundOperation,
                            GroupName = group.Name
                        };
                        await _context.UserOperations.AddAsync(userOperation);
                    }
                }
                var beforeChangesUser = _mapper.Map<UserHistoryDTO>(userHasGroup);

                userHasGroup.Group = group;
                userHasGroup.LastGroupId = null;
                _context.Update(userHasGroup);

                var currentData = _mapper.Map<User, UserHistoryDTO>(userHasGroup);
                currentData.updatedAt = DateTime.UtcNow;

                var history = new SystemHistory
                {
                    Table = HistoryColumns.TableGroups,
                    TypeOperation = HistoryColumns.Update,
                    CreatedBy = userHasGroup?.Id,
                    TableItemId = userId,
                    CurrentData = currentData,
                    PreviousData = beforeChangesUser,
                    UpdatedBy = userLoggedIn?.Id,
                };

                await _context.SystemHistories.AddAsync(history);

                await _context.SaveChangesAsync();
                var userDTO = _mapper.Map<User, UserGroupDTO>(userHasGroup);
                return userDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public async Task RemoveUserInGroup(Guid groupId, Guid userId, User userLoggedIn)
        {
            var group = await _context.Groups
               .Where(x => x.Id == groupId)
               .FirstOrDefaultAsync();
            if (group == null)
                throw new NotFoundException("Group is not found.");

            var userHasGroup = await _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.Group)
                .Include(x => x.UserPermissions)
                .FirstOrDefaultAsync();

            if (userHasGroup == null)
                throw new NotFoundException("User is not found.");

            if (userHasGroup.Group == null)
                throw new ConflictException("User no have a group");

            var permissionsUser = await _context.UserPermissions.Include(x => x.User).Where(x => x.User.Id == userId).ToListAsync();
            _context.RemoveRange(permissionsUser);

            var operationsUser = await _context.UserOperations.Include(x => x.UserPermission).ThenInclude(x => x.User).Where(x => x.UserPermission.User.Id == userId).ToListAsync();
            _context.RemoveRange(operationsUser);

            userHasGroup.Group = null;
            userHasGroup.LastGroupId = group.Id;

            //var history = new SystemHistory
            //{
            //    Table = HistoryColumns.TableGroups,
            //    TypeOperation = HistoryColumns.Delete,
            //    CreatedBy = userHasGroup?.Id,
            //    TableItemId = userId,
            //    CurrentData = DateTime.UtcNow,
            //    PreviousData = beforeChangesUser,
            //    UpdatedBy = userLoggedIn?.Id,
            //};

            _context.Update(userHasGroup);
            await _context.SaveChangesAsync();

        }
        public async Task<List<GroupDTO>> GetGroups()
        {
            var groups = await _context.Groups.ToListAsync();
            var groupsDTO = _mapper.Map<List<Group>, List<GroupDTO>>(groups);
            return groupsDTO;
        }

        public async Task<GroupDTO> GetGroupById(Guid id)
        {
            var group = await _context.Groups
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group is null)
                throw new NotFoundException("Group not found");

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);
            return groupDTO;
        }

        public async Task<GroupDTO> UpdateGroup(Guid id, UpdateGroupViewModel body)
        {
            var group = await _context.Groups
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group is null)
                throw new NotFoundException("Group not found");

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(group, body);

            if (updatedProperties.Any() is false)
                throw new BadRequestException("No properties were updated, try other values");

            if (updatedProperties.TryGetValue("name", out var groupName))
            {
                var userPermissions = await _context.UserPermissions
                    .Where(x => x.GroupId == id)
                    .ToListAsync();

                for (int i = 0; i < userPermissions.Count; ++i)
                    userPermissions[i].GroupName = groupName.ToString();

                var groupPermissions = await _context.GroupPermissions
                    .Include(x => x.Group)
                    .Where(x => x.Group.Id == id)
                    .ToListAsync();

                for (int i = 0; i < groupPermissions.Count; ++i)
                    groupPermissions[i].GroupName = groupName.ToString();

                var groupOperations = await _context.GroupOperations
                    .Include(x => x.GroupPermission)
                    .ThenInclude(x => x.Group)
                    .Where(x => x.GroupPermission.Group.Id == id)
                    .ToListAsync();

                for (int i = 0; i < groupOperations.Count; ++i)
                    groupOperations[i].GroupName = groupName.ToString();

                var userOperations = await _context.UserOperations
                    .Include(x => x.UserPermission)
                    .Where(x => x.UserPermission.GroupId == id)
                    .ToListAsync();

                for (int i = 0; i < userOperations.Count; ++i)
                    userOperations[i].GroupName = groupName.ToString();

                _context.UserPermissions.UpdateRange(userPermissions);
                _context.GroupPermissions.UpdateRange(groupPermissions);
                _context.GroupOperations.UpdateRange(groupOperations);
                _context.UserOperations.UpdateRange(userOperations);
            }

            _context.Groups.Update(group);
            await _context.SaveChangesAsync();

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);

            return groupDTO;
        }

        public async Task DeleteGroup(Guid id)
        {
            var group = await _context.Groups
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group is null || group.IsActive is false)
                throw new NotFoundException("Group not found or inactive already");

            group.IsActive = false;
            group.DeletedAt = DateTime.UtcNow;
            _context.Update(group);

            for (int i = 0; i < group.User?.Count; ++i)
            {
                group.User[i].LastGroupId = group.Id;
                group.User[i].Group = null;
            }

            var userPermissions = await _context.UserPermissions
                    .Where(x => x.GroupId == id)
                    .ToListAsync();

            var groupPermissions = await _context.GroupPermissions
               .Include(x => x.Group)
               .Where(x => x.Group.Id == id)
               .ToListAsync();

            var changedDate = DateTime.UtcNow;
            for (int i = 0; i < groupPermissions.Count; ++i)
            {
                groupPermissions[i].IsActive = false;
                groupPermissions[i].DeletedAt = changedDate;
                groupPermissions[i].UpdatedAt = changedDate;
            }

            _context.UpdateRange(groupPermissions);
            _context.RemoveRange(userPermissions);

            await _context.SaveChangesAsync();
        }

        public async Task RestoreGroup(Guid id)
        {
            var group = await _context.Groups
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group is null || group.IsActive is true)
                throw new NotFoundException("Group not found or active already");

            group.IsActive = true;
            group.DeletedAt = null;
            _context.Update(group);

            var groupPermissions = await _context.GroupPermissions
               .Include(x => x.Group)
               .Include(x => x.Menu)
               .Include(x => x.Operations)
               .Where(x => x.Group.Id == id)
               .ToListAsync();

            var users = await _context.Users
                   .Where(x => x.LastGroupId == id)
                   .ToListAsync();

            var changedDate = DateTime.UtcNow;

            foreach (var user in users)
            {
                user.Group = group;

                for (int i = 0; i < groupPermissions.Count; ++i)
                {
                    groupPermissions[i].IsActive = true;
                    groupPermissions[i].DeletedAt = null;
                    groupPermissions[i].UpdatedAt = changedDate;

                    _context.Update(groupPermissions[i]);

                    var userPermission = new UserPermission
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        GroupId = group.Id,
                        GroupName = group.Name,
                        GroupMenu = groupPermissions[i],
                        MenuIcon = groupPermissions[i].MenuIcon,
                        MenuId = groupPermissions[i].Menu?.Id,
                        MenuName = groupPermissions[i].Menu?.Name,
                        MenuOrder = groupPermissions[i].Menu?.Order,
                        MenuRoute = groupPermissions[i].Menu?.Route,
                        hasChildren = groupPermissions[i].hasChildren,
                        hasParent = groupPermissions[i].hasParent,
                        User = user,
                    };

                    foreach (var operation in groupPermissions[i].Operations)
                    {
                        var userOperation = new UserOperation
                        {
                            Id = Guid.NewGuid(),
                            OperationName = operation?.OperationName,
                            GlobalOperation = operation?.GlobalOperation,
                            UserPermission = userPermission,
                        };

                        await _context.AddAsync(userOperation);
                    }

                    await _context.AddAsync(userPermission);
                }
            }
            _context.UpdateRange(users);


            await _context.SaveChangesAsync();
        }
    }
}