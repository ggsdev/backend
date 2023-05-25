using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail invalid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        public bool IsLocalAuthentication { get; set; } = true; // false para se autenticar com active directory
    }
}
