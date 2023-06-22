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
        Task<List<Reservoir>> GetAsync();
        Task<Reservoir?> GetWithZoneAsync(Guid? id);
        Task<Reservoir?> GetOnlyReservoirAsync(Guid? id);
        Task<Reservoir?> GetWithZoneFieldAsync(Guid? id);
        Task SaveChangesAsync();
    }
}
