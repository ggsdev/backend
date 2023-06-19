﻿namespace PRIO.DTOS.HistoryDTOS
{
    public class CompletionHistoryDTO
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public string? codCompletion { get; set; }
        public Guid? wellId { get; set; }
        public Guid? reservoirId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
