using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.Measurements.Infra.EF.Repositories
{
    public class MeasurementHistoryRepository : IMeasurementHistoryRepository
    {
        private readonly DataContext _context;

        public MeasurementHistoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MeasurementHistory history)
        {
            await _context.MeasurementHistories.AddAsync(history);
        }

    }
}
