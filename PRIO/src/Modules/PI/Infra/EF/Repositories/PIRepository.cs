using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.PI.Infra.EF.Models;
using PRIO.src.Modules.PI.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.PI.Infra.EF.Repositories
{
    public class PIRepository : IPIRepository
    {
        private readonly DataContext _context;

        public PIRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Value>> GetValuesByDate(DateTime date)
        {
            return _context.Values.Include(v => v.Attribute).ThenInclude(a => a.Element).Where(x => x.Date.Date == date).ToList();
        }

    }
}
