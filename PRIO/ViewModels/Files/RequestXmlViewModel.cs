using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Files
{
    public class RequestXmlViewModel
    {
        [Required]
        public List<FileContent>? Files { get; set; }
    }

    public class FileContent
    {
        [Required]
        public string? ContentBase64 { get; set; }
        [Required]
        public string? FileName { get; set; }

    }
}
