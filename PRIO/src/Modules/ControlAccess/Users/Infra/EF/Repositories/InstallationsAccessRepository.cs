using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Repositories
{
    public class InstallationsAccessRepository : IInstallationsAccessRepository
    {
        private readonly DataContext _context;

        public InstallationsAccessRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddInstallationsAccess(InstallationsAccess userOperation)
        {

            Console.WriteLine(userOperation);
            await _context.AddAsync(userOperation);
        }
    }
}
