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

    }
}
