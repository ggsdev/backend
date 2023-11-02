namespace PRIO.src.Modules.Balance.Balance.Dtos
{
    public class FieldWithInjectionsValuesDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? CodField { get; set; }
        public List<WellsWithInjectionsValuesDTO>? Wells { get; set; }
    }
}
