using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.ViewModels
{
    public class RequestWellTestXls
    {
        [Required(ErrorMessage = "Base64 string is required")]
        public string? ContentBase64 { get; set; }
        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; } = string.Empty;
        public Guid BTPId { get; set; }
    }
}
