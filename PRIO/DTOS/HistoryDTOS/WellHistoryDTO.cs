namespace PRIO.DTOS.HistoryDTOS
{
    public class WellHistoryDTO
    {
        public string? codWell { get; set; }
        public string? name { get; set; }
        public string? wellOperatorName { get; set; }
        public string? codWellAnp { get; set; }
        public string? categoryAnp { get; set; }
        public string? categoryReclassificationAnp { get; set; }
        public string? categoryOperator { get; set; }
        public bool? statusOperator { get; set; }
        public string? type { get; set; }
        public decimal? waterDepth { get; set; }
        public decimal? topOfPerforated { get; set; }
        public decimal? baseOfPerforated { get; set; }
        public string? artificialLift { get; set; }
        public string? latitude4C { get; set; }
        public string? longitude4C { get; set; }
        public string? latitudeDD { get; set; }
        public string? longitudeDD { get; set; }
        public string? datumHorizontal { get; set; }
        public string? typeBaseCoordinate { get; set; }
        public string? typeOperation { get; set; }
        public string? coordX { get; set; }
        public string? coordY { get; set; }
        public string? description { get; set; }
        public Guid fieldId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public bool? isActive { get; set; }
    }
}
