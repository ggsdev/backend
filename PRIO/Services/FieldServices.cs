using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.Models.HierarchyModels;

namespace PRIO.Services
{
    public class FieldServices
    {
        private readonly DataContext _context;

        public FieldServices(DataContext context)
        {
            _context = context;
        }


        public async Task<Field?> GetFieldById(Guid fieldId)
        {
            return await _context.Fields.Include(x => x.Zones).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == fieldId);
        }
    }
}
