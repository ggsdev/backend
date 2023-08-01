using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Shared.Infra.EF;
using System.Globalization;

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

        public async Task<Production?> GetExistingByDate(string date)
        {

            Console.WriteLine(date);
            if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return null;
            }

            DateTime targetDate = parsedDate;
            return await _context.Productions
                .Where(x => x.CalculatedImportedAt.Year == parsedDate.Year &&
                            x.CalculatedImportedAt.Month == parsedDate.Month &&
                            x.CalculatedImportedAt.Day == parsedDate.Day)
                .FirstOrDefaultAsync();
        }

        public async Task AddOrUpdateProduction(Production production)
        {
            var existingProduction = await _context.Productions
                .FirstOrDefaultAsync(p => p.CalculatedImportedAt.Date == production.CalculatedImportedAt.Date);

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
    }
}
