using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Zones.Interfaces
{
    public interface IZoneRepository
    {
        Task AddAsync(Zone zone);
        void Update(Zone zone);
        void Delete(Zone zone);
        void Restore(Zone zone);
        Task<Zone?> GetByIdAsync(Guid? id);
        Task<List<Zone>> GetAsync(User user);
        Task<Zone?> GetByCode(string code);
        Task<Zone?> GetZoneAndChildren(Guid? id);
        Task<Zone?> GetOnlyZone(Guid? id);
        Task<Zone?> GetByIdWithReservoirsAsync(Guid? id);
        Task<Zone?> GetWithField(Guid? id);
        Task<Zone?> GetWithUser(Guid? id);
        Task SaveChangesAsync();
    }
}
