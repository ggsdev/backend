namespace PRIO.src.Modules.ControlAccess.Groups.ViewModels
{
    public class MenuParentInGroupViewModel
    {
        public Guid? MenuId { get; set; }
        public List<MenuChildrenInGroupViewModel>? Childrens { get; set; }
        public List<OperationsInGroupViewModel>? Operations { get; set; }

    }
}
