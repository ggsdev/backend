namespace PRIO.Models
{
    public class FileType : BaseModel
    {
        public string Name { get; set; }
        public string Acronym { get; set; }
        public string? Description { get; set; }
        public List<Measurement> Measurements { get; set; }
    }
}
