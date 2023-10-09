using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Balance.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly DataContext _context;
        public BalanceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddUEPBalance(UEPsBalance UEPBalance)
        {
            await _context.UEPsBalance.AddAsync(UEPBalance);
        }
        public async Task AddInstallationBalance(InstallationsBalance installationBalance)
        {
            await _context.InstallationsBalance.AddAsync(installationBalance);
        }
        public async Task AddFieldBalance(FieldsBalance fieldBalance)
        {
            await _context.FieldsBalance.AddAsync(fieldBalance);
        }
        public async Task<List<FieldsBalance>> GetBalances(List<Guid> fieldIds)
        {
            return await _context.FieldsBalance
                 .Include(fb => fb.FieldProduction)
                 .Where(fb => fieldIds.Contains(fb.FieldProduction.FieldId))
                 .OrderByDescending(fb => fb.MeasurementAt)
                 .ToListAsync();
        }
        public async Task<FieldsBalance?> GetBalanceById(Guid fieldBalanceId)
        {
            return await _context.FieldsBalance
                .Include(fb => fb.FieldProduction)
                 .Where(fb => fb.Id == fieldBalanceId)
                 .FirstOrDefaultAsync();
        }
        public async Task<Field?> GetDatasByBalanceId(Guid fieldId)
        {
            var field = await _context.Fields
                 .Include(f => f.Wells)
                    .ThenInclude(w => w.WellsValues)
                        .ThenInclude(wv => wv.InjectionWaterWell)
                 .Include(f => f.Wells)
                    .ThenInclude(w => w.WellsValues)
                        .ThenInclude(wv => wv.InjectionGasWell)
                 .Include(f => f.Wells)
                    .ThenInclude(w => w.WellsValues)
                        .ThenInclude(wv => wv.Value)
                            .ThenInclude(v => v.Attribute)
                                .ThenInclude(a => a.Element)
                 .Include(f => f.Wells)
                    .ThenInclude(w => w.ManualWellConfiguration)
                        .ThenInclude(mw => mw.ProductivityIndex)
                 .Include(f => f.Wells)
                    .ThenInclude(w => w.ManualWellConfiguration)
                        .ThenInclude(mw => mw.InjectivityIndex)
                 .Include(f => f.Wells)
                    .ThenInclude(w => w.ManualWellConfiguration)
                        .ThenInclude(mw => mw.BuildUp)
                 .Where(f => f.Id == fieldId)
                 .FirstOrDefaultAsync();



            return field;
        }
        public void UpdateFieldBalance(FieldsBalance fieldBalance)
        {
            _context.FieldsBalance.Update(fieldBalance);
        }

        public async Task<FieldsBalance?> GetBalanceField(Guid fieldId, DateTime measuredAt)
        {
            return await _context.FieldsBalance
                .Include(x => x.FieldProduction)
                .FirstOrDefaultAsync(x => x.FieldProduction.FieldId == fieldId && x.MeasurementAt.Date == measuredAt.Date);
        }
    }
}
