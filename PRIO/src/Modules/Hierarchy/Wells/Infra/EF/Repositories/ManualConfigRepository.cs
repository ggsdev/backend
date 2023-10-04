using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Repositories
{
    public class ManualConfigRepository : IManualConfigRepository
    {
        private readonly DataContext _context;

        public ManualConfigRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ManualWellConfiguration?> GetManualConfig(Guid configId)
        {
            return await _context.ManualWellConfiguration.Include(x => x.InjectivityIndex).Include(x => x.ProductivityIndex).Include(x => x.BuildUp).Where(x => x.Id == configId).FirstOrDefaultAsync();
        }
        public void UpdateProductivity(ProductivityIndex productivityIndex)
        {
            _context.ProductivityIndex.Update(productivityIndex);
        }
        public void UpdateInjectivity(InjectivityIndex injectivityIndex)
        {
            _context.InjectivityIndex.Update(injectivityIndex);
        }
        public void UpdateBuildUp(BuildUp BuildUp)
        {
            _context.BuildUp.Update(BuildUp);
        }
        public async Task AddConfigAsync(ManualWellConfiguration manualWellConfiguration)
        {
            await _context.ManualWellConfiguration.AddAsync(manualWellConfiguration);
        }
        public async Task AddInjectivityAsync(InjectivityIndex injectivityIndex)
        {
            await _context.InjectivityIndex.AddAsync(injectivityIndex);
        }
        public async Task AddProductivityAsync(ProductivityIndex productivityIndex)
        {
            await _context.ProductivityIndex.AddAsync(productivityIndex);
        }
        public async Task AddBuildUpAsync(BuildUp buildUp)
        {
            await _context.BuildUp.AddAsync(buildUp);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
