using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces
{
    public interface IMeasuringPointRepository
    {
        Task<MeasuringPoint?> GetByIdAsync(Guid? id);
        Task<List<MeasuringPoint>> ListAllAsync();
        Task<MeasuringPoint?> GetByTagMeasuringPoint(string? tagMeasuringPoint);
        Task<MeasuringPoint?> GetByTagMeasuringPointUpdate(string? tagMeasuringPoint, Guid installationId, Guid pointMeasuringId);
        Task<MeasuringPoint?> GetByMeasuringPointNameWithInstallation(string? measuringPointName, Guid installationId);
        Task<MeasuringPoint?> GetByMeasuringPointNameWithInstallationUpdate(string? measuringPointName, Guid installationId, Guid pointMeasuringId);
        Task Update(MeasuringPoint measuringPoint);
        void Delete(MeasuringPoint measuringPoint);
        Task Restore(MeasuringPoint measuringPoint);
        Task AddAsync(MeasuringPoint measuringPoint);
        Task<MeasuringPoint?> GetMeasuringPointWithChildren(Guid? id);
        Task SaveChangesAsync();
    }
}
