﻿using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Hierarchy.Completions.ViewModels
{
    public class CreateDoubleCompletionViewModel
    {
        public string? Description { get; set; }
        public decimal? AllocationReservoir { get; set; }
        [Required(ErrorMessage = "ReservoirId is required")]
        public Guid? ReservoirId { get; set; }
        [Required(ErrorMessage = "WellId is required")]
        public Guid? WellId { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CompletionUpdateId { get; set; }
        public decimal? AllocationReservoirUpdate { get; set; }

    }
}
