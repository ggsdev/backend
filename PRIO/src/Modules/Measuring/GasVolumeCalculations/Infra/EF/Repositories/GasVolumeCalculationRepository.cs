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
        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            var entitiesToRemove = entities.ToList();
            _context.RemoveRange(entitiesToRemove);
        }
        public async Task<GasVolumeCalculation?> GetGasVolumeCalculationByIdAsync(Guid? id)
        {
            return await _context.GasVolumeCalculations
                .Include(x => x.Installation)
                .FirstOrDefaultAsync(c => c.Id == id);
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

        #region AssistanceGas
        public async Task<AssistanceGas?> GetAssistanceGas(Guid id)
        {
            return await _context.AssistanceGases
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }

        public async Task<AssistanceGas?> GetOtherAssistanceGas(Guid mpointId, Guid assistanceGasId)
        {
            return await _context.AssistanceGases
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(assistanceGasId))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();
        }

        public async Task AddAssistanceGas(AssistanceGas gas)
        {
            await _context.AssistanceGases.AddAsync(gas);
        }

        #endregion

        public async Task<ExportGas?> GetExportGas(Guid id)
        {
            return await _context.ExportGases
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }
        public async Task AddExportGas(ExportGas gas)
        {
            await _context.ExportGases.AddAsync(gas);
        }

        public async Task<ExportGas?> GetOtherExportGas(Guid mpointId, Guid exportGas)
        {
            return await _context.ExportGases
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(exportGas))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();
        }



        public async Task<GasVolumeCalculation?> GetGasVolumeCalculationByInstallationId(Guid id)
        {
            var gasVolumeCalculation = await _context.GasVolumeCalculations
                .Include(x => x.Installation)
                    .ThenInclude(x => x.MeasuringPoints)
                .Include(x => x.AssistanceGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.ExportGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.HighPressureGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.HPFlares)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.ImportGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.LowPressureGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.LPFlares)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.PilotGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.PurgeGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Where(x => x.Installation.Id == id)
                .FirstOrDefaultAsync();

            return gasVolumeCalculation;
        }


        public async Task<GasVolumeCalculation?> GetGasVolumeCalculationByInstallationUEP(string uepCode)
        {
            var gasVolumeCalculation = await _context.GasVolumeCalculations
                .Include(x => x.Installation)
                    .ThenInclude(x => x.MeasuringPoints)
                .Include(x => x.AssistanceGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.ExportGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.HighPressureGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.HPFlares)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.ImportGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.LowPressureGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.LPFlares)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.PilotGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.PurgeGases)
                    .ThenInclude(x => x.MeasuringPoint)
                .Where(x => x.Installation.UepCod == uepCode)
                .FirstOrDefaultAsync();

            return gasVolumeCalculation;
        }

        public async Task<GasVolumeCalculation?> GetGasVolumeCalculationByInstallationIdXML(Guid id)
        {
            var gasVolumeCalculation = await _context.GasVolumeCalculations
                .Include(x => x.Installation)
                    .ThenInclude(x => x.MeasuringPoints)
                .Where(x => x.Installation.Id == id)
                .FirstOrDefaultAsync();

            return gasVolumeCalculation;
        }

        public async Task<HighPressureGas?> GetHighPressureGas(Guid id)
        {
            return await _context.HighPressureGases
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }
        public async Task AddHighPressureGas(HighPressureGas gas)
        {
            await _context.HighPressureGases.AddAsync(gas);
        }

        public async Task<HighPressureGas?> GetOtherHighPressureGas(Guid mpointId, Guid highGas)
        {
            return await _context.HighPressureGases
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(highGas))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();
        }

        public async Task<HPFlare?> GetHPFlare(Guid id)
        {
            return await _context.HPFlares
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }
        public async Task AddHPFlare(HPFlare gas)
        {
            await _context.HPFlares.AddAsync(gas);
        }

        public async Task<HPFlare?> GetOtherHPFlare(Guid mpointId, Guid hpFlare)
        {
            return await _context.HPFlares
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(hpFlare))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();
        }

        public async Task<ImportGas?> GetImportGas(Guid id)
        {
            return await _context.ImportGases
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }
        public async Task AddImportGas(ImportGas gas)
        {
            await _context.ImportGases.AddAsync(gas);
        }

        public async Task<ImportGas?> GetOtherImportGas(Guid mpointId, Guid importGas)
        {
            return await _context.ImportGases
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(importGas))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();

        }


        public async Task<LowPressureGas?> GetLowPressureGas(Guid id)
        {
            return await _context.LowPressureGases
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }
        public async Task AddLowPressureGas(LowPressureGas gas)
        {
            await _context.LowPressureGases.AddAsync(gas);
        }

        public async Task<LowPressureGas?> GetOtherLowPressureGas(Guid mpointId, Guid low)
        {
            return await _context.LowPressureGases
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(low))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();
        }


        public async Task<LPFlare?> GetLPFlare(Guid id)
        {
            return await _context.LPFlares
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }
        public async Task AddLPFlare(LPFlare gas)
        {
            await _context.LPFlares.AddAsync(gas);
        }

        public async Task<LPFlare?> GetOtherLPFlare(Guid mpointId, Guid lp)
        {
            return await _context.LPFlares
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(lp))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();
        }

        public async Task<PilotGas?> GetPilotGas(Guid id)
        {
            return await _context.PilotGases
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }
        public async Task AddPilotGas(PilotGas gas)
        {
            await _context.PilotGases.AddAsync(gas);
        }

        public async Task<PilotGas?> GetOtherPilotGas(Guid mpointId, Guid pilot)
        {
            return await _context.PilotGases
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(pilot))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();
        }

        public async Task<PurgeGas?> GetPurgeGas(Guid id)
        {
            return await _context.PurgeGases
               .Include(x => x.GasVolumeCalculation)
               .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
               .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id);
        }
        public async Task AddPurgeGas(PurgeGas gas)
        {
            await _context.PurgeGases.AddAsync(gas);
        }

        public async Task<PurgeGas?> GetOtherPurgeGas(Guid mpointId, Guid purge)
        {
            return await _context.PurgeGases
               .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
                   .Where(x => !x.GasVolumeCalculation.Id.Equals(purge))
                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                   .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
