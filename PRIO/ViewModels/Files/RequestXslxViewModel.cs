using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Files
{
    public class RequestXslxViewModel
    {
        [Required(ErrorMessage = "Base64 string is required")]
        public string? ContentBase64 { get; set; }
    }
}
