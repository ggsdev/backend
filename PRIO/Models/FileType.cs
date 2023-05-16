namespace PRIO.Models
{
    public class FileType : BaseModel
    {
        public string Name { get; set; }
        public string Acronym { get; set; }
        public int QtdColumns { get; set; }
        public string Structure { get; set; }
        public string Description { get; set; }
        public List<Measurement> Measurements { get; set; }
    }
}
