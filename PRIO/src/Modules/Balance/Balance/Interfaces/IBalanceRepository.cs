using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Interfaces
{
    public interface IBalanceRepository
    {
        Task AddUEPBalance(UEPsBalance UEPBalance);
        Task AddInstallationBalance(InstallationsBalance UEPBalance);
        Task AddFieldBalance(FieldsBalance UEPBalance);
        void UpdateFieldBalance(FieldsBalance fieldBalance);
        Task<FieldsBalance?> GetBalanceField(Guid fieldId, DateTime measuredAt);
    }
}
