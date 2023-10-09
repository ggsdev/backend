using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.Balance.Injection.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Repositories
{
    public class InjectionRepository : IInjectionRepository
    {
        private readonly DataContext _context;
        public InjectionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<InjectionWaterWell?> GetWaterInjectionById(Guid? id)
        {
            return await _context.InjectionWaterWell
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<InjectionWaterWell>> GetWaterWellInjectionsByDate(DateTime date, Guid fieldId)
        {
            return await _context.InjectionWaterWell
                .Include(x => x.WellValues)
                        .ThenInclude(x => x.Value)
                            .ThenInclude(x => x.Attribute)
                                .ThenInclude(x => x.Element)
                                .Include(x => x.WellValues)
                                    .ThenInclude(x => x.Well)
                                        .ThenInclude(x => x.Field)
                                            .ThenInclude(x => x.Installation)
                .Where(x => x.MeasurementAt.Date == date.Date && x.WellValues.Well.Field.Id == fieldId)
                .ToListAsync();
        }
        public async Task<InjectionWaterGasField?> GetWaterGasFieldInjectionsById(Guid id)
        {
            return await _context.InjectionWaterGasField
                .Include(x => x.WellsWaterInjections)
                    .ThenInclude(x => x.WellValues)
                        .ThenInclude(x => x.Value)
                            .ThenInclude(x => x.Attribute)
                .Include(x => x.WellsGasInjections)
                    .ThenInclude(x => x.WellValues)
                        .ThenInclude(x => x.Value)
                            .ThenInclude(x => x.Attribute)
                                .ThenInclude(x => x.Element)
                .Include(x => x.Field)
                    .ThenInclude(x => x.Installation)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<InjectionWaterGasField?> GetWaterGasFieldInjectionByDate(DateTime dateInjection)
        {
            return await _context.InjectionWaterGasField
                .Include(x => x.WellsWaterInjections)
                    .ThenInclude(x => x.WellValues)
                        .ThenInclude(x => x.Value)
                            .ThenInclude(x => x.Attribute)
                .Include(x => x.WellsGasInjections)
                    .ThenInclude(x => x.WellValues)
                        .ThenInclude(x => x.Value)
                            .ThenInclude(x => x.Attribute)
                                .ThenInclude(x => x.Element)
                .Include(x => x.Field)
                    .ThenInclude(x => x.Installation)
                .Where(x => x.MeasurementAt.Date == dateInjection.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<List<InjectionGasWell>> GetGasWellInjectionsByDate(DateTime date, Guid fieldId)
        {
            return await _context.InjectionGasWell
                    .Include(x => x.WellValues)
                        .ThenInclude(x => x.Value)
                            .ThenInclude(x => x.Attribute)
                                .ThenInclude(x => x.Element)
                                .Include(x => x.WellValues)
                                    .ThenInclude(x => x.Well)
                                        .ThenInclude(x => x.Field)
                                            .ThenInclude(x => x.Installation)
                .Where(x => x.MeasurementAt.Date == date.Date && x.WellValues.Well.Field.Id == fieldId)
                .ToListAsync();
        }

        public async Task<bool> AnyByDate(DateTime date)
        {
            return await _context.InjectionWaterGasField
                .Where(x => x.MeasurementAt.Date == date.Date && x.Status == true)
                .AnyAsync();
        }

        public async Task<List<InjectionWaterGasField>> GetInjectionsByInstallationId(Guid installationId)
        {
            return await _context.InjectionWaterGasField
                    .Include(x => x.Field)
                        .ThenInclude(x => x.Installation)
                    .Include(x => x.BalanceField)
                        .ThenInclude(x => x.InstallationBalance)
                            .ThenInclude(x => x.Installation)
                                .Where(x => x.BalanceField.InstallationBalance.Installation.Id == installationId)
                .ToListAsync();
        }
        public async Task AddWellInjectionAsync(InjectionWaterWell injection)
        {
            await _context.InjectionWaterWell.AddAsync(injection);
        }
        public async Task AddGasWellInjectionAsync(InjectionGasWell injection)
        {
            await _context.InjectionGasWell.AddAsync(injection);
        }
        public async Task AddWaterGasInjection(InjectionWaterGasField injection)
        {
            await _context.InjectionWaterGasField.AddAsync(injection);
        }

        public void UpdateWaterInjection(InjectionWaterWell injection)
        {
            _context.InjectionWaterWell
                .Update(injection);
        }

        public async Task Save()
        {
            await _context
                .SaveChangesAsync();
        }

        public async Task<InjectionGasWell?> GetGasInjectionById(Guid? id)
        {
            return await _context.InjectionGasWell
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
