using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
