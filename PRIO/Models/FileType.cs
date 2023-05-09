namespace PRIO.Models
{
    public class FileType
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public int QtdColumns { get; set; }
        public string Structure { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<Measurement> Measurements { get; set; }
    }
}
