namespace PRIO.src.Modules.Measuring.WellEvents.Dtos
{
    public class WellEventByIdDto
    {
        public Guid Id { get; set; }
        public string Uep { get; set; }
        public string Installation { get; set; }
        public string Field { get; set; }
        public string EventDateAndHour { get; set; }
        public string? EventRelated { get; set; }
        public string SystemRelated { get; set; }
        public string InitialReason { get; set; }
        public string? StatusAnp { get; set; }
        public string? StateAnp { get; set; }

        public List<ReasonDetailedDto> DetailedClosing { get; set; }
    }

    public class ReasonDetailedDto
    {
        public string SystemRelated { get; set; }
        public string StartDate { get; set; }
        public string? Downtime { get; set; }
        public TimeSpan? TimeOperating { get; set; }
    }
}
