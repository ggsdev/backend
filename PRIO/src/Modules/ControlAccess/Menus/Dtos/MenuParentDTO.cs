namespace PRIO.src.Modules.ControlAccess.Menus.Dtos
{
    public class MenuParentDTO
    {
        public string? Name { get; set; }
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public string? Order { get; set; }
        public List<MenuChildrenDTO>? Children { get; set; }
    }
}
