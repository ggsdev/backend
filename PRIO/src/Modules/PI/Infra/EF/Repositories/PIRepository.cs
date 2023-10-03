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

        public async Task<List<Models.Attribute>> GetTagsByWellName(string wellName, string wellOperatorName)
        {
            return await _context.Attributes
                .Include(x => x.Element)
                .Where(x => x.WellName.ToUpper().Trim().Contains(wellName.ToUpper().Trim()) || x.WellName.ToUpper().Trim().Contains(wellOperatorName.ToUpper().Trim()))
                .ToListAsync();
        }

        public async Task AddTag(Models.Attribute atr)
        {
            await _context.Attributes
                .AddAsync(atr);
        }
        public async Task<bool> AnyTag(string tagName)
        {
            return await _context.Attributes
                .AnyAsync(x => x.Name.ToUpper().Trim().Contains(tagName.ToUpper().Trim()));
        }

        public async Task<Element?> GetElementByParameter(string parameter)
        {
            return await _context.Elements
                .FirstOrDefaultAsync(x => x.Parameter.ToUpper().Trim().Contains(parameter.ToUpper().Trim()));

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
