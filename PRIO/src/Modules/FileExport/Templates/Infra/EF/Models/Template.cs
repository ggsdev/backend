using PRIO.src.Modules.FileExport.Templates.Infra.EF.Enums;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.Templates.Infra.EF.Models
{
    public class Template : BaseModel
    {
        public string FileName { get; set; } = null!;
        public TypeFile TypeFile { get; set; }
        public string FileContent { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public string Group { get; set; } = null!;



    }

}
