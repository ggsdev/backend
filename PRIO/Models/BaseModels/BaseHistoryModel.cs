namespace PRIO.Models.BaseModels
{
    public class BaseHistoryModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public string? TypeOperation { get; set; }
    }
}
