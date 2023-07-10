using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Interfaces
{
    public interface IGlobalOperationsRepository
    {
        Task<GlobalOperation> GetGlobalOperationByMetlhod(string operationName);
    }
}
