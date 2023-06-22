using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserWithGroupAndPermissionsAsync(Guid userId);
    }
}
