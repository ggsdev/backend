using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileImport.XML.NFSM.ViewModels
{
    public class NFSMImportViewModel
    {
        [Required(ErrorMessage = "File is required")]
        public FileContentNfsm File { get; set; }
    }
    public class FileContentNfsm
    {
        [Required(ErrorMessage = "Base64 string is required")]
        public string ContentBase64 { get; set; } = string.Empty;
        [Required(ErrorMessage = "File name is required")]
        public string FileName { get; set; } = string.Empty;
        [StringLength(3, ErrorMessage = "FileType cannot exceed 3 characters")]
        public string FileType { get; set; } = string.Empty;
    }
}
