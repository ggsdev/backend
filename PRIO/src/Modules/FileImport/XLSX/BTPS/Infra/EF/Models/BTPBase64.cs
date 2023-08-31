using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models
{
    public class BTPBase64 : BaseModel
    {
        public string Filename { get; set; }
        public string Type { get; set; }
        public string FileContent { get; set; }
        public User User { get; set; }
        public WellTests WellTest { get; set; }
    }
}
