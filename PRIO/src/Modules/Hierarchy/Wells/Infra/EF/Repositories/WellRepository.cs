using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Repositories
{
    public class WellRepository : IWellRepository
    {
        private readonly DataContext _context;
        public WellRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Well?> GetByCode(string? cod)
        {
            return await _context.Wells
                    .FirstOrDefaultAsync(x => x.CodWellAnp == cod);
        }

        public async Task<Well?> GetByNameOrOperatorName(string name)
        {
            return await _context.Wells
                .Where(x => x.Name!.ToUpper().Trim() == name.ToUpper().Trim() || x.WellOperatorName!.ToUpper().Trim() == name.ToUpper().Trim())
                .Include(w => w.Field)
                    .ThenInclude(f => f.Installation)
                .FirstOrDefaultAsync();
        }
        public async Task<List<PI.Infra.EF.Models.Attribute>> GetTagsFromWell(string wellName, string wellOperatorName)
        {
            var firstList = await _context.Attributes.Where(x => x.WellName.ToUpper().Contains(wellName.ToUpper()) || x.WellName.ToUpper().Contains(wellOperatorName.ToUpper())).Include(x => x.Element).ToListAsync();
            var returnList = new List<PI.Infra.EF.Models.Attribute>();

            foreach (var atrb in firstList)
            {
                var wellNames = atrb.WellName.Split(",");
                foreach (var well in wellNames)
                {
                    if (well.ToUpper() == wellOperatorName.ToUpper() || well.ToUpper() == wellName.ToUpper())
                        returnList.Add(atrb);
                }
            }
            return returnList;
        }
        public async Task<Well?> GetByNameOrOperator(string wellName, string wellOperatorName)
        {
            return await _context.Wells
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Name.ToUpper().Trim().Contains(wellName.ToUpper().Trim()) || x.Name.ToUpper().Trim().Contains(wellOperatorName.ToUpper().Trim()));

        }

        public async Task<List<Well>> GetWellsWithEvents(Guid fieldId, string eventType)
        {
            return await _context.Wells
                .Include(x => x.WellEvents)
                .ThenInclude(d => d.EventReasons)
                .Include(x => x.Field)
                .Where(x => x.Field.Id == fieldId)
                .ToListAsync();
        }

        public async Task<Well?> GetByIdAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.User)
                .Include(x => x.ManualWellConfiguration)
                .Include(x => x.Completions)
                .ThenInclude(x => x.Reservoir)
                .ThenInclude(x => x.Zone)
                .Include(x => x.Field)
                .ThenInclude(f => f.Installation)
                .ThenInclude(i => i.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Well?> GetByIdWithEventsAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.WellEvents)
                .ThenInclude(x => x.EventReasons)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Well?> GetByNameAsync(string? name)
        {
            return await _context.Wells
                .Include(x => x.User)
                .Include(x => x.Completions)
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Name.Trim() == name.Trim());
        }

        public async Task<Well?> GetWellAndChildren(Guid? id)
        {
            return await _context.Wells
                                .Include(r => r.Completions)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Well?> GetByIdWithFieldAndCompletions(Guid? id)
        {
            return await _context.Wells
               .Include(x => x.User)
                .Include(x => x.Field)
                .Include(x => x.WellEvents)
                    .ThenInclude(x => x.EventReasons)
                .Include(x => x.Completions)
                .ThenInclude(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Well?> GetWithFieldAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.WellEvents)
                .ThenInclude(x => x.EventReasons)
                .Include(x => x.Field)
                .Include(x => x.Completions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Well?> GetWithUserAsync(Guid? id)
        {
            return await _context.Wells
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Well?> GetOnlyWellAsync(Guid? id)
        {
            return await _context.Wells
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Well well)
        {
            await _context.Wells.AddAsync(well);
        }

        public async Task<List<Well>> GetAsync(User user)
        {
            List<Guid> installationIds = user.InstallationsAccess
                 .Select(installationAccess => installationAccess.Installation.Id)
                 .ToList();

            if (user.Type == "Master")
            {
                return await _context.Wells
                    .Include(x => x.User)
                    .Include(x => x.Completions)
                    .ThenInclude(x => x.Reservoir)
                    .ThenInclude(x => x.Zone)
                    .Include(x => x.Field)
                    .ThenInclude(f => f.Installation)
                    .ThenInclude(i => i.Cluster)
                    .ToListAsync();
            }
            else
            {
                return await _context.Wells
                    .Include(x => x.User)
                    .Include(x => x.Completions)
                    .ThenInclude(x => x.Reservoir)
                    .ThenInclude(x => x.Zone)
                    .Include(x => x.Field)
                    .ThenInclude(f => f.Installation)
                    .ThenInclude(i => i.Cluster)
                    .Where(x => installationIds.Contains(x.Field.Installation.Id)).ToListAsync();
            }
        }

        public void Update(Well well)
        {
            _context.Wells.Update(well);
        }

        public void Delete(Well well)
        {
            _context.Wells.Update(well);
        }

        public void Restore(Well well)
        {
            _context.Wells.Update(well);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
