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

        public async Task<WellsValues?> GetWellValuesWithChildrens(DateTime date, Guid wellId, Infra.EF.Models.Attribute atr)
        {
            return await _context.WellValues.Include(wv => wv.Value).ThenInclude(v => v.Attribute).ThenInclude(a => a.Element).Include(wv => wv.Well).Include(wv => wv.InjectionWaterWell).Where(wv => wv.Value.Date.Date == date && wv.Well.Id == wellId && wv.Value.Attribute.Id == atr.Id).FirstOrDefaultAsync();
        }
        public async Task<List<Value>> GetValuesByDate(DateTime date)
        {
            return await _context.Values
                .Include(v => v.Attribute)
                .ThenInclude(a => a.Element)
                .Where(x => x.Date.Date == date).ToListAsync();
        }

        public async Task<WellsValues?> GetWellValuesById(Guid id)
        {
            return await _context.WellValues
                .Include(v => v.Value)
                .Include(a => a.Well)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Models.Attribute?> GetById(Guid id)
        {
            return await _context.Attributes
                .Include(x => x.Element)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public async Task<List<Models.Attribute>> GetTagsByWellName(string wellName, string wellOperatorName)
        {
            return await _context.Attributes
                .Include(x => x.Element)
                .Where(x => x.WellName.ToUpper().Trim() == wellName.ToUpper().Trim() || x.WellName.ToUpper().Trim().Contains(wellOperatorName.ToUpper().Trim()))
                .ToListAsync();
        }

        public async Task AddValue(Value value)
        {
            await _context.Values.AddAsync(value);
        }
        public async Task AddWellValue(WellsValues wellValues)
        {
            await _context.WellValues.AddAsync(wellValues);
        }

        public async Task AddTag(Models.Attribute atr)
        {
            await _context.Attributes
                .AddAsync(atr);
        }
        public async Task<bool> AnyTag(string tagName, Guid? id = null)
        {
            return await _context.Attributes
                .Where(x => x.Name.ToUpper().Trim() == tagName.ToUpper().Trim() && x.Id != id && x.IsActive)
                .AnyAsync();
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Element?> GetElementByParameter(string parameter)
        {
            return await _context.Elements
                .FirstOrDefaultAsync(x => x.Parameter.ToUpper().Trim() == parameter.ToUpper().Trim());

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Models.Attribute atr)
        {
            _context.Update(atr);
        }
    }
}
