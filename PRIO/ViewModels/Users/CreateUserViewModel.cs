using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "E-mail invalid")]
        public string? Email { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
    }
}
