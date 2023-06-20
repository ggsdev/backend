using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Dtos
{
    public class CreateUpdateReservoirDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? CodReservoir { get; set; }
        public string? Description { get; set; }
        public UserDTO? User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
