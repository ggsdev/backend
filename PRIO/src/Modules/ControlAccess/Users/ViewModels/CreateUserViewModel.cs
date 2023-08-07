using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.ControlAccess.Users.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
    }
}
