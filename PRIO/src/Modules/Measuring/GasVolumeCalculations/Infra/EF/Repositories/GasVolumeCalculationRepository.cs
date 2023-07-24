using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
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

        public async Task<GasVolumeCalculation> AddGasVolumeCalculationAsync(Installation installation)
        {
            var gasCalculation = new GasVolumeCalculation
            {
                Id = Guid.NewGuid(),
                Installation = installation,

            };
            await _context.GasVolumeCalculations.AddAsync(gasCalculation);
            return gasCalculation;
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

        #region Assistance Gas

        public async Task<AssistanceGas?> GetAssistanceGas(Guid id)
        {
            return await _context.AssistanceGases
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }

        public async Task AddAssistanceGas(AssistanceGas gas)
        {
            await _context.AssistanceGases.AddAsync(gas);
        }
        #endregion

        #region Export Gas
        public async Task<ExportGas?> GetExportGas(string? staticName)
        {
            return await _context.ExportGases
                .FirstOrDefaultAsync(c => c.StaticLocalMeasuringPoint == staticName);
        }
        public async Task AddExportGas(ExportGas gas)
        {
            await _context.ExportGases.AddAsync(gas);
        }

        #endregion

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
