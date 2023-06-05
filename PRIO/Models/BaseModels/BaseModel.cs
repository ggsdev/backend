namespace PRIO.Models.BaseModels
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
