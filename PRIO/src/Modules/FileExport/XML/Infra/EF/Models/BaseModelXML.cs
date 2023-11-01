using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XML.Infra.EF.Models
{
    public class BaseModelXML : BaseModel
    {
        public string? Filename { get; set; }
        public string? FileContent { get; set; }
    }
}
