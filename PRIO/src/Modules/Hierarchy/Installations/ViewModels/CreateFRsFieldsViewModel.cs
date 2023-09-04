namespace PRIO.src.Modules.Hierarchy.Installations.ViewModels
{
    public class CreateFRsFieldsViewModel
    {
        public Guid InstallationId { get; set; }
        public bool isApplicableFRGas { get; set; }
        public bool isApplicableFRWater { get; set; }
        public bool isApplicableFROil { get; set; }
        public List<FRFieldsViewModel> Fields { get; set; }
    }

    public class FRViewModel
    {
        public bool IsApplicable { get; set; }
        public List<FRFieldsViewModel> Fields { get; set; }
    }
}
