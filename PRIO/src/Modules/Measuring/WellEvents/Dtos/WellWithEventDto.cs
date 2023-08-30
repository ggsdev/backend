namespace PRIO.src.Modules.Measuring.WellEvents.Dtos
{
    public class WellWithEventDto
    {
        public Guid WellId { get; set; }
        public string Status { get; set; }
        public string DateLastStatus { get; set; }
        public Guid EventId { get; set; }
    }
}
