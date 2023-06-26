using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataContext _context;
        public EquipmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MeasuringEquipment?> GetByIdAsync(Guid? id)
        {
            return await _context.MeasuringEquipments
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(MeasuringEquipment installation)
        {
            await _context.MeasuringEquipments.AddAsync(installation);
        }

        public async Task<List<MeasuringEquipment>> GetAsync()
        {
            return await _context.MeasuringEquipments
                .ToListAsync();
        }
        public async Task<MeasuringEquipment?> GetWithInstallationAsync(Guid? id)
        {
            return await _context.MeasuringEquipments
                .Include(x => x.Installation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(MeasuringEquipment installation)
        {
            _context.MeasuringEquipments.Update(installation);
        }

        public void Delete(MeasuringEquipment installation)
        {
            _context.MeasuringEquipments.Update(installation);
        }

        public void Restore(MeasuringEquipment installation)
        {
            _context.MeasuringEquipments.Update(installation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
