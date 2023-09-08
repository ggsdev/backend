namespace PRIO.src.Modules.Measuring.WellEvents.Dtos
{
    public class EventWithReasonDTO
    {
        public Guid Id { get; set; }
        public string EventStatus { get; set; } = "A";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Interval { get; set; }
        public List<EventReasonDTO> EventReasons { get; set; }
    }
}
