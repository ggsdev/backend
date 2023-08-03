using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces
{
    public interface IBTPRepository
    {
        Task<List<BTP>> GetAllBTPsAsync();
        Task<List<BTP>> GetAllBTPsByTypeAsync(string type);
        Task<BTP?> GetByIdAsync(Guid id);
        Task<BTPData?> GetByDateAsync(string date);
        Task AddBTPAsync(BTPData data);
        Task AddBTPBase64Async(BTPBase64 data);
        Task SaveChangesAsync();
    }
}
