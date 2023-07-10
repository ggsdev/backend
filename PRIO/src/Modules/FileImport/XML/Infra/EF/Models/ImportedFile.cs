using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XML.Infra.EF.Models
{
    public class ImportedFile : BaseModel
    {
        public string Content { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
}
