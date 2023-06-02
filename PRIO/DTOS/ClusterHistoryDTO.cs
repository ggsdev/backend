﻿namespace PRIO.DTOS
{
    public class ClusterHistoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodCluster { get; set; }
        public string? CodClusterOld { get; set; }
        public string? Description { get; set; }
        public string? DescriptionOld { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsActiveOld { get; set; }
        public string? CreatedAt { get; set; }
        public string? Type { get; set; }
    }
}
