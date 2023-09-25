using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models
{
    public class InstallationsAccess
    {
        public Guid Id { get; set; }
        public Installation Installation { get; set; }
        public User User { get; set; }
    }
}
