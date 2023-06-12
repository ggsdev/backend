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
        [Required(ErrorMessage = "Base64 string is required")]
        public string? ContentBase64 { get; set; }
        [Required(ErrorMessage = "File name is required")]
        public string? FileName { get; set; }

    }
}
