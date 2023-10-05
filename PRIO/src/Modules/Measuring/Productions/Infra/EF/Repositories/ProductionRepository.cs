using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Repositories
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly DataContext _context;

        public ProductionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddProduction(Production production)
        {
            await _context.Productions.AddAsync(production);
        }
        public async Task AddWaterProduction(Water water)
        {
            await _context.Waters.AddAsync(water);
        }
        public void UpdateFieldProduction(FieldProduction fieldProduction)
        {
            _context.FieldsProductions.Update(fieldProduction);
        }
        public async Task<List<FieldProduction>> GetAllFieldProductionByProduction(Guid productionId)
        {
            return await _context.FieldsProductions
                .Include(x => x.WellProductions)
                .Where(x => x.ProductionId == productionId)
                .ToListAsync();
        }
        public async Task<FieldProduction?> GetFieldProductionByFieldAndProductionId(Guid fieldId, Guid productionId)
        {
            return await _context.FieldsProductions
                .Include(x => x.WellProductions)
                .ThenInclude(x => x.WellTest)
                .Include(x => x.WellProductions)
                .ThenInclude(x => x.WellLosses)
                .FirstOrDefaultAsync(x => x.FieldId == fieldId && x.ProductionId == productionId);
        }
        public async Task<WellLosses?> GetWellLossByEventAndWellProductionId(Guid eventId, Guid wellProductionId)
        {
            return await _context.WellLosses
                .Include(x => x.Event)
                .Include(x => x.WellAllocation)
                .FirstOrDefaultAsync(x => x.Event.Id == eventId && x.WellAllocation.Id == wellProductionId);
        }
        public async Task<WellProduction?> GetWellProductionByWellAndProductionId(Guid wellId, Guid productionId)
        {
            return await _context.WellProductions
                .Include(x => x.Production)
                .Include(x => x.FieldProduction)
                .FirstOrDefaultAsync(x => x.Id == wellId && x.Production.Id == productionId);
        }
        public async Task AddFieldProduction(FieldProduction fieldProduction)
        {
            await _context.FieldsProductions.AddAsync(fieldProduction);
        }
        public async Task<Production?> GetProductionGasByDate(DateTime date)
        {
            return await _context.Productions
                .Include(x => x.Gas)
                .Where(x => x.MeasuredAt.Year == date.Year &&
                            x.MeasuredAt.Month == date.Month &&
                            x.MeasuredAt.Day == date.Day && x.IsActive)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> AnyByDate(DateTime date)
        {
            return await _context.Productions
                .Where(x => x.MeasuredAt.Year == date.Year &&
                            x.MeasuredAt.Month == date.Month &&
                            x.MeasuredAt.Day == date.Day)
                .AnyAsync();
        }
        public async Task<Production?> GetExistingByDateWithProductionAllocation(DateTime date)
        {
            return await _context.Productions
                .Include(x => x.WellProductions)
                    .ThenInclude(x => x.FieldProduction)
                .Include(x => x.FieldsFR)
                    .ThenInclude(x => x.Field)
                .Where(x => x.MeasuredAt.Year == date.Year &&
                            x.MeasuredAt.Month == date.Month &&
                            x.MeasuredAt.Day == date.Day && x.IsActive)
                .FirstOrDefaultAsync();
        }
        public async Task<Production?> GetExistingByDate(DateTime date)
        {
            return await _context.Productions
                .Include(x => x.Installation)
                .ThenInclude(x => x.Fields)
                    .ThenInclude(f => f.Wells)
                    .ThenInclude(d => d.WellTests)
                    .Include(x => x.WellProductions)
                        .ThenInclude(d => d.FieldProduction)
                .Include(x => x.Comment)
                    .ThenInclude(x => x.CommentedBy)
                .Include(x => x.GasLinear)
                .Include(x => x.GasDiferencial)
                .Include(x => x.Gas)
                .Include(x => x.Water)
                .Include(x => x.Oil)
                .Include(x => x.NFSMs)
                .Include(x => x.FieldsFR)
                    .ThenInclude(x => x.Field)
                .Include(x => x.Measurements)
                    .ThenInclude(m => m.MeasurementHistory)
                        .ThenInclude(x => x.ImportedBy)
                .Include(x => x.Measurements)
                    .ThenInclude(d => d.MeasuringPoint)
                .Where(x => x.MeasuredAt.Year == date.Year &&
                            x.MeasuredAt.Month == date.Month &&
                            x.MeasuredAt.Day == date.Day && x.IsActive)
                .FirstOrDefaultAsync();
        }
        public async Task<Production?> GetCleanByDate(DateTime date)
        {
            return await _context.Productions
                .Where(x => x.MeasuredAt.Year == date.Year &&
                            x.MeasuredAt.Month == date.Month &&
                            x.MeasuredAt.Day == date.Day && x.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task<Production?> GetById(Guid? id)
        {
            return await _context.Productions
                .Include(x => x.Installation)
                .ThenInclude(x => x.Fields)
                    .ThenInclude(f => f.Wells)
                    .ThenInclude(d => d.WellTests)
                .Include(x => x.WellProductions)
                        .ThenInclude(d => d.FieldProduction)
                .Include(x => x.Comment)
                    .ThenInclude(x => x.CommentedBy)
                .Include(x => x.GasLinear)
                .Include(x => x.GasDiferencial)
                .Include(x => x.Gas)
                .Include(x => x.Water)
                .Include(x => x.Oil)
                .Include(x => x.NFSMs)
                .Include(x => x.FieldsFR)
                    .ThenInclude(x => x.Field)
                .Include(x => x.Measurements)
                    .ThenInclude(m => m.MeasurementHistory)
                        .ThenInclude(x => x.ImportedBy)
                .Include(x => x.Measurements)
                    .ThenInclude(d => d.MeasuringPoint)
                .Where(x => x.Id == id && x.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Production>> GetAllProductions()
        {
            return await _context.Productions
                .Include(x => x.Installation)
                .Include(x => x.Water)
                .Include(x => x.GasLinear)
                .Include(x => x.GasDiferencial)
                .Include(x => x.Oil)
                .Include(x => x.Measurements)
                    .ThenInclude(m => m.MeasurementHistory)
                .OrderBy(x => x.MeasuredAt)
                    .ToListAsync();
        }

        public async Task AddOrUpdateProduction(Production production)
        {
            var existingProduction = await _context.Productions
                .FirstOrDefaultAsync(p => p.MeasuredAt.Date == production.MeasuredAt.Date);

            if (existingProduction == null)
            {
                await _context.AddAsync(production);
            }
            else
            {
                existingProduction.StatusProduction = production.StatusProduction;
                existingProduction.TotalProduction = production.TotalProduction;

                _context.Update(existingProduction);
            }
        }

        public void Update(Production production)
        {
            _context.Update(production);
        }
        public async Task AddGas(Gas gas)
        {
            await _context.Gases.AddAsync(gas);
        }
    }
}
