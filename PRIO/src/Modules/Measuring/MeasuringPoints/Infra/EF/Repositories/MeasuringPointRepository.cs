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

        public async Task<MeasuringPoint?> GetByMeasuringPointNameWithInstallation(string? measuringPointName)
        {
            return await _context.MeasuringPoints.Include(x => x.Installation)
                .FirstOrDefaultAsync(x => x.TagPointMeasuring == measuringPointName);
        }
        public async Task AddAsync(MeasuringPoint measuringPoint)
        {
            await _context.MeasuringPoints.AddAsync(measuringPoint);
            await _context.SaveChangesAsync();
        }
        public async Task<List<MeasuringPoint>> ListAllAsync()
        {
            var measuringPoints = await _context.MeasuringPoints.ToListAsync();
            return measuringPoints;

        }
    }
}
