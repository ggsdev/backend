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

        public async Task AddRangeNFSMsProductionsAsync(List<NFSMsProductions> nfsmProductions)
        {
            await _context.AddRangeAsync(nfsmProductions);
        }

        public async Task AddAsync(NFSM nfsm)
        {
            await _context.AddAsync(nfsm);
        }

        public async Task AddHistoryAsync(NFSMHistory history)
        {
            await _context.AddAsync(history);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<NFSM>> GetAll()
        {
            return await _context.NFSMs
                .Include(x => x.ImportHistory)
                    .Include(x => x.Installation)
                    .Include(x => x.MeasuringPoint)
                    .Include(x => x.Measurements)
                    .Include(x => x.Productions)
                    .ToListAsync();
        }

        public async Task<NFSM?> GetOneById(Guid id)
        {
            return await _context.NFSMs
                .Include(x => x.ImportHistory)
                    .Include(x => x.Installation)
                    .Include(x => x.MeasuringPoint)
                    .Include(x => x.Measurements)
                    .Include(x => x.Productions)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<NFSMHistory?> DownloadFile(Guid id)
        {
            return await _context.NFSMImportHistories.FirstOrDefaultAsync(x => x.Id == id);

        }
    }
}
