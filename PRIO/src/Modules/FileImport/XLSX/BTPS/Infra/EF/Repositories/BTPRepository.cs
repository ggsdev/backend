using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Repositories
{
    public class BTPRepository : IBTPRepository
    {
        private readonly DataContext _context;

        public BTPRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<BTP>> GetAllBTPsAsync()
        {
            return await _context.BTPs.ToListAsync();
        }
        public async Task<List<BTP>> GetAllBTPsByTypeAsync(string type)
        {
            return await _context.BTPs.Where(x => x.Type == type).ToListAsync();
        }
        public async Task<BTP?> GetByIdAsync(Guid id)
        {
            return await _context.BTPs.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<BTPData?> GetByDateAsync(string date, Guid wellId)
        {
            return await _context.BTPDatas
                .Include(x => x.BTPBase64)
                .ThenInclude(x => x.User)
                .Include(x => x.Well)
                .Where(x => x.ApplicationDate == date)
                .Where(x => x.Well.Id == wellId)
                .FirstOrDefaultAsync();
        }
        public async Task AddBTPAsync(BTPData data)
        {
            await _context.BTPDatas.AddAsync(data);
        }
        public async Task AddBTPBase64Async(BTPBase64 data)
        {
            await _context.BTPBases64.AddAsync(data);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
