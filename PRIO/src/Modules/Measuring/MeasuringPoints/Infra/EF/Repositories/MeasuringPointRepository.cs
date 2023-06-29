using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Repositories
{
    public class MeasuringPointRepository : IMeasuringPointRepository
    {
        private readonly DataContext _context;

        public MeasuringPointRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<MeasuringPoint?> GetByIdAsync(Guid? id)
        {
            return await _context.MeasuringPoints
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<MeasuringPoint?> GetByTagMeasuringPoint(string? tagMeasuringPoint)
        {
            return await _context.MeasuringPoints
                .FirstOrDefaultAsync(x => x.TagPointMeasuring == tagMeasuringPoint);
        }
        public async Task AddAsync(MeasuringPoint measuringPoint)
        {
            await _context.MeasuringPoints.AddAsync(measuringPoint);
            await _context.SaveChangesAsync();
        }
    }
}
