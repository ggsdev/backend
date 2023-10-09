using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Interfaces
{
    public interface IBalanceRepository
    {
        Task AddUEPBalance(UEPsBalance UEPBalance);
        Task AddInstallationBalance(InstallationsBalance UEPBalance);
        Task AddFieldBalance(FieldsBalance UEPBalance);
        Task<List<FieldsBalance>> GetBalances(List<Guid> fieldIds);
        Task<FieldsBalance?> GetBalanceField(Guid fieldId, DateTime measuredAt);
        Task<UEPsBalance?> GetUepBalance(Guid uepId, DateTime measuredAt);
        Task<FieldsBalance?> GetBalanceById(Guid fieldBalanceId);
        Task<Field?> GetDatasByBalanceId(Guid fieldId);
        void UpdateFieldBalance(FieldsBalance fieldBalance);
        void UpdateInstallationBalance(InstallationsBalance balance);
        void UpdateUepBalance(UEPsBalance balance);
    }
}
