using PRIO.Models.UserControlAccessModels;

namespace PRIO.Models
{
    public class SystemHistory
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Table { get; set; } = string.Empty;
        public User? CreatedBy { get; set; }
        public User? UpdatedBy { get; set; }
        public Guid? TableItemId { get; set; }
        public object? PreviousData { get; set; }
        public object? UpdatedData { get; set; }
        public object? FieldsChanged { get; set; }
        public string TypeOperation { get; set; } = Utils.HistoryColumns.Create;
    }
}
