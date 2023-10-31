using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XML.Infra.EF.Models
{
    public class WellEventXML042Base64 : BaseModel
    {
        public WellEvent? WellEvent { get; set; }
        public string? FileContent { get; set; }
    }
}
