namespace PRIO.src.Modules.Balance.Balance.Dtos
{
    public class WellsWithInjectionsValuesDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? WellOperatorName { get; set; }
        public string? CategoryOperator { get; set; }
        public List<WellValuesDTO>? WellsValues { get; set; }
    }
}
