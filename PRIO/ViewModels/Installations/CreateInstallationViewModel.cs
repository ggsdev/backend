﻿using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Installations
{
    public class CreateInstallationViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "ClusterId is required.")]
        public Guid ClusterId { get; set; }

        public string? CodInstallation { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
