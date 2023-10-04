using AutoMapper;
using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.Interfaces;
using PRIO.src.Modules.ControlAccess.Operations.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.UserDtos;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
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
        private IInstallationRepository _installationRepository;
        private IInstallationsAccessRepository _installationAccessRepository;
        private UserFactory _userFactory;
        private UserPermissionFactory _userPermissionFactory;
        private UserOperationFactory _userOperationFactory;
        private InstallationAccessFactory _installationAccessFactory;
        private IMapper _mapper;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _table = HistoryColumns.TableUsers;

        public UserService(IMapper mapper, IUserRepository user, IUserPermissionRepository userPermissionRepository, IUserOperationRepository userOperationRepository, IGroupPermissionRepository groupPermissionRepository, IGroupOperationRepository groupOperationRepository, IGlobalOperationsRepository globalOperationsRepository, SystemHistoryService systemHistoryService, IInstallationRepository installationRepository, IInstallationsAccessRepository installationAccessRepository, UserFactory userFactory, InstallationAccessFactory installationAccessFactory, UserOperationFactory userOperationFactory, UserPermissionFactory userPermissionFactory)
        {
            _mapper = mapper;
            _userRepository = user;
            _userPermissionRepository = userPermissionRepository;
            _userOperationRepository = userOperationRepository;
            _groupPermissionRepository = groupPermissionRepository;
            _groupOperationRepository = groupOperationRepository;
            _globalOperationsRepository = globalOperationsRepository;
            _installationRepository = installationRepository;
            _systemHistoryService = systemHistoryService;
            _installationRepository = installationRepository;
            _installationAccessRepository = installationAccessRepository;
            _userFactory = userFactory;
            _installationAccessFactory = installationAccessFactory;
            _userOperationFactory = userOperationFactory;
            _userPermissionFactory = userPermissionFactory;

        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var userDTOS = _mapper.Map<List<User>, List<UserDTO>>(users);

            return userDTOS;
        }
        public async Task<UserDTO> CreateUserAsync(CreateUserViewModel body, User loggedUser)
        {
            var treatedUsername = body.Username.Split('@')[0];

            //var userInAd = ActiveDirectory
            //    .CheckUserExistsInActiveDirectory(treatedUsername);

            //if (userInAd is false)
            //    throw new NotFoundException("Não foi possível validar o usuário no domínio, digite um usuário valido");
            var userInDatabase = await _userRepository
                .GetUserByUsername(treatedUsername);

            if (userInDatabase != null)
                throw new ConflictException("Usuário já cadastrado");

            if (body.InstallationsId is null || body.InstallationsId.Count == 0)
                throw new ConflictException("Usuário precisa ter instalações associadas.");

            var instalationsToRelation = new List<Installation>();

            foreach (var installationId in body.InstallationsId)
            {
                var verifyInstallation = await _installationRepository.GetByIdAsync(installationId);
                if (verifyInstallation == null)
                    throw new ConflictException("Instalação não existente.");

                instalationsToRelation.Add(verifyInstallation);
            }
            var user = _userFactory.CreateUser(body, treatedUsername);
            await _userRepository.AddUser(user);

            foreach (var installation in instalationsToRelation)
            {
                var create = _installationAccessFactory.CreateInstallationAccess(installation, user);
                await _installationAccessRepository.AddInstallationsAccess(create);
            }


            //var currentData = _mapper.Map<User, UserHistoryDTO>(user);

            //var dateNow = DateTime.UtcNow.AddHours(-3);

            //currentData.createdAt = dateNow;
            //currentData.updatedAt = dateNow;

            //var history = new SystemHistory
            //{
            //    Table = HistoryColumns.TableUsers,
            //    TypeOperation = HistoryColumns.Create,
            //    CreatedBy = loggedUser?.Id,
            //    TableItemId = userId,
            //    CurrentData = currentData,
            //};

            //await _systemHistoryRepository.AddAsync(history);

            await _systemHistoryService.Create<User, UserHistoryDTO>(_table, loggedUser, user.Id, user);


            var userDTO = _mapper.Map<User, UserDTO>(user);
            await _userRepository.SaveChangesAsync();

            return userDTO;
        }
        public async Task<UserWithPermissionsDTO?> ProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user is null)
                throw new NotFoundException("User not found");

            if (userId.ToString() != user.Id.ToString())
                throw new ConflictException("User don't have permission to do that.");

            var userDTO = BuildPermissions(user);

            foreach (var installationAccess in user.InstallationsAccess)
            {
                var installationDatabase = await _installationRepository.GetByIdAsync(installationAccess.Installation.Id);
                if (installationDatabase == null)
                    throw new NotFoundException("Instalação não encontrada.");

                userDTO.InstallationsAccess.Where(x => x.Id == installationAccess.Id).FirstOrDefault().Name = installationDatabase.Name;
                userDTO.InstallationsAccess.Where(x => x.Id == installationAccess.Id).FirstOrDefault().Id = installationDatabase.Id;
            }

            return userDTO;
        }
        public async Task<UserWithPermissionsDTO?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null)
                throw new NotFoundException("User not found");

            var userDTO = BuildPermissions(user);

            foreach (var installationAccess in user.InstallationsAccess)
            {
                var installationDatabase = await _installationRepository.GetByIdAsync(installationAccess.Installation.Id);
                if (installationDatabase == null)
                    throw new NotFoundException("Instalação não encontrada.");

                userDTO.InstallationsAccess.Where(x => x.Id == installationAccess.Id).FirstOrDefault().Name = installationDatabase.Name;
                userDTO.InstallationsAccess.Where(x => x.Id == installationAccess.Id).FirstOrDefault().Id = installationDatabase.Id;
            }

            return userDTO;
        }
        public async Task<UserDTO?> UpdateUserByIdAsync(Guid id, UpdateUserViewModel body, User loggedUser)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null || user.IsActive is false)
                throw new NotFoundException("User not found");

            if (user.Type == "Master")
                throw new ConflictException("Usuário Master do sistema não pode ser alterado.");

            var beforeChangesUser = _mapper.Map<UserHistoryDTO>(user);
            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(user, body);

            //var userId = (Guid)HttpContext.Items["Id"]!;
            //var userOperation = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            //if (userOperation is null)
            //    return NotFound(new ErrorResponseDTO
            //    {
            //        Message = $"User is not found"
            //    });            

            if (body.Username is not null)
            {
                var username = await _userRepository.GetUserByUsername(body.Username);
                if (username != null)
                    throw new ConflictException("Já existe um usuário com este nome de usuário");
            };

            if (body.InstallationsId is not null)
            {
                var instalationsToRelation = new List<Installation>();

                if (body.InstallationsId.Count == 0)
                    throw new ConflictException("Usuário precisa estar associado com pelo menos uma instalação, considere deletar o usuário.");
                else
                {
                    foreach (var installationId in body.InstallationsId)
                    {
                        var verifyInstallation = await _installationRepository.GetByIdAsync(installationId);
                        if (verifyInstallation == null)
                            throw new ConflictException("Instalação não existente.");

                        instalationsToRelation.Add(verifyInstallation);
                    }
                    user.InstallationsAccess.Clear();

                    foreach (var installation in instalationsToRelation)
                    {

                        var create = _installationAccessFactory.CreateInstallationAccess(installation, user);
                        await _installationAccessRepository.AddInstallationsAccess(create);
                    }
                }

            }

            await _systemHistoryService.Update(_table, loggedUser, updatedProperties, user.Id, user, beforeChangesUser);

            //user.Name = body.Name is not null ? body.Name : user.Name;
            user.Password = body.Password is not null ? BCrypt.Net.BCrypt.HashPassword(body.Password) : user.Password;
            //user.Email = body.Email is not null ? body.Email : user.Email;
            //user.Username = body.Username is not null ? body.Username : user.Username;

            _userRepository.UpdateUser(user);

            var userDTO = _mapper.Map<User, UserDTO>(user);
            await _userRepository.SaveChangesAsync();
            return userDTO;
        }
        public async Task DeleteUserByIdAsync(Guid id, Guid userOperationId, User loggedUser)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null || user.IsActive is false)
                throw new NotFoundException("User not found");

            if (user.Type == "Master")
                throw new ConflictException("Usuário Master do sistema não pode ser Deletado.");

            var userOperation = await _userRepository.GetUserById(userOperationId);
            if (userOperation is null)
                throw new NotFoundException("User not found");

            var properties = new
            {
                DeletedAt = DateTime.UtcNow.AddHours(-3),
                IsActive = false
            };

            user.DeletedAt = DateTime.UtcNow.AddHours(-3);
            user.IsActive = false;

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(user, properties);

            await _systemHistoryService.Delete<User, UserHistoryDTO>(_table, loggedUser, updatedProperties, user.Id, user);

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();
        }
        public async Task<UserDTO?> RestoreUserByIdAsync(Guid id, Guid userOperationId, User loggedUser)
        {
            var user = await _userRepository.GetUserById(id);

            if (user is null || user.IsActive is true)
                throw new NotFoundException("User is already active.");

            var userOperation = await _userRepository.GetUserById(userOperationId);
            if (userOperation is null)
                throw new NotFoundException("User not found");

            user.IsActive = true;
            user.DeletedAt = null;

            var properties = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            _userRepository.UpdateUser(user);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(user, properties);

            await _systemHistoryService.Delete<User, UserHistoryDTO>(_table, loggedUser, updatedProperties, user.Id, user);


            var UserDTO = _mapper.Map<User, UserDTO>(user);
            await _userRepository.SaveChangesAsync();
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

            if (userWithPermissions.Type == "Usuário Master não pode sofrer alterações nas permissões.")
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
                    var verifyRelationMenuParentDTO = _mapper.Map<GroupPermission, GroupPermissionsDTO>(verifyRelationMenuParent);

                    var createMenuParent = _userPermissionFactory.CreateUserPermission(verifyRelationMenuParentDTO, userWithPermissions, verifyRelationMenuParent);
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
                                    var verifyRelationMenuChildrenDTO = _mapper.Map<GroupPermission, GroupPermissionsDTO>(verifyRelationMenuChildren);
                                    var createMenuChildren = _userPermissionFactory.CreateUserPermission(verifyRelationMenuChildrenDTO, userWithPermissions, verifyRelationMenuChildren);
                                    await _userPermissionRepository.AddUserPermission(createMenuChildren);

                                    foreach (var operation in children.Operations)
                                    {
                                        //PROCURANDO PERMISSAO DA OPERACAO NO GRUPO
                                        var verifyRelationMenuChildrenWithOperations = await _groupOperationRepository.GetGroupOperationsByMenuIdAndGroupPermissionId(operation.OperationId, verifyRelationMenuChildren.Id);

                                        //EXISTE PERMISSAO DA OPERACAO
                                        if (verifyRelationMenuChildrenWithOperations is not null)
                                        {
                                            var groupOperationDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(verifyRelationMenuChildrenWithOperations);
                                            var createGroupOperationsInUser = _userOperationFactory.CreateUserOperation(groupOperationDTO, createMenuChildren, verifyRelationMenuChildrenWithOperations.GlobalOperation, userWithPermissions.Group);
                                            await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                                        }
                                        else // NAO EXISTE PERMISSAO DA OPERACAO
                                        {
                                            var verifyRelationMenuChildrenWithOperationsInMaster = await _groupOperationRepository.GetGroupOperationsByOperationIdAndGroupName(operation.OperationId, "Master");

                                            var groupOperationDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(verifyRelationMenuChildrenWithOperationsInMaster);
                                            var createGroupOperationsInUser = _userOperationFactory.CreateUserOperation(groupOperationDTO, createMenuChildren, verifyRelationMenuChildrenWithOperationsInMaster.GlobalOperation, userWithPermissions.Group);
                                            createGroupOperationsInUser.GroupName = "Master";
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


                                    var verifyRelationMenuChildrenInMasterDTO = _mapper.Map<GroupPermission, GroupPermissionsDTO>(verifyRelationMenuChildrenInMaster);
                                    var createMenuChildren = _userPermissionFactory.CreateUserPermission(verifyRelationMenuChildrenInMasterDTO, userWithPermissions, verifyRelationMenuChildrenInMaster);

                                    foreach (var operation in children.Operations)
                                    {
                                        //PROCURANDO PERMISSAO DA OPERACAO PELO MASTER
                                        var verifyRelationMenuChildrenWithOperationsInMaster = await _groupOperationRepository.GetGroupOperationsByOperationIdAndGroupName(operation.OperationId, "Master");

                                        var verifyRelationMenuChildrenWithOperationsInMasterDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(verifyRelationMenuChildrenWithOperationsInMaster);
                                        var createGroupOperationsInUser = _userOperationFactory.CreateUserOperation(verifyRelationMenuChildrenWithOperationsInMasterDTO, createMenuChildren, verifyRelationMenuChildrenWithOperationsInMaster.GlobalOperation, userWithPermissions.Group);
                                        createGroupOperationsInUser.GroupName = "Master";
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
                                    var verifyRelationMenuChildrenWithOperationsDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(verifyRelationMenuParentWithOperations);

                                    var createGroupOperationsInUser = _userOperationFactory.CreateUserOperation(verifyRelationMenuChildrenWithOperationsDTO, createMenuParent, verifyRelationMenuParentWithOperations.GlobalOperation, userWithPermissions.Group);
                                    await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                                }
                                else // NAO EXISTE PERMISSAO DA OPERACAO
                                {
                                    var verifyRelationMenuChildrenWithOperationsInMaster = await _groupOperationRepository.GetGroupOperationsByOperationIdAndGroupName(operation.OperationId, "Master");
                                    var verifyRelationMenuChildrenWithOperationsInMasterDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(verifyRelationMenuChildrenWithOperationsInMaster);
                                    var createGroupOperationsInUser = _userOperationFactory.CreateUserOperation(verifyRelationMenuChildrenWithOperationsInMasterDTO, createMenuParent, verifyRelationMenuParentWithOperations.GlobalOperation, userWithPermissions.Group);
                                    createGroupOperationsInUser.GroupName = "Master";
                                    await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                                }
                            }
                        }
                    }
                }
                else // NÃO EXISTE A PERMISSAO NO GRUPO
                {
                    var verifyRelationMenuParentInMaster = await _groupPermissionRepository.GetGroupPermissionByMenuIdAndGroupName(menu.MenuId, "Master");
                    var verifyRelationMenuParentInMasterDTO = _mapper.Map<GroupPermission, GroupPermissionsDTO>(verifyRelationMenuParentInMaster);

                    var createMenuParent = _userPermissionFactory.CreateUserPermission(verifyRelationMenuParentInMasterDTO, userWithPermissions, verifyRelationMenuParentInMaster);

                    await _userPermissionRepository.AddUserPermission(createMenuParent);

                    if (menu.Childrens is not null && menu.Childrens.Count != 0)
                    {
                        foreach (var children in menu.Childrens)
                        {
                            if (children.Operations is not null && children.Operations.Count != 0)
                            {
                                var verifyRelationMenuChildrenInMaster = await _groupPermissionRepository.GetGroupPermissionByMenuIdAndGroupName(children.ChildrenId, "Master");
                                var verifyRelationMenuChildrenInMasterDTO = _mapper.Map<GroupPermission, GroupPermissionsDTO>(verifyRelationMenuChildrenInMaster);

                                var createMenuChildren = _userPermissionFactory.CreateUserPermission(verifyRelationMenuChildrenInMasterDTO, userWithPermissions, verifyRelationMenuChildrenInMaster);
                                await _userPermissionRepository.AddUserPermission(createMenuChildren);

                                foreach (var operation in children.Operations)
                                {
                                    //PROCURANDO PERMISSAO DA OPERACAO PELO MASTER
                                    var verifyRelationMenuParentWithOperationsInMaster = await _groupOperationRepository.GetGroupOperationsByOperationIdAndGroupName(operation.OperationId, "Master");
                                    var verifyRelationMenuParentWithOperationsInMasterDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(verifyRelationMenuParentWithOperationsInMaster);

                                    var createGroupOperationsInUser = _userOperationFactory.CreateUserOperation(verifyRelationMenuParentWithOperationsInMasterDTO, createMenuChildren, verifyRelationMenuParentWithOperationsInMaster.GlobalOperation, userWithPermissions.Group);
                                    createGroupOperationsInUser.GroupName = "Master";
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

            var userDTO = _mapper.Map<User, ProfileDTO>(userWithPermissions);
            await _userRepository.SaveChangesAsync();
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
                var groupPermissionDTO = _mapper.Map<GroupPermission, GroupPermissionsDTO>(groupPermission);
                var createuserPermission = _userPermissionFactory.CreateUserPermission(groupPermissionDTO, user, groupPermission);

                await _userPermissionRepository.AddUserPermission(createuserPermission);

                foreach (var operation in permission.Operations)
                {
                    var foundOperation = await _globalOperationsRepository.GetGlobalOperationByMetlhod(operation.OperationName);
                    var foundOperationDTO = _mapper.Map<GroupOperation, UserGroupOperationDTO>(operation);
                    var createGroupOperationsInUser = _userOperationFactory.CreateUserOperation(foundOperationDTO, createuserPermission, foundOperation, user.Group);

                    await _userOperationRepository.AddUserOperation(createGroupOperationsInUser);
                }
            }
            user.IsPermissionDefault = true;
            _userRepository.UpdateUser(user);
            var userDTO = _mapper.Map<User, UserGroupDTO>(user);
            await _userRepository.SaveChangesAsync();

            return userDTO;

        }
        public async Task<List<User>> GetAllEncryptedAdminUsers()
        {
            return await _userRepository.GetAdminUsers();
        }
        private UserWithPermissionsDTO BuildPermissions(User user)
        {
            user.UserPermissions = user.UserPermissions.OrderBy(x => x.MenuOrder).ToList();
            var userDTO = _mapper.Map<User, UserWithPermissionsDTO>(user);

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

