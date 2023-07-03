using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileImport.XLSX.ViewModels
{
    public class RequestXslxViewModel
    {
        [Required(ErrorMessage = "Base64 string is required")]
        public string? ContentBase64 { get; set; }
        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; } = string.Empty;
    }
}
