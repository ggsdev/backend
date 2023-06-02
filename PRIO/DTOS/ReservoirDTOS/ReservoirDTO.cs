using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.ReservoirDTOS
{
    public class ReservoirDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodReservoir { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}
