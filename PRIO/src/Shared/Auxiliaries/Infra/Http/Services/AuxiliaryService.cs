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
            var validTables = new List<string>
            {
                "clusters", "installations", "fields", "zones", "reservoirs", "wells", "completions"
            };

            if (validTables.Contains(table) is false)
                throw new BadRequestException($"Invalid table options are: {string.Join(" ", validTables)}");

            var validRoutes = new List<string>
            {
                "/cadastrosBasicos"
            };

            if (validRoutes.Contains(route) is false)
                throw new BadRequestException($"Invalid route options are: {string.Join(" ", validRoutes)}");

            var selectOptions = await _context.Auxiliaries
                .Where(x => x.Table == table && x.Route == route)
                .ToListAsync();

            return selectOptions;
        }
    }
}
