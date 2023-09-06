using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Interfaces
{
    public interface IWellProductionRepository
    {
        Task Save();
        Task<List<FieldProduction>> getAllFieldsProductionsByProductionId(Guid productionId);
        Task AddAsync(Infra.EF.Models.WellProduction wellApp);
        Task AddWellLossAsync(WellLosses wellLoss);
        void Update(Infra.EF.Models.WellProduction wellApp);
        Task AddCompletionProductionAsync(CompletionProduction completionApp);
        Task<List<Infra.EF.Models.WellProduction>> GetByProductionId(Guid productionId);
        void UpdateCompletionProduction(CompletionProduction completionApp);
        Task<CompletionProduction?> GetCompletionProduction(Guid completionId, Guid productionId);
        Task AddReservoirProductionAsync(ReservoirProduction reservoirApp);
        void UpdateReservoirProduction(ReservoirProduction reservoirApp);
        Task AddZoneProductionAsync(ZoneProduction zoneApp);
        void UpdateZoneProduction(ZoneProduction zoneApp);
        Task<ReservoirProduction?> GetReservoirProductionForWellAndReservoir(Guid productionId, Guid ReservoirId);
        Task<ZoneProduction?> GetZoneProductionForWellAndReservoir(Guid productionId, Guid ReservoirId);
        Task<List<WellProduction>> GetWellProductionsByEventDate(DateTime eventDate);
    }
}
