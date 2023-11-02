using PRIO.src.Modules.FileExport.XML.ViewModels;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XML.Handlers
{
    public class WellTestsHandler : FileNameHandler
    {
        public override string Handle(ExportXMLViewModel body, object model)
        {
            if (model is WellTests tests)
            {
                return tests.Well.WellOperatorName;
            }
            return base.Handle(body, model);
        }
    }
}
