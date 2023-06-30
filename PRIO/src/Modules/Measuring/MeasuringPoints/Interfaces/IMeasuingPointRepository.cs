using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces
{
    public interface IMeasuringPointRepository
    {
        Task<MeasuringPoint?> GetByIdAsync(Guid? id);
        Task<List<MeasuringPoint>> ListAllAsync();
        Task<MeasuringPoint?> GetByTagMeasuringPoint(string? tagMeasuringPoint);
        Task<MeasuringPoint?> GetByMeasuringPointNameWithInstallation(string? measuringPointName);
        Task AddAsync(MeasuringPoint measuringPoint);
    }
}
