﻿using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Clusters.ViewModels
{
    public class UpdateClusterViewModel
    {
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string? Name { get; set; }
        [StringLength(60, ErrorMessage = "CodCluster cannot exceed 60 characters.")]
        public string? Description { get; set; }
    }
}
