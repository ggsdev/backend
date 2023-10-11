namespace PRIO.src.Modules.Balance.Injection.Dtos
{
    public class WellValueDTO
    {
        public Guid Id { get; set; }
        //public Well Well { get; set; }
        public ValueSensorDTO? Value { get; set; }
    }
}
