namespace PRIO.src.Modules.Balance.Balance.Dtos
{
    public class ValueWithInjecctionDTO
    {
        public Guid? Id { get; set; }
        public Guid? AssignedId { get; set; }
        public double? AssignedValue { get; set; }
        public string? TagName { get; set; }
        public string? Parameter { get; set; }
        public double? Amount { get; set; }
        public bool? IsCaptured { get; set; }

    }
}
