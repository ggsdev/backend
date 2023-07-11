using Microsoft.EntityFrameworkCore.Query;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Measurements.Interfaces
{
    public interface IMeasurementRepository
    {
        Task AddAsync(Measurement measurement);
        //Task AddRangeAsync(List<Measurement> measurements);
        Task<Measurement?> GetUnique039Async(string codFailure);
        Task<Measurement?> GetUnique001Async(string numSerie);
        Task<Measurement?> GetUnique002Async(string numSerie);
        Task<Measurement?> GetUnique003Async(string numSerie);
        IIncludableQueryable<FileType, User?> FileTypeBuilderByName(string name);
        IIncludableQueryable<FileType, User?> FileTypeBuilderByAcronym(string acronym);
        IIncludableQueryable<FileType, User?> FileTypeBuilder();
        Task<List<FileType>> FilesToListAsync(IIncludableQueryable<FileType, User?> files);
        Task<bool> GetAnyAsync(Guid id);
        Task SaveChangesAsync();
        int CountAdded();

    }
}
