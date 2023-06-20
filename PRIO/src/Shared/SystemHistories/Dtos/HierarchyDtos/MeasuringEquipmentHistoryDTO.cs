namespace PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos
{
    public class MeasuringEquipmentHistoryDTO
    {
        public string? tagEquipment { get; set; }
        public string? tagMeasuringPoint { get; set; }
        public string? serieNumber { get; set; }
        public string? type { get; set; }
        public string? typeEquipment { get; set; }
        public string? model { get; set; }
        public bool? hasSeal { get; set; }
        public bool? mVS { get; set; }
        public string? communicationProtocol { get; set; }
        public string? typePoint { get; set; }
        public string? channelNumber { get; set; }
        public bool? inOperation { get; set; }
        public string? fluid { get; set; }
        public Guid? installationId { get; set; }
        public string? description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
