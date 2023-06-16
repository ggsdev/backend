using PRIO.Utils;

namespace PRIO.Models
{
    public class SystemHistory
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Table { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? TableItemId { get; set; }
        public object? PreviousData { get; set; }
        public object? CurrentData { get; set; }
        public object? FieldsChanged { get; set; }
        public string TypeOperation { get; set; } = HistoryColumns.Create;
    }
}
