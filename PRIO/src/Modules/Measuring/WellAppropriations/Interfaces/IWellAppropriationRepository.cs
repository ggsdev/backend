using PRIO.src.Modules.Measuring.WellAppropriations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellAppropriations.Interfaces
{
    public interface IWellAppropriationRepository
    {
        Task Save();
        Task AddAsync(WellAppropriation wellApp);
    }
}
