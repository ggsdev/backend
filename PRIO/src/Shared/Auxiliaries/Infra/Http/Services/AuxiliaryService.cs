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
            var auxliaryList = await _context.Auxiliaries
                .AsNoTracking()
                .ToListAsync();

            var tableParameter = table.ToUpper().Trim();
            var routeParameter = route.ToUpper().Trim();

            var validTables = auxliaryList
                .Select(x => x.Table)
                .Distinct()
                .ToList();

            if (!validTables.Contains(tableParameter.ToUpper()))
                throw new BadRequestException($"Invalid table options are: {string.Join(", ", validTables)}");

            var validRoutes = auxliaryList
                .Select(x => x.Route)
                .Distinct()
                .ToList();

            if (!validRoutes.Contains(routeParameter.ToUpper()))
                throw new BadRequestException($"Invalid route options are: {string.Join(" ", validRoutes)}");

            var selectOptions = auxliaryList
                .Where(x => x.Table != null && x.Table.ToUpper() == tableParameter && x.Route != null && x.Route.ToUpper() == routeParameter)
                .OrderBy(x => x.Select)
                .ToList();

            return selectOptions;
        }
    }
}
