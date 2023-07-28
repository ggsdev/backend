using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Measurements.Interfaces
{
    public interface IMeasurementHistoryRepository
    {
        Task AddAsync(MeasurementHistory history);
        Task<bool> GetAnyByContent(string base64);
        Task<List<MeasurementHistory>> GetLastUpdatedHistoriesXML(string fileType);
        Task<List<MeasurementHistory>> GetProductionOfTheDayByImportId(Guid importId);
    }
}
