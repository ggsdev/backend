using System.ComponentModel.DataAnnotations;

namespace PRIO.Files._039
{

    public class XmlViewModel
    {
        [Required]
        public IFormFile File { get; set; }
        public JsonData? Json { get; set; }
    }

    public class JsonData
    {
        [Required]
        public string? UnitName { get; set; }
        [Required]
        public string? FileType { get; set; }
    }
}
