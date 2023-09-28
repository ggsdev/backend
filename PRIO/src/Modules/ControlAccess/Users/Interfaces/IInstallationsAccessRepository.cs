using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Interfaces
{
    public interface IInstallationsAccessRepository
    {
        Task AddInstallationsAccess(InstallationsAccess installationAccess);
    }
}
