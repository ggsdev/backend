using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Interfaces
{
    public interface IGroupRepository
    {
        Task AddAsync(Group group);
        Task<Group?> GetGroupByIdAsync(Guid id);
        Task<Group?> GetGroupByNameAsync(string groupName);
        Task<List<Group>> GetGroups();
        Task ValidateMenu(InsertGroupPermission body);
        Task ValidateMenusByCreateViewModel(CreateGroupViewModel body);
        Task<Group?> GetGroupWithPermissionsAndOperationsByIdAsync(Guid groupId);
        Task<Group> NewGroupPermissionsAsync(Group group, InsertGroupPermission body);
        void UpdateUser(User user);
        void UpdateGroup(Group group);
        Task SaveChangesAsync();
    }
}
