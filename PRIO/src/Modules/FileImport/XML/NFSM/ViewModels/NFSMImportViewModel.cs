using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileImport.XML.NFSM.ViewModels
{
    public class NFSMImportViewModel
    {
        [Required(ErrorMessage = "File is required")]
        public FileContent File { get; set; }
    }

    public class FileContent
    {
        [Required(ErrorMessage = "Base64 string is required")]
        public string ContentBase64 { get; set; }
        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; }
        [StringLength(3, ErrorMessage = "FileType cannot exceed 3 characters")]
        public string FileType { get; set; }
    }
}
