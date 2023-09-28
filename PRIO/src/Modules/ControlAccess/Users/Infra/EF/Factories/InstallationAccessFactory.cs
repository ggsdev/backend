using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories
{
    public class InstallationAccessFactory
    {
        public InstallationsAccess CreateInstallationAccess(Installation installation, User user)
        {
            var accessId = Guid.NewGuid();
            return new InstallationsAccess
            {
                Installation = installation,
                User = user,
                Id = Guid.NewGuid()
            };

        }
    }
}
