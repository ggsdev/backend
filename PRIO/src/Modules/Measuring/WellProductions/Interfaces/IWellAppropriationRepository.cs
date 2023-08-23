using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Interfaces
{
    public interface IWellAppropriationRepository
    {
        Task Save();
        Task AddAsync(WellProduction wellApp);
    }
}
