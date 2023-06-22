using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Interfaces
{
    public interface IEquipmentRepository
    {
        Task AddAsync(MeasuringEquipment equipment);
        void Update(MeasuringEquipment equipment);
        void Delete(MeasuringEquipment equipment);
        void Restore(MeasuringEquipment equipment);
        Task<MeasuringEquipment?> GetByIdAsync(Guid? id);
        Task<List<MeasuringEquipment>> GetAsync();
        Task SaveChangesAsync();
    }
}
