using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.Balance.Injection.Dtos
{
    public class WaterInjectionUpdateDto
    {
        public Guid FieldInjectionId { get; set; }
        public double TotalWaterInjected { get; set; }
        public List<InjectionValuesDto> AssignedValues { get; set; } = new();
    }

    public class InjectionValuesDto
    {
        public Guid InjectionId { get; set; }
        public double InjectionValue { get; set; }
        public UserDTO UpdatedBy { get; set; }
    }

    public class GasInjectionUpdateDto
    {
        public Guid FieldInjectionId { get; set; }
        public double TotalGasLift { get; set; }
        public List<InjectionValuesDto> AssignedValues { get; set; } = new();
    }
}
