using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Interfaces
{
    public interface IPIRepository
    {
        Task<List<Value>> GetValuesByDate(DateTime date);
        Task<List<Infra.EF.Models.Attribute>> GetTagsByWellName(string wellName, string wellOperatorName);
        Task AddTag(Infra.EF.Models.Attribute atr);
        Task AddValue(Value value);
        Task AddWellValue(WellsValues wellValues);
        Task<bool> AnyTag(string tagName);
        Task<Element?> GetElementByParameter(string parameter);
        Task<WellsValues?> GetWellValuesWithChildrens(DateTime date, Guid wellId, Infra.EF.Models.Attribute atr);
        Task<WellsValues?> GetWellValuesById(Guid id);
        Task SaveChanges();

    }
}
