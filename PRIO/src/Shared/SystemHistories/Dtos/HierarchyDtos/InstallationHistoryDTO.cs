﻿namespace PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos
{
    public class InstallationHistoryDTO
    {
        public string? name { get; set; }
        public string? codInstallationAnp { get; set; }
        public string? uepCod { get; set; }
        public string? uepName { get; set; }
        public string? description { get; set; }
        public double? gasSafetyBurnVolume { get; set; }
        public DateTime createdAt { get; set; }
        public Guid clusterId { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool isActive { get; set; }

    }
}
