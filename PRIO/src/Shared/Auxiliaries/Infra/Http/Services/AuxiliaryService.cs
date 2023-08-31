using Microsoft.EntityFrameworkCore;
using PRIO.src.Shared.Auxiliaries.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Shared.Auxiliaries.Infra.Http.Services
{
    public class AuxiliaryService
    {
        private readonly DataContext _context;

        public AuxiliaryService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Auxiliary>> Get(string table, string route)
        {
            var auxiliaries = await _context.Auxiliaries
                .Select(x => new
                {
                    x.Table,
                    x.Route,
                })
                .ToListAsync();

            //var validTables = auxiliaries
            //    .GroupBy(x => new { x.Table })
            //    .Select(group => group.First().Table?.ToLower())
            //    .ToList();

            //if (validTables.Contains(table.ToLower()) is false)
            //    throw new BadRequestException($"Invalid table options are: {string.Join(", ", validTables)}");

            var validRoutes = auxiliaries
                .GroupBy(x => new { x.Route })
                .Select(group => group.First().Route?.ToLower())
                .ToList();

            if (validRoutes.Contains(route.ToLower()) is false)
                throw new BadRequestException($"Invalid route options are: {string.Join(" ", validRoutes)}");

            var selectOptions = await _context.Auxiliaries
                .Where(x => x.Table == table && x.Route == route)
                .OrderBy(x => x.Select)
                .ToListAsync();

            return selectOptions;
        }
    }
}
