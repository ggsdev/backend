using Microsoft.EntityFrameworkCore.Query;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Measurements.Interfaces
{
    public interface IMeasurementRepository
    {
        Task AddAsync(Measurement measurement);
        //Task AddRangeAsync(List<Measurement> measurements);
        void UpdateMeasurement(Measurement measurement);
        Task<Measurement?> GetUnique039Async(string codFailure);
        Task<Measurement?> GetUnique001Async(string numSerie);
        Task<Measurement?> GetUnique002Async(string numSerie);
        Task<Measurement?> GetUnique003Async(string numSerie);
        Task<bool> GetAnyByDate(DateTime? date, string fileType);
        //Task<Measurement?> GetMeasurementByDate(DateTime? date);
        IIncludableQueryable<FileType, User?> FileTypeBuilderByName(string name);
        IIncludableQueryable<FileType, User?> FileTypeBuilderByAcronym(string acronym);
        IIncludableQueryable<FileType, User?> FileTypeBuilder();
        Task<List<FileType>> FilesToListAsync(IIncludableQueryable<FileType, User?> files);
        Task<bool> GetAnyAsync(Guid id);
        Task SaveChangesAsync();
        int CountAdded();
        Task<bool> GetAnyImported(Guid? id);
        void UpdateAny<T>(T entity);
        Task AddRangeAsync(List<Measurement> measurements);

    }
}
