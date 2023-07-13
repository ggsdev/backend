using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task AddUserPermission(UserPermission userPermission);
        Task<List<User>> GetUsers();
        Task<User?> GetUsersByEmail(string email);
        Task<User> GetUserById(Guid id);
        Task ValidateMenu(InsertUserPermissionViewModel body);
        Task UpdateUser(User userHasGroup);
        Task UpdateUsers(List<User> users);
        Task<User> GetUserWithGroupAndPermissionsAsync(Guid userId);
        Task<List<User>> GetUsersByLastGroupId(Guid groupId);
        Task SaveChangesAsync();
    }
}
