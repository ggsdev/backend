using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Interfaces
{
    public interface IProductionRepository
    {
        Task AddProduction(Production production);
        Task<Production?> GetExistingByDate(DateTime date);
        Task<Production?> GetCleanByDate(DateTime date);
        Task<Production?> GetExistingByDateWithProductionAllocation(DateTime date);
        Task<bool> AnyByDate(DateTime date);
        Task<Production?> GetProductionGasByDate(DateTime date);
        void Update(Production production);
        Task SaveChangesAsync();
        Task AddOrUpdateProduction(Production production);
        Task AddGas(Gas gas);
        Task<List<Production>> GetAllProductions();
        Task<Production?> GetById(Guid? id);
        Task<Production?> GetByIdClean(Guid? id);
        Task AddFieldProduction(FieldProduction fieldProduction);
        Task AddWaterProduction(Water water);
        void UpdateFieldProduction(FieldProduction fieldProduction);
        Task<FieldProduction?> GetFieldProductionByFieldAndProductionId(Guid fieldId, Guid productionId);
        Task<WellProduction?> GetWellProductionByWellAndProductionId(Guid wellId, Guid productionId);
        Task<WellLosses?> GetWellLossByEventAndWellProductionId(Guid eventId, Guid wellProductionId);
        Task<List<FieldProduction>> GetAllFieldProductionByProduction(Guid productionId);
        Task<Production?> GetProductionOilByDate(DateTime date);
    }
}
