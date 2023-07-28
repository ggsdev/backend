using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models
{
    public class MeasurementHistory
    {
        public Guid Id { get; set; }
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;
        public DateTime? MeasuredAt { get; set; }
        public User ImportedBy { get; set; }
        public List<Measurement>? Measurements { get; set; }
        public string TypeOperation { get; set; } = HistoryColumns.Import;
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileAcronym { get; set; } = string.Empty;

        public string FileContent { get; set; } = string.Empty;
    }
}
