using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.Balance.Injection.Dtos
{
    public class WaterInjectionUpdateDto
    {
        public double Total { get; set; }
        public List<WaterAssignatedValuesDto> AssignedValues { get; set; } = new();
    }

    public class WaterAssignatedValuesDto
    {
        public Guid InjectionId { get; set; }
        public double AssignedValue { get; set; }
        public UserDTO UpdatedBy { get; set; }
    }
}
