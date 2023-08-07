using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces
{
    public interface IBTPRepository
    {
        Task<List<BTP>> GetAllBTPsAsync();
        Task<List<BTPData>> GetAllBTPsDataAsync();
        Task<BTPData?> GetByWellAndDateXls(Guid wellId, string dateXls);
        Task<BTPData?> GetByWellAndApplicationDateXls(Guid wellId, string appDateXls);
        Task<List<BTPData>> GetAllBTPsDataByWellIdAsync(Guid wellId);
        Task<List<BTP>> GetAllBTPsByTypeAsync(string type);
        Task<BTP?> GetByIdAsync(Guid id);
        Task<BTPData?> GetByDateAsync(string date, Guid wellId);
        Task AddBTPAsync(BTPData data);
        Task RemoveValidate(ValidateBTP validate);
        Task AddBTPValidateAsync(ValidateBTP data);
        Task AddBTPBase64Async(BTPBase64 data);
        Task<ValidateBTP> GetValidate(Guid WellId, Guid BTPId, Guid ContentId, Guid DataId);
        Task SaveChangesAsync();
    }
}
