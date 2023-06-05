namespace PRIO.Models.BaseModels
{
    public class BaseHistoryModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? DescriptionOld { get; set; }
        public string? Description { get; set; }
        public bool? IsActiveOld { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? TypeOperation { get; set; }
    }
}
