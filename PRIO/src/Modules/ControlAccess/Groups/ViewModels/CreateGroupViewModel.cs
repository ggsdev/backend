using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.ControlAccess.Groups.ViewModels
{
    public class CreateGroupViewModel
    {
        [Required]
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public List<MenuParentInGroupViewModel>? Menus { get; set; }
    }
}
