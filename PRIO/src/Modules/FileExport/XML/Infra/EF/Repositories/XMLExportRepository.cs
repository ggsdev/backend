using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Interfaces;
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
            if (model is WellTestXML042Base64 wellTest)
            {
                await _context.WellTestXML042Base64.AddAsync(wellTest);
            }
            else if (model is WellEventXML042Base64 wellEvent)
            {
                await _context.WellEventXML042Base64.AddAsync(wellEvent);
            }
        }

    }
}
