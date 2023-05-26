using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels
{
    public class RequestXslxViewModel
    {
        [Required]
        public string ContentBase64 { get; set; } = string.Empty;
    }
}
