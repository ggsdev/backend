using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly DataContext _context;
        public FieldRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Field?> GetByCod(string? cod)
        {
            return await _context.Fields
              .FirstOrDefaultAsync(x => x.CodField == cod);
        }
        public async Task<Field?> GetByIdAsync(Guid? id)
        {
            var field = await _context.Fields
                    .Include(x => x.User)
                    .Include(x => x.Wells)
                        .ThenInclude(x => x.WellEvents)
                    .Include(x => x.Installation)
                        .ThenInclude(i => i!.Cluster)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (field != null && field.Wells is not null)
            {
                field.Wells = field.Wells
                    .OrderBy(w => w.Name, new NaturalStringComparer())
                    .ToList();
            }

            return field;
        }

        public async Task<Field?> GetCleanById(Guid? id)
        {
            return await _context.Fields
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<Field>> GetFieldsByUepCode(string code)
        {
            return await _context.Fields
                .Include(x => x.Installation)
                .Where(x => x.Installation.UepCod == code && x.IsActive)
                .ToListAsync();
        }
        public async Task<bool> Any(Guid id)
        {
            return await _context.Fields
                .AnyAsync(x => x.Id == id);
        }

        public async Task<List<Field>> GetFieldsByInstallationId(Guid id)
        {
            return await _context.Fields
               .Include(x => x.Installation)
               .Where(x => x.Installation.Id == id && x.IsActive)
               .ToListAsync();
        }

        public async Task<Field?> GetByNameAsync(string? name)
        {
            return await _context.Fields
                .Include(x => x.User)
               .Include(x => x.Installation)
               .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Field?> GetFieldAndChildren(Guid? id)
        {
            return await _context.Fields
                        .Include(f => f.Zones)
                            .ThenInclude(z => z.Reservoirs)
                                .ThenInclude(r => r.Completions)
                        .Include(f => f.Wells)
                             .ThenInclude(w => w.WellEvents)
                                .ThenInclude(r => r.EventReasons)
                        .Include(f => f.Wells)
                             .ThenInclude(w => w.Completions)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Field?> GetOnlyField(Guid? id)
        {
            return await _context.Fields
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Field?> GetByIdWithWellsAndZonesAsync(Guid? id)
        {
            return await _context.Fields
                .Include(x => x.Wells)
                .Include(x => x.Zones)
                .Include(x => x.Installation)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Field field)
        {
            await _context.Fields.AddAsync(field);
        }

        public async Task<List<Field>> GetAsync(User user)
        {
            List<Guid> installationIds = user.InstallationsAccess
                 .Select(installationAccess => installationAccess.Installation.Id)
                 .ToList();

            if (user.Type == "Master")
            {
                return await _context.Fields
               .Include(x => x.Installation)
               .ThenInclude(i => i!.Cluster)
               .Include(x => x.Wells)
               .Include(x => x.User)
               .ToListAsync();
            }
            else
            {
                return await _context.Fields
               .Include(x => x.Installation)
               .ThenInclude(i => i!.Cluster)
               .Include(x => x.Wells)
               .Include(x => x.User)
               .Where(x => installationIds.Contains(x.Installation.Id)).ToListAsync();
            }
        }

        public void Update(Field field)
        {
            _context.Fields.Update(field);
        }

        public void Delete(Field field)
        {
            _context.Fields.Update(field);
        }

        public void Restore(Field field)
        {
            _context.Fields.Update(field);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
