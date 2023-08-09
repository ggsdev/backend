using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.NFSMS.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Respositories
{
    public class NFSMRepository : INFSMRepository
    {

        private readonly DataContext _context;
        public NFSMRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NFSM nfsm)
        {
            await _context.AddAsync(nfsm);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<NFSM>> GetAll()
        {
            return await _context.NFSMs
                    .Include(x => x.Installation)
                    .Include(x => x.MeasuringPoint)
                    .Include(x => x.Measurements)
                    .Include(x => x.Productions)
                    .ToListAsync();
        }

        public async Task<NFSM?> GetOneById(Guid id)
        {
            return await _context.NFSMs
                    .Include(x => x.Installation)
                    .Include(x => x.MeasuringPoint)
                    .Include(x => x.Measurements)
                    .Include(x => x.Productions)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
