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
                .Include(p => p.Installation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<MeasuringPoint>> GetByInstallationIdAsync(Guid? id)
        {
            return await _context.MeasuringPoints
                .Include(p => p.Installation)
                .Where(x => x.Installation!.Id == id)
                .ToListAsync();
        }
        public async Task<List<MeasuringPoint>> GetByUEPCode(string? uepCode)
        {
            return await _context.MeasuringPoints
                .Include(p => p.Installation)
                .Where(x => x.Installation!.UepCod == uepCode && x.Installation.CodInstallationAnp == uepCode)
                .ToListAsync();
        }
        public async Task<MeasuringPoint?> GetByTagMeasuringPoint(string? tagMeasuringPoint)
        {
            return await _context.MeasuringPoints
                .Include(x => x.Installation)
                .FirstOrDefaultAsync(x => x.TagPointMeasuring == tagMeasuringPoint);
        }

        public async Task<MeasuringPoint?> GetMeasuringPointWithChildren(Guid? id)
        {
            return await _context.MeasuringPoints
               .Include(x => x.MeasuringEquipments)
               //.Include(x => x.DOR)
               //.Include(x => x.TOGRecoveredOil)
               //.Include(x => x.Section)
               //.Include(x => x.DrainVolume)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MeasuringPoint?> GetByTagMeasuringPointUpdate(string? tagMeasuringPoint, Guid installationId, Guid pointMeasuringId)
        {
            return await _context.MeasuringPoints
                .Include(x => x.Installation)
                .FirstOrDefaultAsync(x => x.TagPointMeasuring == tagMeasuringPoint && x.Installation.Id == installationId && x.Id != pointMeasuringId);
        }

        public async Task<MeasuringPoint?> GetByMeasuringPointNameWithInstallation(string? measuringPointName, Guid installationId)
        {
            return await _context.MeasuringPoints.Include(x => x.Installation)
                .FirstOrDefaultAsync(x => x.DinamicLocalMeasuringPoint == measuringPointName && x.Installation.Id == installationId);
        }
        public async Task<MeasuringPoint?> GetByMeasuringPointNameWithInstallationUpdate(string? measuringPointName, Guid installationId, Guid pointMeasuringId)
        {
            return await _context.MeasuringPoints.Include(x => x.Installation)
                .FirstOrDefaultAsync(x => x.DinamicLocalMeasuringPoint == measuringPointName && x.Installation.Id == installationId && x.Id != pointMeasuringId);
        }
        public async Task AddAsync(MeasuringPoint measuringPoint)
        {
            await _context.MeasuringPoints.AddAsync(measuringPoint);
            await _context.SaveChangesAsync();
        }
        public async Task<List<MeasuringPoint>> ListAllAsync()
        {
            var measuringPoints = await _context.MeasuringPoints.Include(x => x.Installation).ToListAsync();
            return measuringPoints;

        }
        public async Task Update(MeasuringPoint measuringPoint)
        {
            _context.MeasuringPoints.Update(measuringPoint);
            await _context.SaveChangesAsync();
        }
        public void Delete(MeasuringPoint measuringPoint)
        {
            _context.MeasuringPoints.Update(measuringPoint);
        }
        public async Task Restore(MeasuringPoint measuringPoint)
        {
            _context.MeasuringPoints.Update(measuringPoint);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
