using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels
{
    public class RequestXmlViewModel
    {
        [Required]
        public List<FileContent> Files { get; set; } = new();
    }

    public class FileContent
    {
        [Required]
        public string ContentBase64 { get; set; } = string.Empty;
        [Required]
        public string FileName { get; set; } = string.Empty;

    }
}
