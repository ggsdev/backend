namespace PRIO.src.Modules.PI.Dtos
{
    public class HistoryValueDTO
    {
        public string Installation { get; set; }
        public string Field { get; set; }
        public string Well { get; set; }
        public double Value { get; set; }
        public string TAG { get; set; }
        public DateTime Date { get; set; }
    }
}
