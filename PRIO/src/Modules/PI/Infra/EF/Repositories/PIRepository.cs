using PRIO.src.Modules.PI.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.PI.Infra.EF.Repositories
{
    public class PIRepository : IPIRepository
    {
        private readonly DataContext _context;

        public PIRepository(DataContext context)
        {
            _context = context;
        }



    }
}
