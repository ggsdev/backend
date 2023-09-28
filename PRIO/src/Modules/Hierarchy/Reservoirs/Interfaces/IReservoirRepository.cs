using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces
{
    public interface IReservoirRepository
    {
        Task AddAsync(Reservoir reservoir);
        void Update(Reservoir reservoir);
        void Delete(Reservoir reservoir);
        void Restore(Reservoir reservoir);
        Task<Reservoir?> GetByIdAsync(Guid? id);
        Task<Reservoir?> GetByNameAsync(string? name);
        Task<List<Reservoir>> GetAsync(User user);
        Task<Reservoir?> GetWithZoneAsync(Guid? id);
        Task<Reservoir?> GetOnlyReservoirAsync(Guid? id);
        Task<Reservoir?> GetWithZoneFieldAsync(Guid? id);
        Task<Reservoir?> GetByIdWithCompletionsAsync(Guid? id);
        Task<Reservoir?> GetReservoirAndChildren(Guid? id);
        Task SaveChangesAsync();
    }
}
