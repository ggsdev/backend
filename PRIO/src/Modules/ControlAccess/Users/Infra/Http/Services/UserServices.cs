﻿using AutoMapper;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.UserDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;
        private IUserPermissionRepository _userPermissionRepository;
        private IUserOperationRepository _userOperationRepository;
        private IGroupPermissionRepository _groupPermissionRepository;
        private IGroupOperationRepository _groupOperationRepository;
        private IGlobalOperationsRepository _globalOperationsRepository;
        private ISystemHistoryRepository _systemHistoryRepository;
        private IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository user, ISystemHistoryRepository systemHistoryRepository, IUserPermissionRepository userPermissionRepository, IUserOperationRepository userOperationRepository, IGroupPermissionRepository groupPermissionRepository, IGroupOperationRepository groupOperationRepository, IGlobalOperationsRepository globalOperationsRepository)
        {
            _mapper = mapper;
            _userRepository = user;
            _systemHistoryRepository = systemHistoryRepository;
            _userPermissionRepository = userPermissionRepository;
            _userOperationRepository = userOperationRepository;
            _groupPermissionRepository = groupPermissionRepository;
            _groupOperationRepository = groupOperationRepository;
            _globalOperationsRepository = globalOperationsRepository;
        }


        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var userDTOS = _mapper.Map<List<User>, List<UserDTO>>(users);

            return userDTOS;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserViewModel body, User loggedUser)
        {
            var userInAd = ActiveDirectory
                .CheckUserExistsInActiveDirectory(body.Username);
            if (userInAd is false)
                throw new NotFoundException("Não foi possível validar o usuário no domínio, digite um usuário valido");

            var userInDatabase = await _userRepository
                .GetUserByUsername(body.Username);
            if (userInDatabase != null)
                throw new ConflictException("Usuário já cadastrado");

            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Name = body.Name,
                Username = body.Username,
                Description = body.Description is not null ? body.Description : null,
            };

            await _userRepository
                .CreateUser(user);

            var currentData = _mapper.Map<User, UserHistoryDTO>(user);

            var dateNow = DateTime.UtcNow;

            currentData.createdAt = dateNow;
            currentData.updatedAt = dateNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableUsers,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = loggedUser?.Id,
                TableItemId = userId,
                CurrentData = currentData,
            };

            await _systemHistoryRepository.AddAsync(history);

            await _userRepository.SaveChangesAsync();
            var userDTO = _mapper.Map<User, UserDTO>(user);

            return userDTO;
        }

        public async Task<ProfileDTO?> ProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user is null)
                throw new NotFoundException("User not found");

            if (userId.ToString() != user.Id.ToString())
                throw new ConflictException("User don't have permission to do that.");


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
            return userDTO;
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null)
                throw new NotFoundException("User not found");

            var userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO?> UpdateUserByIdAsync(Guid id, UpdateUserViewModel body)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null || user.IsActive is false)
                throw new NotFoundException("User not found");

            var beforeChangesUser = _mapper.Map<UserHistoryDTO>(user);
            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(user, body);

            //var userId = (Guid)HttpContext.Items["Id"]!;
            //var userOperation = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            //if (userOperation is null)
            //    return NotFound(new ErrorResponseDTO
            //    {
            //        Message = $"User is not found"
            //    });            

            user.Name = body.Name is not null ? body.Name : user.Name;
            user.Password = body.Password is not null ? BCrypt.Net.BCrypt.HashPassword(body.Password) : user.Password;
            user.Email = body.Email is not null ? body.Email : user.Email;
            user.Username = body.Username is not null ? body.Username : user.Username;

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();
            var userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public async Task DeleteUserByIdAsync(Guid id, Guid userOperationId)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null || user.IsActive is false)
                throw new NotFoundException("User not found");

            var userOperation = await _userRepository.GetUserById(userOperationId);
            if (userOperation is null)
                throw new NotFoundException("User not found");

            user.DeletedAt = DateTime.UtcNow;
            user.IsActive = false;

            await _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserDTO?> RestoreUserByIdAsync(Guid id, Guid userOperationId)
        {
            var user = await _userRepository.GetUserById(id);

            if (user is null || user.IsActive is true)
                throw new NotFoundException("User is already active.");

            var userOperation = await _userRepository.GetUserById(userOperationId);
            if (userOperation is null)
                throw new NotFoundException("User not found");

            user.IsActive = true;
            user.DeletedAt = null;

            await _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();

            var UserDTO = _mapper.Map<User, UserDTO>(user);
            return UserDTO;
        }

        public async Task<ProfileDTO?> EditPermissionUserByIdAsync(InsertUserPermissionViewModel body, Guid id)
        {
            if (body.Menus is null)
                throw new NotFoundException("Menus is not found");

            await _userRepository.ValidateMenu(body);

            var userWithPermissions = await _userRepository.GetUserById(id);
            if (userWithPermissions is null)
                throw new NotFoundException("User not found");
            if (userWithPermissions.Group is null)
                throw new NotFoundException("User no have found");

            var permissionsUser = await _userPermissionRepository.GetUserPermissionsByUserId(id);
            await _userPermissionRepository.RemoveUserPermissions(permissionsUser);

            var operationsUser = await _userOperationRepository.GetUserOperationsByUserId(id);
            await _userOperationRepository.RemoveUserOperations(operationsUser);


            foreach (var menu in body.Menus)
            {
                //PROCURANDO PERMISSAO NO GRUPO
                var verifyRelationMenuParent = await _groupPermissionRepository.GetGroupPermissionByMenuIdAndGroupId(menu.MenuId, userWithPermissions.Group.Id);


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
                    await _userPermissionRepository.AddUserPermission(createMenuParent);
                    // EXISTE A PERMISSAO NO GRUPO E TEM LISTA DE FILHO
                    if (menu.Childrens is not null && menu.Childrens.Count != 0)
                    {

                        foreach (var children in menu.Childrens)
                        {
                            //PROCURANDO PERMISSAO DO FILHO
                            var verifyRelationMenuChildren = await _groupPermissionRepository.GetGroupPermissionByMenuIdAndGroupId(children.ChildrenId, userWithPermissions.Group.Id);

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
                                    await _userPermissionRepository.AddUserPermission(createMenuChildren);

                                    foreach (var operation in children.Operations)
                                    {
                                        //PROCURANDO PERMISSAO DA OPERACAO NO GRUPO
                                        var verifyRelationMenuChildrenWithOperations = await _groupOperationRepository.GetGroupOperationsByMenuIdAndGroupPermissionId(operation.OperationId, verifyRelationMenuChildren.Id);

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
                                            await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                                        }
                                        else // NAO EXISTE PERMISSAO DA OPERACAO
                                        {
                                            var verifyRelationMenuChildrenWithOperationsInMaster = await _groupOperationRepository.GetGroupOperationsByOperationIdAndGroupName(operation.OperationId, "Master");

                                            var createGroupOperationsInUser = new UserOperation
                                            {
                                                OperationName = verifyRelationMenuChildrenWithOperationsInMaster.OperationName,
                                                UserPermission = createMenuChildren,
                                                GlobalOperation = verifyRelationMenuChildrenWithOperationsInMaster.GlobalOperation,
                                                GroupName = verifyRelationMenuChildrenWithOperationsInMaster.GroupName,
                                                Id = Guid.NewGuid(),
                                            };
                                            await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                                        }
                                    }
                                }
                            }
                            else // NÃO EXISTE PERMISSAO DO FILHO NO GRUPO 
                            {
                                if (children.Operations is not null && children.Operations.Count != 0)
                                {
                                    //PROCURANDO PERMISSAO PELO MASTER
                                    var verifyRelationMenuChildrenInMaster = await _groupPermissionRepository.GetGroupPermissionByMenuIdAndGroupName(children.ChildrenId, "Master");

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
                                    await _userPermissionRepository.AddUserPermission(createMenuChildren);

                                    foreach (var operation in children.Operations)
                                    {
                                        //PROCURANDO PERMISSAO DA OPERACAO PELO MASTER
                                        var verifyRelationMenuChildrenWithOperationsInMaster = await _groupOperationRepository.GetGroupOperationsByOperationIdAndGroupName(operation.OperationId, "Master");

                                        var createGroupOperationsInUser = new UserOperation
                                        {
                                            OperationName = verifyRelationMenuChildrenWithOperationsInMaster.OperationName,
                                            UserPermission = createMenuChildren,
                                            GlobalOperation = verifyRelationMenuChildrenWithOperationsInMaster.GlobalOperation,
                                            GroupName = verifyRelationMenuChildrenWithOperationsInMaster.GroupName,
                                            Id = Guid.NewGuid(),
                                        };
                                        await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
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
                                var verifyRelationMenuParentWithOperations = await _groupOperationRepository.GetGroupOperationsByMenuIdAndGroupPermissionId(operation.OperationId, verifyRelationMenuParent.Id);

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
                                    await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                                }
                                else // NAO EXISTE PERMISSAO DA OPERACAO
                                {
                                    var verifyRelationMenuChildrenWithOperationsInMaster = await _groupOperationRepository.GetGroupOperationsByOperationIdAndGroupName(operation.OperationId, "Master");

                                    var createGroupOperationsInUser = new UserOperation
                                    {
                                        OperationName = verifyRelationMenuChildrenWithOperationsInMaster.OperationName,
                                        UserPermission = createMenuParent,
                                        GlobalOperation = verifyRelationMenuChildrenWithOperationsInMaster.GlobalOperation,
                                        GroupName = verifyRelationMenuChildrenWithOperationsInMaster.GroupName,
                                        Id = Guid.NewGuid(),
                                    };
                                    await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                                }
                            }
                        }
                    }
                }
                else // NÃO EXISTE A PERMISSAO NO GRUPO
                {
                    var verifyRelationMenuParentInMaster = await _groupPermissionRepository.GetGroupPermissionByMenuIdAndGroupName(menu.MenuId, "Master");

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
                    await _userPermissionRepository.AddUserPermission(createMenuParent);

                    if (menu.Childrens is not null && menu.Childrens.Count != 0)
                    {
                        foreach (var children in menu.Childrens)
                        {
                            if (children.Operations is not null && children.Operations.Count != 0)
                            {

                                var verifyRelationMenuChildrenInMaster = await _groupPermissionRepository.GetGroupPermissionByMenuIdAndGroupName(children.ChildrenId, "Master");

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
                                await _userPermissionRepository.AddUserPermission(createMenuChildren);

                                foreach (var operation in children.Operations)
                                {
                                    //PROCURANDO PERMISSAO DA OPERACAO PELO MASTER
                                    var verifyRelationMenuParentWithOperationsInMaster = await _groupOperationRepository.GetGroupOperationsByOperationIdAndGroupName(operation.OperationId, "Master");

                                    var createGroupOperationsInUser = new UserOperation
                                    {
                                        OperationName = verifyRelationMenuParentWithOperationsInMaster.OperationName,
                                        UserPermission = createMenuChildren,
                                        GlobalOperation = verifyRelationMenuParentWithOperationsInMaster.GlobalOperation,
                                        GroupName = verifyRelationMenuParentWithOperationsInMaster.GroupName,
                                        Id = Guid.NewGuid(),
                                    };
                                    await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                                }
                            }
                        }
                    }
                }
                await _userRepository.SaveChangesAsync();
            }

            var gPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(userWithPermissions.Group.Id);
            var uPermissions = await _userPermissionRepository.GetUserPermissionsByUserId(id);
            var gPermissionsCount = gPermissions.Count();
            var uPermissionsCount = uPermissions.Count();
            if (uPermissionsCount != gPermissionsCount)
            {
                userWithPermissions.IsPermissionDefault = false;
            }
            else
            {
                int? verifyCount = 0;
                foreach (var gPermission in gPermissions)
                {
                    var verifyPermission = await _userPermissionRepository.GetUserPermissionByMenuNameAndUserId(gPermission.MenuName, userWithPermissions.Id);

                    if (verifyPermission is null)
                    {
                        userWithPermissions.IsPermissionDefault = false;
                    }
                    else
                    {
                        verifyCount++;
                    }
                }
                if (verifyCount != uPermissionsCount)
                {
                    userWithPermissions.IsPermissionDefault = false;
                }
                else
                {
                    userWithPermissions.IsPermissionDefault = true;
                }
            }

            var gOperations = await _groupOperationRepository.GetGroupOperationsByGroupId(userWithPermissions.Group.Id);
            var uOperations = await _userOperationRepository.GetUserOperationsByUserId(id);

            var gOperationsCount = gOperations.Count();
            var uOperationsCount = uOperations.Count();
            if (uOperationsCount != gOperationsCount)
            {
                userWithPermissions.IsPermissionDefault = false;
            }
            else
            {
                int? verifyCount = 0;
                foreach (var gOperation in gOperations) // PAREI AQUI !
                {
                    var verifyPermission = _userOperationRepository.GetUserOperationsByOperationNameMenuNameAndPermissionId(gOperation.OperationName, gOperation.GroupPermission.MenuName, userWithPermissions.Id);


                    if (verifyPermission is null)
                    {
                        userWithPermissions.IsPermissionDefault = false;
                    }
                    else
                    {
                        verifyCount++;
                    }
                }
                if (verifyCount != uOperationsCount)
                {
                    userWithPermissions.IsPermissionDefault = false;
                }
                else
                {
                    userWithPermissions.IsPermissionDefault = true;
                }
            }

            await _userRepository.SaveChangesAsync();
            var userDTO = _mapper.Map<User, ProfileDTO>(userWithPermissions);
            return userDTO;
        }

        public async Task<UserGroupDTO> RefreshPermissionUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserWithGroupAndPermissionsAsync(id);
            if (user == null)
                throw new NotFoundException("User is not found.");

            if (user.Group == null)
                throw new NotFoundException("User's group is not found.");

            var permissionsUser = await _userPermissionRepository.GetUserPermissionsByUserId(id);
            await _userPermissionRepository.RemoveUserPermissions(permissionsUser);

            var operationsUser = await _userOperationRepository.GetUserOperationsByUserId(id);
            await _userOperationRepository.RemoveUserOperations(operationsUser);
            await _userRepository.SaveChangesAsync();


            var gPermissions = await _groupPermissionRepository.GetGroupPermissionsByGroupId(user.Group.Id);

            foreach (var permission in gPermissions)
            {
                var groupPermission = await _groupPermissionRepository.GetGroupPermissionById(permission.Id);

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
                    MenuId = permission.Menu.Id,
                    User = user,
                    GroupMenu = groupPermission,
                    CreatedAt = DateTime.UtcNow,
                };

                await _userPermissionRepository.AddUserPermission(userPermission);

                foreach (var operation in permission.Operations)
                {
                    var foundOperation = await _globalOperationsRepository.GetGlobalOperationByMetlhod(operation.OperationName);


                    var userOperation = new UserOperation
                    {
                        Id = Guid.NewGuid(),
                        OperationName = operation.OperationName,
                        UserPermission = userPermission,
                        GlobalOperation = foundOperation,
                        GroupName = user.Group.Name
                    };

                    await _userOperationRepository.AddUserOperation(userOperation);
                }
            }
            user.IsPermissionDefault = true;
            await _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();
            var userDTO = _mapper.Map<User, UserGroupDTO>(user);

            return userDTO;

        }
        //public async Task<List<UserDTO>> RetrieveAllUsersAndMap()
        //{

        //    var users = await _context.Users.ToListAsync();
        //    var userDTOs = new List<UserDTO>();

        //    foreach (var user in users)
        //    {
        //        if (!user.IsActive)
        //            continue;

        //        var userDTO = _mapper.Map<User, UserDTO>(user);
        //        userDTOs.Add(userDTO);
        //    }

        //    return userDTOs;
        //}


    }
}

