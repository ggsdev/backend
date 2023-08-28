using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
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
                .ThenInclude(x => x.BtpData)
                .FirstOrDefaultAsync(x => x.FieldId == fieldId && x.ProductionId == productionId);
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
                            x.MeasuredAt.Day == date.Day)
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

        public async Task<Production?> GetExistingByDate(DateTime date)
        {
            return await _context.Productions
                .Include(x => x.Installation)
                .Include(x => x.Comment)
                    .ThenInclude(x => x.CommentedBy)
                .Include(x => x.GasLinear)
                .Include(x => x.GasDiferencial)
                .Include(x => x.Gas)
                .Include(x => x.Water)
                .Include(x => x.Oil)
                .Include(x => x.FieldsFR)
                    .ThenInclude(x => x.Field)
                .Include(x => x.Measurements)
                    .ThenInclude(m => m.MeasurementHistory)
                .Include(x => x.Measurements)
                    .ThenInclude(d => d.MeasuringPoint)
                .Where(x => x.MeasuredAt.Year == date.Year &&
                            x.MeasuredAt.Month == date.Month &&
                            x.MeasuredAt.Day == date.Day)
                .FirstOrDefaultAsync();
        }

        public async Task<Production?> GetById(Guid? id)
        {
            return await _context.Productions
                .Include(x => x.Installation)
                    .ThenInclude(x => x.Fields)
                    .ThenInclude(f => f.Wells)
                    .ThenInclude(d => d.BTPDatas)
                    .Include(x => x.WellProductions)
                        .ThenInclude(d => d.FieldProduction)
                .Include(x => x.Comment)
                .Include(x => x.GasLinear)
                .Include(x => x.GasDiferencial)
                .Include(x => x.Gas)
                .Include(x => x.Water)
                .Include(x => x.Oil)
                .Include(x => x.FieldsFR)
                    .ThenInclude(x => x.Field)
                .Include(x => x.WellProductions)
                .Include(x => x.Measurements)
                    .ThenInclude(m => m.MeasurementHistory)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Production>> GetAllProductions()
        {
            return await _context.Productions
                .Include(x => x.Installation)
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
