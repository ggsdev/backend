using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.Measurements.Infra.EF.Repositories
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly DataContext _context;
        public MeasurementRepository(DataContext context) { _context = context; }

        public async Task AddAsync(Measurement measurement)
        {
            await _context.AddAsync(measurement);
        }
        public async Task<bool> GetAnyImported(Guid? id)
        {
            return await _context.MeasurementHistories
                .AnyAsync(x => x.Id == id);
        }

        public void UpdateAny<T>(T entity)
        {
            _context.Update(entity);
        }
        public async Task AddRangeAsync(List<Measurement> measurements)
        {
            await _context.Measurements
                .AddRangeAsync(measurements);
        }
        public async Task<Measurement?> GetMeasurementByDate(DateTime? date, string fileType)
        {
            switch (fileType)
            {
                case "001":
                    return await _context.Measurements.FirstOrDefaultAsync(x => x.DHA_INICIO_PERIODO_MEDICAO_001 == date && x.IsActive);
                case "002":
                    return await _context.Measurements.FirstOrDefaultAsync(x => x.DHA_INICIO_PERIODO_MEDICAO_002 == date && x.IsActive);
                case "003":
                    return await _context.Measurements.FirstOrDefaultAsync(x => x.DHA_INICIO_PERIODO_MEDICAO_003 == date && x.IsActive);
                default:
                    throw new BadRequestException("arquivo: 001,002,003");
            }
        }
        public async Task<Measurement?> GetUnique039Async(string codFailure)
        {
            return await _context.Measurements.FirstOrDefaultAsync(x => x.COD_FALHA_039 == codFailure);
        }
        public async Task<bool> GetAnyByDate(DateTime? date, string fileType)
        {
            switch (fileType)
            {
                case "001":
                    return await _context.Measurements.AnyAsync(x => x.DHA_INICIO_PERIODO_MEDICAO_001 == date);

                case "002":
                    return await _context.Measurements.AnyAsync(x => x.DHA_INICIO_PERIODO_MEDICAO_002 == date);

                case "003":
                    return await _context.Measurements.AnyAsync(x => x.DHA_INICIO_PERIODO_MEDICAO_003 == date);

                default:
                    throw new BadRequestException("arquivo: 001,002,003");
            }
        }

        public async Task<Measurement?> GetUnique001Async(string numSerie)
        {
            return await _context.Measurements.FirstOrDefaultAsync(x => x.NUM_SERIE_ELEMENTO_PRIMARIO_001 == numSerie);
        }

        public void UpdateMeasurement(Measurement measurement)
        {
            _context.Update(measurement);
        }

        public async Task<Measurement?> GetUnique002Async(string numSerie)
        {
            return await _context.Measurements.FirstOrDefaultAsync(x => x.NUM_SERIE_ELEMENTO_PRIMARIO_002 == numSerie);
        }

        public async Task<Measurement?> GetUnique003Async(string numSerie)
        {
            return await _context.Measurements.FirstOrDefaultAsync(x => x.NUM_SERIE_ELEMENTO_PRIMARIO_003 == numSerie);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> GetAnyAsync(Guid id)
        {
            return await _context.Measurements
                      .AnyAsync(x => x.Id == id);
        }

        public IIncludableQueryable<FileType, User?> FileTypeBuilderByName(string name)
        {
            return _context.FileTypes
                    .Where(x => x.Name == name)
                    .Include(x => x.Measurements)
                    .ThenInclude(m => m.User);
        }
        public IIncludableQueryable<FileType, User?> FileTypeBuilder()
        {
            return _context.FileTypes
                    .Include(x => x.Measurements)
                    .ThenInclude(m => m.User);
        }
        public IIncludableQueryable<FileType, User?> FileTypeBuilderByAcronym(string acronym)
        {
            return _context.FileTypes
                    .Where(x => x.Acronym == acronym)
                    .Include(x => x.Measurements)
                    .ThenInclude(m => m.User);
        }
        public async Task<List<FileType>> FilesToListAsync(IIncludableQueryable<FileType, User?> files)
        {
            return await files.ToListAsync();
        }
        public int CountAdded()
        {
            return _context.Measurements.Count();
        }
    }
}
