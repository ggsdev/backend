using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool IsLocalAuthentication { get; set; } = true; // false para se autenticar com active directory
    }
}
