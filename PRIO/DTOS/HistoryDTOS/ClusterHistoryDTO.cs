﻿namespace PRIO.DTOS.HistoryDTOS
{
    public class ClusterHistoryDTO
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public string? codCluster { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
