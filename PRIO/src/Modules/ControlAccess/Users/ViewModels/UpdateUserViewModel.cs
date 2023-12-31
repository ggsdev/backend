﻿using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.ControlAccess.Users.ViewModels
{
    public class UpdateUserViewModel
    {
        [EmailAddress(ErrorMessage = "E-mail invalid")]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Description { get; set; }
        public List<Guid>? InstallationsId { get; set; }
    }
}


