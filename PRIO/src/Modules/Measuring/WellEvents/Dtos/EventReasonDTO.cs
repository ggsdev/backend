namespace PRIO.src.Modules.Measuring.WellEvents.Dtos
{
    public class EventReasonDTO
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Interval { get; set; }
    }
}
