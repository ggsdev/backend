using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileImport.XML.ViewModels
{
    public class RequestXmlViewModel
    {
        [Required]
        public List<FileContent> Files { get; set; } = new();
    }

    public class FileContent
    {
        [Required(ErrorMessage = "Base64 string is required")]
        public string ContentBase64 { get; set; } = string.Empty;
        [Required(ErrorMessage = "File name is required")]
        [StringLength(3, ErrorMessage = "FileName cannot exceed 3 characters.")]
        public string FileName { get; set; } = string.Empty;

    }
}
