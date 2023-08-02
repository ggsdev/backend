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

        public async Task AddProduction(Production production)
        {
            await _context.Productions.AddAsync(production);
        }

        public async Task<Production?> GetExistingByDate(DateTime date)
        {

            return await _context.Productions
                .Include(x => x.GasLinear)
                .Include(x => x.GasDiferencial)
                .Include(x => x.Gas)
                .Include(x => x.Oil)
                .Include(x => x.Measurements)
                    .ThenInclude(m => m.MeasurementHistory)
                .Include(x => x.Measurements)
                    .ThenInclude(d => d.MeasuringPoint)
                .Where(x => x.MeasuredAt.Year == date.Year &&
                            x.MeasuredAt.Month == date.Month &&
                            x.MeasuredAt.Day == date.Day)
                .FirstOrDefaultAsync();
        }

        public async Task AddOrUpdateProduction(Production production)
        {
            var existingProduction = await _context.Productions
                .FirstOrDefaultAsync(p => p.MeasuredAt.Date == production.MeasuredAt.Date);

            if (existingProduction == null)
            {
                _context.Productions.Add(production);
            }
            else
            {
                existingProduction.StatusProduction = production.StatusProduction;
                existingProduction.TotalProduction = production.TotalProduction;

                _context.Productions.Update(existingProduction);
            }

            await _context.SaveChangesAsync();
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
