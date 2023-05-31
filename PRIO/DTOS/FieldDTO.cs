namespace PRIO.DTOS
{
    public class FieldDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodField { get; set; }
        public string? Basin { get; set; }
        public string? State { get; set; }
        public string? Location { get; set; }
        public UserDTO User { get; set; }
        public List<ZoneDTO>? Zones { get; set; }
    }
}
