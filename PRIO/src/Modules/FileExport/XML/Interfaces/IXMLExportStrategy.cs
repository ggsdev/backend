using PRIO.src.Modules.FileExport.Templates.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XML.Interfaces
{
    public interface IXMLExportStrategy
    {
        Task<XMLBase64> ExportXML(object model, Template templateXML);
    }
}
