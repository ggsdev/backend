using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Measurements.Interfaces
{
    public interface IMeasurementHistoryRepository
    {
        Task AddAsync(MeasurementHistory history);
    }
}
