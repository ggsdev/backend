using AutoMapper;
using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.Http.Services
{
    public class GroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupPermissionRepository _groupPermissionRepository;
        private readonly IGroupOperationRepository _groupOperationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IUserOperationRepository _userOperationRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, IGroupPermissionRepository groupPermissionRepository, IUserRepository userRespository, IUserPermissionRepository userPermissionRepository, IUserOperationRepository userOperationRepository, IGroupOperationRepository groupOperationRepository)
        {
            _groupRepository = groupRepository;
            _groupPermissionRepository = groupPermissionRepository;
            _groupOperationRepository = groupOperationRepository;
            _userRepository = userRespository;
            _userPermissionRepository = userPermissionRepository;
            _userOperationRepository = userOperationRepository;
            _mapper = mapper;
        }

        public async Task<GroupWithMenusDTO> CreateGroup(CreateGroupViewModel body)
        {
            if (body.GroupName is null)
                throw new ConflictException("Group Name is Required.");

            var foundGroup = await _groupRepository.GetGroupByNameAsync(body.GroupName);
            if (foundGroup is not null)
                throw new ConflictException("Group Name is already exists.");

            var group = await _groupRepository.CreateGroupAsync(body);
            await _groupRepository.SaveChangesAsync();

            var returnGroup = await _groupRepository.GetGroupWithPermissionsAndOperationsByIdAsync(group.Id);
            var returnGroupDTO = _mapper.Map<Group, GroupWithMenusDTO>(returnGroup);

            return returnGroupDTO;
        }

        public async Task<UserGroupDTO> InsertUserInGroup(Guid groupId, Guid userId, User userLoggedIn)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (group == null)
                throw new NotFoundException("Group is not found.");

            var userHasGroup = await _userRepository.GetUserWithGroupAndPermissionsAsync(userId);
            if (userHasGroup == null)
                throw new NotFoundException("User is not found.");

            if (userHasGroup.Group != null)
                throw new ConflictException("User is already in a group.");

            var groupPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(groupId);
            var groupPermissionsDTO = _mapper.Map<List<GroupPermission>, List<GroupPermissionsDTO>>(groupPermissions);

            try
            {
                await _groupRepository.InsertUserInGroupAsync(group, userHasGroup, groupPermissionsDTO);

                //var beforeChangesUser = _mapper.Map<UserHistoryDTO>(userHasGroup);
                //var currentData = _mapper.Map<User, UserHistoryDTO>(userHasGroup);
                //currentData.updatedAt = DateTime.UtcNow;

                //var history = new SystemHistory
                //{
                //    Table = HistoryColumns.TableGroups,
                //    TypeOperation = HistoryColumns.Update,
                //    CreatedBy = userHasGroup?.Id,
                //    TableItemId = userId,
                //    CurrentData = currentData,
                //    PreviousData = beforeChangesUser,
                //    UpdatedBy = userLoggedIn?.Id,
                //};

                //await _context.SystemHistories.AddAsync(history);

                await _groupRepository.SaveChangesAsync();
                userHasGroup.IsPermissionDefault = true;
                userHasGroup.Group = group;
                await _userRepository.UpdateUser(userHasGroup);
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
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (group == null)
                throw new NotFoundException("Group is not found.");

            var userHasGroup = await _userRepository.GetUserWithGroupAndPermissionsAsync(userId);

            if (userHasGroup == null)
                throw new NotFoundException("User is not found.");

            if (userHasGroup.Group == null)
                throw new ConflictException("User no have a group");

            var permissionsUser = await _userPermissionRepository.GetUserPermissionsByUserId(userId);
            await _userPermissionRepository.RemoveUserPermissions(permissionsUser);

            var operationsUser = await _userOperationRepository.GetUserOperationsByUserId(userId);
            await _userOperationRepository.RemoveUserOperations(operationsUser);

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

            _groupRepository.UpdateUser(userHasGroup);
            await _groupRepository.SaveChangesAsync();

        }
        public async Task<List<GroupDTO>> GetGroups()
        {
            var groups = await _groupRepository.GetGroups();
            var groupsDTO = _mapper.Map<List<Group>, List<GroupDTO>>(groups);
            return groupsDTO;
        }

        public async Task<GroupDTO> GetGroupById(Guid id)
        {
            Console.WriteLine("oi");
            var group = await _groupRepository.GetGroupByIdAsync(id);

            if (group is null)
                throw new NotFoundException("Group not found");

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);
            return groupDTO;
        }

        public async Task<GroupDTO> UpdateGroup(Guid id, UpdateGroupViewModel body)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

            if (group is null)
                throw new NotFoundException("Group not found");

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(group, body);

            if (updatedProperties.Any() is false)
                throw new BadRequestException("No properties were updated, try other values");

            if (updatedProperties.TryGetValue("name", out var groupName))
            {
                var userPermissions = await _userPermissionRepository.GetUserPermissionsByGroupId();

                for (int i = 0; i < userPermissions.Count; ++i)
                    userPermissions[i].GroupName = groupName.ToString();

                var groupPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(id);

                for (int i = 0; i < groupPermissions.Count; ++i)
                    groupPermissions[i].GroupName = groupName.ToString();

                var groupOperations = await _groupOperationRepository.GetGroupOperationsByGroupId(id);

                for (int i = 0; i < groupOperations.Count; ++i)
                    groupOperations[i].GroupName = groupName.ToString();

                var userOperations = await _userOperationRepository.GetUserOperationsByGroupId(id);

                for (int i = 0; i < userOperations.Count; ++i)
                    userOperations[i].GroupName = groupName.ToString();

                _userPermissionRepository.UpdateUserPermissions(userPermissions);
                _groupPermissionRepository.UpdateGroupPermissions(groupPermissions);
                _groupOperationRepository.UpdateGroupOperations(groupOperations);
                _userOperationRepository.UpdateUserOperations(userOperations);
                await _groupRepository.SaveChangesAsync();
            }

            _groupRepository.UpdateGroup(group);
            await _groupRepository.SaveChangesAsync();

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);

            return groupDTO;
        }

        public async Task DeleteGroup(Guid id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);
            if (group is null || group.IsActive is false)
                throw new NotFoundException("Group not found or inactive already");

            group.IsActive = false;
            group.DeletedAt = DateTime.UtcNow;

            _groupRepository.UpdateGroup(group);

            for (int i = 0; i < group.User?.Count; ++i)
            {
                group.User[i].LastGroupId = group.Id;
                group.User[i].Group = null;
            }

            var userPermissions = await _userPermissionRepository.GetUserPermissionsByGroupId(id);

            var groupPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(id);

            var changedDate = DateTime.UtcNow;
            for (int i = 0; i < groupPermissions.Count; ++i)
            {
                groupPermissions[i].IsActive = false;
                groupPermissions[i].DeletedAt = changedDate;
                groupPermissions[i].UpdatedAt = changedDate;
            }

            _groupPermissionRepository.UpdateGroupPermissions(groupPermissions);
            await _userPermissionRepository.RemoveUserPermissions(userPermissions);

            await _groupRepository.SaveChangesAsync();
        }

        public async Task RestoreGroup(Guid id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

            if (group is null || group.IsActive is true)
                throw new NotFoundException("Group not found or active already");

            group.IsActive = true;
            group.DeletedAt = null;
            _groupRepository.UpdateGroup(group);

            var groupPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(id);

            var users = await _userRepository.GetUsersByLastGroupId(id);

            var changedDate = DateTime.UtcNow;

            foreach (var user in users)
            {
                user.Group = group;

                for (int i = 0; i < groupPermissions.Count; ++i)
                {
                    groupPermissions[i].IsActive = true;
                    groupPermissions[i].DeletedAt = null;
                    groupPermissions[i].UpdatedAt = changedDate;

                    _groupPermissionRepository.UpdateGroupPermission(groupPermissions[i]);

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

                        await _userOperationRepository.AddUserOperation(userOperation);
                    }

                    await _userPermissionRepository.AddUserPermission(userPermission);
                }
            }
        }
    }
}