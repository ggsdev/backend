using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Users
{
    public class UpdateUserViewModel
    {
        [EmailAddress(ErrorMessage = "E-mail invalid")]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
    }
}


