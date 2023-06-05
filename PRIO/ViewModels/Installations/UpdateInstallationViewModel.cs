namespace PRIO.ViewModels.Installations
{
    public class UpdateInstallationViewModel
    {
        public string? Name { get; set; }

        public Guid? ClusterId { get; set; }

        public string? CodInstallation { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
