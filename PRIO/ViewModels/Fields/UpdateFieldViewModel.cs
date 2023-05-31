namespace PRIO.ViewModels.Fields
{
    public class UpdateFieldViewModel
    {
        public string? Name { get; set; }
        public string? CodField { get; set; }
        public string? Basin { get; set; }
        public string? State { get; set; }
        public string? Location { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
        public Guid? InstallationId { get; set; }
    }
}
