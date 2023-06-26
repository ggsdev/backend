using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly DataContext _context;
        public FieldRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Field?> GetByIdAsync(Guid? id)
        {
            return await _context.Fields
                .Include(x => x.User)
               .Include(x => x.Installation)
               .ThenInclude(i => i!.Cluster)
               .FirstOrDefaultAsync(x => x.Id == id);
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
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Field field)
        {
            await _context.Fields.AddAsync(field);
        }

        public async Task<List<Field>> GetAsync()
        {
            return await _context.Fields
               .Include(x => x.Installation)
               .ThenInclude(i => i!.Cluster)
               .Include(x => x.User)
               .ToListAsync();
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
