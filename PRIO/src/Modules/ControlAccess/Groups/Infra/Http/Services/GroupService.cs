using AutoMapper;
using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Factories;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.Interfaces;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Menus.Interfaces;
using PRIO.src.Modules.ControlAccess.Operations.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.UserDtos;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
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
        private readonly IMenuRepository _menuRepository;
        private readonly IGlobalOperationsRepository _globalOperationRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly GroupFactory _groupFactory;
        private readonly GroupPermissionFactory _groupPermissionFactory;
        private readonly GroupOperationFactory _groupOperationFactory;
        private readonly UserPermissionFactory _userPermissionFactory;
        private readonly UserOperationFactory _userOperationFactory;
        private readonly IMapper _mapper;


        public GroupService(IGroupRepository groupRepository, IMapper mapper, IGroupPermissionRepository groupPermissionRepository, IUserRepository userRespository, IUserPermissionRepository userPermissionRepository, IUserOperationRepository userOperationRepository, IGroupOperationRepository groupOperationRepository, SystemHistoryService systemHistoryService, GroupFactory groupFactory, IMenuRepository menuRepository, IGlobalOperationsRepository globalOperationRepository, GroupOperationFactory groupOperationFactory, GroupPermissionFactory groupPermissionFactory, UserPermissionFactory userPermissionFactory, UserOperationFactory userOperationFactory)
        {
            _groupRepository = groupRepository;
            _groupPermissionRepository = groupPermissionRepository;
            _groupOperationRepository = groupOperationRepository;
            _userRepository = userRespository;
            _userPermissionRepository = userPermissionRepository;
            _userOperationRepository = userOperationRepository;
            _mapper = mapper;
            _systemHistoryService = systemHistoryService;
            _groupFactory = groupFactory;
            _menuRepository = menuRepository;
            _globalOperationRepository = globalOperationRepository;
            _groupOperationFactory = groupOperationFactory;
            _groupPermissionFactory = groupPermissionFactory;
            _userPermissionFactory = userPermissionFactory;
            _userOperationFactory = userOperationFactory;
        }

        public async Task<GroupWithMenusDTO> CreateGroup(CreateGroupViewModel body, User loggedUser)
        {
            if (body.Name is null)
                throw new ConflictException("Group Name is Required.");

            var foundGroup = await _groupRepository.GetGroupByNameAsync(body.Name);
            if (foundGroup is not null)
                throw new ConflictException("Group Name is already exists.");

            var group = _groupFactory.CreateGroup(body.Name, body.Description);
            await _groupRepository.AddAsync(group);

            await AddPermissionInGroupAsync(body, group);
            await _groupRepository.SaveChangesAsync();

            var returnGroup = await _groupRepository.GetGroupWithPermissionsAndOperationsByIdAsync(group.Id);
            var returnGroupDTO = _mapper.Map<Group, GroupWithMenusDTO>(returnGroup);

            await _systemHistoryService.Create<Group, GroupHistoryDTO>(HistoryColumns.TableGroups, loggedUser, group.Id, group);

            return returnGroupDTO;
        }
        public async Task<Group> AddPermissionInGroupAsync(CreateGroupViewModel body, Group group)
        {
            await _groupRepository.ValidateMenusByCreateViewModel(body);

            foreach (var menuParent in body.Menus)
            {
                var foundMenuParent = await _menuRepository.GetByMenuId(menuParent.MenuId);
                var addDotInOrder = foundMenuParent.Order + ".";

                var foundMenusChildrensInParent = await _menuRepository.GetChildrensByOrder(addDotInOrder);
                var createGroupPermissionParent = _groupPermissionFactory.CreateGroupPermission(foundMenuParent.Icon, foundMenuParent.Name, foundMenuParent.Order, foundMenuParent.Route, foundMenuParent, body.Name, foundMenusChildrensInParent, group);

                await _groupPermissionRepository.AddAsync(createGroupPermissionParent);

                if (menuParent.Childrens is not null)
                {
                    if (menuParent.Childrens.Count != 0)
                    {
                        foreach (var menuChildren in menuParent.Childrens)
                        {
                            var foundMenuChildren = await _menuRepository.GetByMenuId(menuChildren.ChildrenId);

                            var addDotInOrderChildren = foundMenuChildren.Order + ".";
                            var foundMenusChildrensInChildren = await _menuRepository.GetChildrensByOrder(addDotInOrderChildren);

                            var createGroupPermissionChildren = _groupPermissionFactory.CreateGroupPermission(foundMenuChildren.Icon, foundMenuChildren.Name, foundMenuChildren.Order, foundMenuChildren.Route, foundMenuChildren, body.Name, foundMenusChildrensInChildren, group);
                            await _groupPermissionRepository.AddAsync(createGroupPermissionChildren);

                            foreach (var operationsChildren in menuChildren.Operations)
                            {
                                var foundOperation = await _globalOperationRepository.GetGlobalOperationById(operationsChildren.OperationId);
                                var createGroupOperationChildren = _groupOperationFactory.CreateGroupOperation(foundOperation, createGroupPermissionChildren, body.Name, foundOperation.Method);
                                await _groupOperationRepository.AddAsync(createGroupOperationChildren);
                            }
                        }
                    }
                }
                if (createGroupPermissionParent.hasChildren == false)
                {
                    foreach (var operationsParent in menuParent.Operations)
                    {
                        var foundOperation = await _globalOperationRepository.GetGlobalOperationById(operationsParent.OperationId);
                        var createGroupOperationParent = _groupOperationFactory.CreateGroupOperation(foundOperation, createGroupPermissionParent, body.Name, foundOperation.Method);
                        await _groupOperationRepository.AddAsync(createGroupOperationParent);
                    }
                }
            }

            return group;
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
                await InsertUserInGroup(group, userHasGroup, groupPermissionsDTO);

                //var beforeChangesUser = _mapper.Map<UserHistoryDTO>(userHasGroup);
                //var currentData = _mapper.Map<User, UserHistoryDTO>(userHasGroup);
                //currentData.updatedAt = DateTime.UtcNow.AddHours(-3);

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

                userHasGroup.IsPermissionDefault = true;
                userHasGroup.Group = group;
                _userRepository.UpdateUser(userHasGroup);

                await _groupRepository.SaveChangesAsync();
                var userDTO = _mapper.Map<User, UserGroupDTO>(userHasGroup);
                return userDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task InsertUserInGroup(Group group, User userHasGroup, List<GroupPermissionsDTO> groupPermissionsDTO)
        {
            foreach (var permission in groupPermissionsDTO)
            {
                var groupPermission = await _groupPermissionRepository.GetGroupPermissionById(permission.Id);

                var userPermission = _userPermissionFactory.CreateUserPermission(permission, userHasGroup, groupPermission);
                await _userPermissionRepository.AddUserPermission(userPermission);

                foreach (var operation in permission.Operations)
                {
                    var foundOperation = await _globalOperationRepository.GetGlobalOperationByMetlhod(operation.OperationName);
                    var userOperation = _userOperationFactory.CreateUserOperation(operation, userPermission, foundOperation, group);
                    await _userOperationRepository.AddUserOperation(userOperation);
                }
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

            if (userHasGroup.Type == "Master")
                throw new NotFoundException("Usuário Master não pode sofrer alteração no grupo.");

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
            //    CurrentData = DateTime.UtcNow.AddHours(-3),
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
        public async Task<GroupWithGroupPermissionDTO> GetGroupById(Guid id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

            if (group is null)
                throw new NotFoundException("Group not found");

            group.GroupPermissions = group.GroupPermissions.OrderBy(x => x.MenuOrder).ToList();
            var groupDTO = _mapper.Map<Group, GroupWithGroupPermissionDTO>(group);
            var parentElements = new List<GroupPermissionParentDTO>();

            foreach (var gpermission in groupDTO.GroupPermissions)
            {
                if (gpermission.hasParent == false && gpermission.hasChildren == true)
                {
                    gpermission.Children = new List<GroupPermissionChildrenDTO>();
                    parentElements.Add(gpermission);
                }
                else if (gpermission.hasParent == true && gpermission.hasChildren == false)
                {
                    var parentOrder = gpermission.MenuOrder.Split('.')[0];
                    var parentElement = parentElements.FirstOrDefault(x => x.MenuOrder.StartsWith(parentOrder));
                    if (parentElement != null)
                    {
                        var childrenDTO = _mapper.Map<GroupPermissionParentDTO, GroupPermissionChildrenDTO>(gpermission);
                        parentElement.Children.Add(childrenDTO);
                        parentElements.Remove(gpermission);
                    }
                }

                foreach (var operation in gpermission.Operations)
                {
                    var operationName = operation.OperationName;
                }
            }



            groupDTO.GroupPermissions.RemoveAll(permission => permission.MenuOrder.Contains("."));
            return groupDTO;
        }
        public async Task<GroupDTO> UpdateGroup(Guid id, UpdateGroupViewModel body, User loggedUser)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);

            if (group is null)
                throw new NotFoundException("Group not found");

            if (group.Name == "Master")
                throw new ConflictException("Grupo Master não pode ser editado.");

            var beforeChanges = _mapper.Map<Group, GroupHistoryDTO>(group);
            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(group, body);

            if (updatedProperties.Any() is false)
                throw new BadRequestException("No properties were updated, try other values");

            if (updatedProperties.TryGetValue("groupname", out var groupName))
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

            await _systemHistoryService.Update(HistoryColumns.TableGroups, loggedUser, updatedProperties, group.Id, group, beforeChanges);

            _groupRepository.UpdateGroup(group);
            await _groupRepository.SaveChangesAsync();

            var groupDTO = _mapper.Map<Group, GroupDTO>(group);

            return groupDTO;
        }
        public async Task<GroupDTO> EditPermissionGroup(Guid id, InsertGroupPermission body)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id) ?? throw new NotFoundException("Group not found");
            if (group.Name == "Master")
                throw new ConflictException("Grupo Master não pode ser alterado.");
            //REMOVE USER PERMISSIONS
            var users = group.User;
            foreach (var user in users)
            {
                var operationsUser = await _userOperationRepository.GetUserOperationsByUserId(user.Id);
                await _userOperationRepository.RemoveUserOperations(operationsUser);
                var permissionsUser = await _userPermissionRepository.GetUserPermissionsByUserId(user.Id);
                await _userPermissionRepository.RemoveUserPermissions(permissionsUser);
            }

            //REMOVE GROUP PERMISSIONS
            var gOperations = await _groupOperationRepository.GetGroupOperationsByGroupId(id);
            await _groupOperationRepository.RemoveGroupOperations(gOperations);
            var gPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(id);
            await _groupPermissionRepository.RemoveGroupPermissions(gPermissions);

            //ADD GROUP PERMISSIONS
            await _groupRepository.NewGroupPermissionsAsync(group, body);
            var groupPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(id);

            //ADD USER PERMISSIONS
            var changedDate = DateTime.UtcNow.AddHours(-3);
            foreach (var user in users)
            {
                user.Group = group;

                for (int i = 0; i < groupPermissions.Count; ++i)
                {
                    var groupPermissionsDTO = _mapper.Map<GroupPermission, GroupPermissionsDTO>(groupPermissions[i]);

                    var userPermission = _userPermissionFactory.CreateUserPermission(groupPermissionsDTO, user, groupPermissions[i]);
                    await _userPermissionRepository.AddUserPermission(userPermission);

                    foreach (var operation in groupPermissions[i].Operations)
                    {
                        var groupOperationsDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(operation);
                        var userOperation = _userOperationFactory.CreateUserOperation(groupOperationsDTO, userPermission, operation.GlobalOperation, group);

                        await _userOperationRepository.AddUserOperation(userOperation);
                    }
                }
            }

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
            group.DeletedAt = DateTime.UtcNow.AddHours(-3);

            _groupRepository.UpdateGroup(group);

            for (int i = 0; i < group.User?.Count; ++i)
            {
                group.User[i].LastGroupId = group.Id;
                group.User[i].Group = null;
            }

            var userPermissions = await _userPermissionRepository.GetUserPermissionsByGroupId(id);
            var groupPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(id);
            var changedDate = DateTime.UtcNow.AddHours(-3);
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

            var changedDate = DateTime.UtcNow.AddHours(-3);
            foreach (var user in users)
            {
                user.Group = group;

                for (int i = 0; i < groupPermissions.Count; ++i)
                {
                    groupPermissions[i].IsActive = true;
                    groupPermissions[i].DeletedAt = null;
                    groupPermissions[i].UpdatedAt = changedDate;

                    _groupPermissionRepository.UpdateGroupPermission(groupPermissions[i]);

                    var groupPermissionsDTO = _mapper.Map<GroupPermission, GroupPermissionsDTO>(groupPermissions[i]);

                    var userPermission = _userPermissionFactory.CreateUserPermission(groupPermissionsDTO, user, groupPermissions[i]);
                    await _userPermissionRepository.AddUserPermission(userPermission);

                    foreach (var operation in groupPermissions[i].Operations)
                    {
                        var groupOperationsDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(operation);

                        var userOperation = _userOperationFactory.CreateUserOperation(groupOperationsDTO, userPermission, operation.GlobalOperation, group);
                        await _userOperationRepository.AddUserOperation(userOperation);
                    }
                }
            }
            await _groupRepository.SaveChangesAsync();
        }
    }
}