using PRIO.src.Modules.ControlAccess.Groups.Dtos;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group?> GetGroupByIdAsync(Guid id);
        Task<Group?> GetGroupByNameAsync(string groupName);
        Task<List<Group>> GetGroups();
        Task<Group> GetGroupWithPermissionsAndOperationsByIdAsync(Guid groupId);
        Task<Group> CreateGroupAsync(CreateGroupViewModel body);
        Task InsertUserInGroupAsync(Group group, User userHasGroup, List<GroupPermissionsDTO> groupPermissionsDTO);
        void UpdateUser(User user);
        void UpdateGroup(Group group);
        Task SaveChangesAsync();
    }
}
