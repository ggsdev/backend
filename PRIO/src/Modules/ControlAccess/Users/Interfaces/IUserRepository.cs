using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;

namespace PRIO.src.Modules.ControlAccess.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAndAddUser(CreateUserViewModel body, string treatedUsername);
        Task AddUser(User user);
        Task AddUserPermission(UserPermission userPermission);
        Task<List<User>> GetUsers();
        Task<User?> GetUsersByEmail(string email);
        Task<User?> GetUserByUsername(string username);
        Task<User> GetUserById(Guid id);
        Task ValidateMenu(InsertUserPermissionViewModel body);
        void UpdateUser(User userHasGroup);
        Task UpdateUsers(List<User> users);
        Task<User> GetUserWithGroupAndPermissionsAsync(Guid userId);
        Task<List<User>> GetUsersByLastGroupId(Guid groupId);
        Task<List<User>> GetAdminUsers();
        Task SaveChangesAsync();
    }
}
