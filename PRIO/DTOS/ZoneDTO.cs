namespace PRIO.DTOS
{
    public class ZoneDTO
    {
        public Guid Id { get; set; }
        public string? CodZone { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}
