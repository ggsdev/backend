using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Interfaces
{
    public interface IManualConfigRepository
    {
        Task AddConfigAsync(ManualWellConfiguration manualWellConfiguration);
        Task AddInjectivityAsync(InjectivityIndex injectivityIndex);
        Task AddProductivityAsync(ProductivityIndex productivityIndex);
        Task AddBuildUpAsync(BuildUp buildUp);
        Task<ManualWellConfiguration?> GetManualConfig(Guid configId);
        void UpdateInjectivity(InjectivityIndex injectivityIndex);
        void UpdateBuildUp(BuildUp BuildUp);
        void UpdateProductivity(ProductivityIndex productivityIndex);
        Task SaveAsync();
    }
}
