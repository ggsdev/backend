namespace PRIO.src.Modules.PI.Interfaces
{
    public interface IPIRepository
    {
        Task<List<Infra.EF.Models.Attribute>> GetTagsByWellName(string wellName, string wellOperatorName);
        Task AddTag(Infra.EF.Models.Attribute atr);
        Task SaveChangesAsync();
    }
}
