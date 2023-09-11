using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Shared.Infra.Http.Services
{
    public class JobScheduler
    {
        private readonly DataContext _context;

        public JobScheduler(DataContext dataContext)
        {
            _context = dataContext;
        }

        public void ExecuteJob()
        {
            Console.WriteLine("asdasdsa");
        }
    }
}
