using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Shared.Auxiliaries.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces
{
    public interface IBTPRepository
    {
        Task<List<BTP>> GetAllBTPsAsync();
        Task<List<WellTests>> GetBtpDatasByFieldId(Guid fieldId);
        Task<WellTests?> GetBTPsDataByWellIdAndActiveAsync(Guid wellId);
        Task<List<WellTests>> GetBtpDatasByUEP(string uep);
        Task AddBTPAsync(BTP btp);
        Task<Auxiliary?> GetTypeAsync(string type);
        Task<BTP?> GetByNameOrContent(string name, string content);
        Task<List<WellTests>> GetAllBTPsDataAsync();
        Task<List<WellTests>?> ListBTPSDataActiveByWellId(Guid wellId);
        Task<WellTests?> GetByWellAndDateXls(Guid wellId, string dateXls);
        Task<WellTests?> GetByWellAndLastDate(Guid wellId, string FinalDate);
        Task<WellTests?> GetByWellAndApplicationDateXls(Guid wellId, string appDateXls);
        Task<List<WellTests>> GetAllBTPsDataByWellIdAsync(Guid wellId);
        Task<WellTests?> GetAllBTPsDataByDataIdAsync(Guid dataId);
        Task<List<BTP>> GetAllBTPsByTypeAsync(string type);
        Task<BTP?> GetByIdAsync(Guid id);
        Task<WellTests?> GetByDataIdAsync(Guid id);
        Task<WellTests?> GetByDateAsync(string date, Guid wellId);
        Task AddBTPAsync(WellTests data);
        Task RemoveValidate(ValidateBTP validate);
        Task AddBTPValidateAsync(ValidateBTP data);
        Task AddBTPBase64Async(BTPBase64 data);
        Task<ValidateBTP> GetValidate(Guid WellId, Guid BTPId, Guid ContentId, Guid DataId);
        void Update(WellTests data);
        Task SaveChangesAsync();
    }
}
