using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileImport.XML.ViewModels
{
    public class RequestXmlViewModel
    {
        [Required]
        public List<FileContent> Files { get; set; }
    }

    public class NFSMViewModel
    {
        [Required]
        public FileContent File { get; set; }
    }

    public class FileContent
    {
        [Required(ErrorMessage = "Base64 string is required")]
        public string ContentBase64 { get; set; } = string.Empty;
        [Required(ErrorMessage = "File name is required")]
        public string FileName { get; set; } = string.Empty;
        [StringLength(3, ErrorMessage = "FileType cannot exceed 3 characters")]
        public string FileType { get; set; } = string.Empty;
    }

    public class ErrorsImportViewModel
    {
        [Required(ErrorMessage = "Errors is required")]
        public List<string> Errors { get; set; } = new();

    }
}
