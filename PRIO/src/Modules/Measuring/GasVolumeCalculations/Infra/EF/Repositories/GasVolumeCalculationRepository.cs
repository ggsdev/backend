using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Repositories
{
    public class GasVolumeCalculationRepository : IGasVolumeCalculationRepository
    {
        private readonly DataContext _context;

        public GasVolumeCalculationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddGasVolumeCalculationAsync(GasVolumeCalculation gasVolume)
        {
            await _context.GasVolumeCalculations.AddAsync(gasVolume);
        }

        public async Task<GasVolumeCalculation?> GetGasVolumeCalculationByIdAsync(Guid? id)
        {
            return await _context.GasVolumeCalculations.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<List<GasVolumeCalculation>> GetAllGasVolumeCalculationsAsync()
        {
            return await _context.GasVolumeCalculations
                .ToListAsync();
        }
        public void UpdateGasVolumeCalculation(GasVolumeCalculation gasVolume)
        {
            _context.GasVolumeCalculations.Update(gasVolume);
        }
        public void DeleteGasVolumeCalculation(GasVolumeCalculation gasVolume)
        {
            _context.GasVolumeCalculations.Update(gasVolume);
        }
        public void RestoreGasVolumeCalculation(GasVolumeCalculation gasVolume)
        {
            _context.GasVolumeCalculations.Update(gasVolume);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
