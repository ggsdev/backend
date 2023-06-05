using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Files
{
    public class RequestXslxViewModel
    {
        [Required]
        public string? ContentBase64 { get; set; }
    }
}
