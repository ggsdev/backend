using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.FileExport.XML.Infra.EF.Repositories
{
    public class XMLExportRepository : IXMLExportRepository
    {
        private readonly DataContext _context;
        public XMLExportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(object model)
        {
            if (model is WellTests) { } else if (model is WellEvent) { }
        }

    }
}
