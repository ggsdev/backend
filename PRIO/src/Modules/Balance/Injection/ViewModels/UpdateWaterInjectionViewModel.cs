namespace PRIO.src.Modules.Balance.Injection.ViewModels
{
    public class UpdateWaterInjectionViewModel
    {
        public List<AssignedValuesViewModel> AssignedValues { get; set; } = new();
    }

    public class AssignedValuesViewModel
    {
        public double AssignedValue { get; set; }
        public Guid InjectionId { get; set; }
    }
}
