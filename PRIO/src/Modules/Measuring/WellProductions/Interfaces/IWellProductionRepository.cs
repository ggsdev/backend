using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Interfaces
{
    public interface IWellProductionRepository
    {
        Task Save();
        Task AddAsync(WellProduction wellApp);
    }
}
