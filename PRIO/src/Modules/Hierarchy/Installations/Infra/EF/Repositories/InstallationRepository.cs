using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories
{
    public class InstallationRepository : IInstallationRepository
    {
        private readonly DataContext _context;
        public InstallationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Installation?> GetInstallationMeasurementByUepAndAnpCodAsync(string codInstallation, string acronym)
        {
            switch (acronym)
            {
                case "039":
                    return await _context.Installations
                        .Include(x => x.MeasuringPoints)
                     .FirstOrDefaultAsync(x => x.UepCod == codInstallation && x.CodInstallationAnp == codInstallation);

                case "001":
                    return await _context.Installations
                        .Include(x => x.MeasuringPoints)
                        .FirstOrDefaultAsync(x => x.UepCod == codInstallation && x.CodInstallationAnp == codInstallation);

                case "002":
                    return await _context.Installations
                        .Include(x => x.MeasuringPoints)
                        .FirstOrDefaultAsync(x => x.UepCod == codInstallation && x.CodInstallationAnp == codInstallation);

                case "003":
                    return await _context.Installations
                        .Include(x => x.MeasuringPoints)
                        .FirstOrDefaultAsync(x => x.UepCod == codInstallation && x.CodInstallationAnp == codInstallation);

                default:
                    throw new BadRequestException("Acronym values are: 001, 002, 003, 039");
            }

        }

        public async Task<List<FieldFR?>> GetFRsByIdAsync(Guid? id)
        {
            return await _context.FieldsFRs
                .Include(x => x.Field)
               .ThenInclude(x => x.Installation)
               .Where(x => x.Field.Installation.Id == id)
               .ToListAsync();
        }

        public async Task<Installation?> GetUepById(Guid? id)
        {
            return await _context.Installations.FirstOrDefaultAsync(x => x.Id == id && x.IsProcessingUnit);
        }

        public async Task<FieldFR?> GetFrByDateMeasuredAndFieldId(DateTime date, Guid fieldId)
        {
            return await _context.FieldsFRs
                .Include(x => x.DailyProduction)
                .Include(x => x.Field)
                .Where(x => (x.DailyProduction.MeasuredAt.Year == date.Year &&
                            x.DailyProduction.MeasuredAt.Month == date.Month &&
                            x.DailyProduction.MeasuredAt.Day == date.Day) && x.Field.Id == fieldId && x.IsActive && x.DailyProduction.IsActive)
                .FirstOrDefaultAsync();
        }

        public void UpdateFr(FieldFR fieldFr)
        {
            _context.Update(fieldFr);
        }
        public async Task<List<FieldFR?>> GetFRsByUEPAsync(string? uep)
        {
            var Frs = await _context.FieldsFRs
                .Include(x => x.Field)
                .ThenInclude(x => x.Installation)
                .Where(x => x.Field.Installation.UepCod == uep)
                .Where(x => x.IsActive == true)
                .ToListAsync();
            return Frs;
        }

        public async Task<List<FieldFR>> GetFRsByIdAsync(Guid id)
        {
            return await _context.FieldsFRs
                .Include(x => x.Field)
                .ThenInclude(x => x.Installation)
                .Where(x => x.Field.Installation.Id == id)
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }
        public async Task AddFRAsync(FieldFR fr)
        {
            await _context.FieldsFRs.AddAsync(fr);
        }

        public async Task<Installation?> GetInstallationAndChildren(Guid? id)
        {
            return await _context.Installations
                    .Include(i => i.Fields)
                        .ThenInclude(f => f.Zones)
                            .ThenInclude(z => z.Reservoirs)
                    .Include(i => i.Fields)
                        .ThenInclude(f => f.Wells)
                                .ThenInclude(r => r.Completions)
                    .Include(x => x.MeasuringPoints)
                        .ThenInclude(x => x.MeasuringEquipments)
                     .Include(x => x.MeasuringPoints)
                        .ThenInclude(x => x.DOR)
                     .Include(x => x.MeasuringPoints)
                        .ThenInclude(x => x.Section)
                     .Include(x => x.MeasuringPoints)
                        .ThenInclude(x => x.TOGRecoveredOil)
                     .Include(x => x.MeasuringPoints)
                        .ThenInclude(x => x.DrainVolume)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Installation?> GetByIdWithCalculationsAsync(Guid? id)
        {
            return await _context.Installations
                .Include(x => x.OilVolumeCalculation)
                .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.AssistanceGases)
                .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.ExportGases)
                .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.ImportGases)
                .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.LowPressureGases)
                .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.HighPressureGases)
                .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.HPFlares)
                .Include(x => x.GasVolumeCalculation)
                        .ThenInclude(x => x.LPFlares)
                .Include(x => x.GasVolumeCalculation)
                        .ThenInclude(x => x.PilotGases)
                .Include(x => x.GasVolumeCalculation)
                        .ThenInclude(x => x.PurgeGases)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Installation?> GetByIdAsync(Guid? id)
        {
            var installation = await _context.Installations
                .Include(x => x.Cluster)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return installation;
        }

        public async Task<Installation?> GetByNameAsync(string? name)
        {
            var installation = await _context.Installations
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();
            return installation;
        }

        public async Task<Installation?> GetByCod(string? cod)
        {
            return await _context.Installations
              .FirstOrDefaultAsync(x => x.CodInstallationAnp == cod);
        }
        public async Task<Installation?> GetByUEPCod(string? cod)
        {
            return await _context.Installations
                .Include(x => x.OilVolumeCalculation)
                .Include(x => x.GasVolumeCalculation)
                .Where(x => x.UepCod == cod && x.CodInstallationAnp == cod)
              .FirstOrDefaultAsync();
        }
        public async Task<List<Installation?>> GetByUEPWithFieldsCod(string? cod)
        {
            return await _context.Installations
                .Include(x => x.Fields)
                .ThenInclude(x => x.FRs)
                .Where(x => x.UepCod == cod)
              .ToListAsync();
        }

        public async Task<List<Installation>> GetByIdWithFieldsCod(Guid id)
        {
            return await _context.Installations
                .Include(x => x.Fields)
                .ThenInclude(x => x.FRs)
                .Where(x => x.Id == id)
              .ToListAsync();
        }

        public async Task<Installation?> GetByIdWithFieldsMeasuringPointsAsync(Guid? id)
        {
            return await _context.Installations
                .Include(x => x.Fields)
                .Include(x => x.OilVolumeCalculation)
                .Include(x => x.GasVolumeCalculation)
                .Include(x => x.MeasuringPoints)
                .Include(x => x.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Installation installation)
        {
            await _context.Installations.AddAsync(installation);
        }

        public async Task<List<Installation>> GetAsync()
        {
            return await _context.Installations
                .Include(x => x.Cluster)
                .Include(x => x.User)
                .Include(x => x.Fields)
                    .ThenInclude(x => x.Wells)
                        .ThenInclude(x => x.WellEvents)
                .ToListAsync();
        }
        public async Task<List<Installation>> GetUEPsAsync()
        {
            return await _context.Installations
                .Include(x => x.Cluster)
                .Include(x => x.User)
                .Include(x => x.Fields)
                    .ThenInclude(x => x.Wells)
                        .ThenInclude(x => x.WellEvents)
                .Where(x => x.IsProcessingUnit == true)
                .ToListAsync();
        }

        public async Task<List<Installation>> GetInstallationChildrenOfUEP(string uepCode)
        {
            return await _context.Installations
                .Include(x => x.Fields)
                    .ThenInclude(x => x.Wells)
                        .ThenInclude(x => x.BTPDatas)
                .Where(x => x.UepCod == uepCode)
                .ToListAsync();
        }
        public async Task<List<Installation>> GetUEPsCreateAsync(string table)
        {
            if (table == "oil")
            {

                return await _context.Installations
                    .Include(x => x.Cluster)
                    .Include(x => x.User)
                    .Include(x => x.OilVolumeCalculation)
                    .ThenInclude(x => x.Sections)
                    .Include(x => x.OilVolumeCalculation)
                    .ThenInclude(x => x.DrainVolumes)
                    .Include(x => x.OilVolumeCalculation)
                    .ThenInclude(x => x.DORs)
                    .Include(x => x.OilVolumeCalculation)
                    .ThenInclude(x => x.TOGRecoveredOils)
                    .Include(x => x.GasVolumeCalculation)
                    .Where(x => x.IsProcessingUnit == true)
                    .Where(x => x.OilVolumeCalculation.Sections.Count == 0 &&
                        x.OilVolumeCalculation.DrainVolumes.Count == 0 &&
                        x.OilVolumeCalculation.DORs.Count == 0 &&
                        x.OilVolumeCalculation.TOGRecoveredOils.Count == 0)
                    .ToListAsync();
            }
            else if (table == "gas")
            {
                return await _context.Installations
                    .Include(x => x.Cluster)
                    .Include(x => x.User)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.AssistanceGases)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.LowPressureGases)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.HighPressureGases)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.HPFlares)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.LPFlares)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.ExportGases)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.ImportGases)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.PilotGases)
                    .Include(x => x.GasVolumeCalculation)
                    .ThenInclude(x => x.PurgeGases)
                    .Where(x => x.IsProcessingUnit == true)
                    .Where(x => x.GasVolumeCalculation.AssistanceGases.Count == 0 &&
                        x.GasVolumeCalculation.LowPressureGases.Count == 0 &&
                        x.GasVolumeCalculation.HighPressureGases.Count == 0 &&
                        x.GasVolumeCalculation.LPFlares.Count == 0 &&
                        x.GasVolumeCalculation.ExportGases.Count == 0 &&
                        x.GasVolumeCalculation.ImportGases.Count == 0 &&
                        x.GasVolumeCalculation.PilotGases.Count == 0 &&
                        x.GasVolumeCalculation.PurgeGases.Count == 0 &&
                        x.GasVolumeCalculation.HPFlares.Count == 0

                        )
                    .ToListAsync();
            }
            return await _context.Installations
                    .Include(x => x.Cluster)
                    .Include(x => x.User)
                    .Include(x => x.OilVolumeCalculation)
                    .ThenInclude(x => x.Sections)
                    .Include(x => x.OilVolumeCalculation)
                    .ThenInclude(x => x.DrainVolumes)
                    .Include(x => x.OilVolumeCalculation)
                    .ThenInclude(x => x.DORs)
                    .Include(x => x.OilVolumeCalculation)
                    .ThenInclude(x => x.TOGRecoveredOils)
                    .Include(x => x.GasVolumeCalculation)
                    .Where(x => x.IsProcessingUnit == true)
                    .ToListAsync();
        }

        public void Update(Installation installation)
        {
            _context.Installations.Update(installation);
        }

        public void Delete(Installation installation)
        {
            _context.Installations.Update(installation);
        }

        public void Restore(Installation installation)
        {
            _context.Installations.Update(installation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
