using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserPermission(UserPermission userPermission);
        Task UpdateUsers(List<User> users);
        Task<User> GetUserWithGroupAndPermissionsAsync(Guid userId);
        Task<List<User>> GetUsersByLastGroupId(Guid groupId);

    }
}
