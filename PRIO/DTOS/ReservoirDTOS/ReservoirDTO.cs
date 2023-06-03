using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.ReservoirDTOS
{
    public class ReservoirDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? CodReservoir { get; set; }
        public UserDTO? User { get; set; }
    }
}
