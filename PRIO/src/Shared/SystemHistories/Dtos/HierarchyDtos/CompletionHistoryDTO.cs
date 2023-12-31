﻿namespace PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos
{
    public class CompletionHistoryDTO
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public decimal? topOfPerforated { get; set; }
        public decimal? baseOfPerforated { get; set; }
        public decimal? allocationReservoir { get; set; }
        public Guid? wellId { get; set; }
        public Guid? reservoirId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
