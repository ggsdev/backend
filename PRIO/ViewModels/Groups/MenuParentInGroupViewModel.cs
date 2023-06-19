namespace PRIO.ViewModels.Groups
{
    public class MenuParentInGroupViewModel
    {
        public Guid? MenuId { get; set; }
        public List<MenuChildrenInGroupViewModel>? Childrens { get; set; }
        public List<OperationsInGroupViewModel>? Operations { get; set; }

    }
}
