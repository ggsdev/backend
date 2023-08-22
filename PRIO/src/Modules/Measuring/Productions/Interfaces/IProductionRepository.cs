using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Interfaces
{
    public interface IProductionRepository
    {
        Task AddProduction(Production production);
        Task<Production?> GetExistingByDate(DateTime date);
        Task<bool> AnyByDate(DateTime date);
        Task<Production?> GetProductionGasByDate(DateTime date);
        void Update(Production production);
        Task SaveChangesAsync();
        Task AddOrUpdateProduction(Production production);
        Task AddGas(Gas gas);
        Task<List<Production>> GetAllProductions();
        Task<Production?> GetById(Guid? id);
        Task AddFieldProduction(FieldProduction fieldProduction);
    }
}
