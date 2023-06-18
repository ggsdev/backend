using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail invalid")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? Description { get; set; }
    }
}
