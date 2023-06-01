namespace PRIO.ViewModels.Completions
{
    public class UpdateCompletionViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? ReservoirId { get; set; }
        public Guid? WellId { get; set; }
    }
}
