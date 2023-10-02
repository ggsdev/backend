using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Interfaces
{
    public interface IPIRepository
    {
        Task<List<Value>> GetValuesByDate(DateTime date);
    }
}
