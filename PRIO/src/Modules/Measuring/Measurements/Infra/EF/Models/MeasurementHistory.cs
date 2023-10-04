using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models
{
    public class MeasurementHistory : BaseModel
    {
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow.AddHours(-3);
        public DateTime MeasuredAt { get; set; }
        public User ImportedBy { get; set; }
        public List<Measurement>? Measurements { get; set; }
        public string TypeOperation { get; set; } = HistoryColumns.Import;
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileAcronym { get; set; } = string.Empty;

        public string FileContent { get; set; } = string.Empty;
    }
}
