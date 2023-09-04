using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models
{
    public class InstallationBTP : BaseModel
    {
        public Installation Installation { get; set; }
        public BTP BTP { get; set; }
    }
}
