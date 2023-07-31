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
        public async Task<List<FieldFR?>> GetFRsByUEPAsync(string? uep)
        {
            var Frs = await _context.FieldsFRs
                .Include(x => x.Field)
                .ThenInclude(x => x.Installation)
                .Where(x => x.Field.Installation.UepCod == uep)
                .ToListAsync();
            return Frs;
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

        public async Task<Installation?> GetByIdWithFieldsMeasuringPointsAsync(Guid? id)
        {
            return await _context.Installations
                .Include(x => x.Fields)
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
                .ToListAsync();
        }
        public async Task<List<Installation>> GetUEPsAsync()
        {
            return await _context.Installations
                .Include(x => x.Cluster)
                .Include(x => x.User)
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
