using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models
{
    public class NFSMHistory
    {
        public Guid Id { get; set; }
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;
        public DateTime MeasuredAt { get; set; }
        public User ImportedBy { get; set; }
        public NFSM NFSM { get; set; }
        public string TypeOperation { get; set; } = HistoryColumns.Import;
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileAcronym { get; set; } = string.Empty;
        public string FileContent { get; set; } = string.Empty;
    }
}
