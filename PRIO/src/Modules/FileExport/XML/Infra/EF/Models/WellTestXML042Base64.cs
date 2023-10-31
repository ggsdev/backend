using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XML.Infra.EF.Models
{
    public class WellTestXML042Base64 : BaseModel
    {
        public WellTests? WellTest { get; set; }
        public string? FileContent { get; set; }
    }
}
