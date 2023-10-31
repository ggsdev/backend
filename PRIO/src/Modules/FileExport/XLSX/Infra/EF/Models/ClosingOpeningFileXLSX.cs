using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XLSX.Infra.EF.Models
{
    public class ClosingOpeningFileXLSX : BaseModel
    {
        public string FileName { get; set; } = null!;
        public string FileContent { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public string Group { get; set; } = null!;
        public Field Field { get; set; } = null!;
    }
}
