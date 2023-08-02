using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.ViewModels
{
    public class RequestWellTestXls
    {
        [Required(ErrorMessage = "Base64 string is required")]
        public string? ContentBase64 { get; set; }
        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        public string applicationDate { get; set; }
        public bool isValid { get; set; }
        [Required(ErrorMessage = "BTPId is required")]
        public Guid BTPId { get; set; }
        [Required(ErrorMessage = "WellId is required")]
        public Guid WellId { get; set; }
    }
}
