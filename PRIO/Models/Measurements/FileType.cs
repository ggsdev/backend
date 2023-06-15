using PRIO.Models.BaseModels;

namespace PRIO.Models.Measurements
{
    public class FileType : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public List<Measurement> Measurements { get; set; }
    }
}
