using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Shared.Errors;
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

        public async Task<bool> GetAnyByContent(string base64)
        {
            return await _context.MeasurementHistories.AnyAsync(x => x.FileContent == base64);
        }

        public async Task<List<MeasurementHistory>> GetLastUpdatedHistoriesXML(string fileType)
        {
            switch (fileType)
            {
                case "001":
                    {
                        var lastImportedDate = await _context.MeasurementHistories
                        .Where(x => x.FileType == "001")
                        .OrderByDescending(x => x.ImportedAt)
                        .Select(x => x.ImportedAt)
                        .FirstOrDefaultAsync();

                        if (lastImportedDate == default)
                        {
                            return new List<MeasurementHistory>();
                        }
                        var startDate = lastImportedDate;
                        var endDate = lastImportedDate.AddDays(1);

                        return await _context.MeasurementHistories
                            .Where(x => x.FileType == "001" && x.ImportedAt >= startDate && x.ImportedAt < endDate)
                            .ToListAsync();
                    }

                case "002":
                    {
                        var lastImportedDate = await _context.MeasurementHistories
                         .Where(x => x.FileType == "002")
                         .OrderByDescending(x => x.ImportedAt)
                         .Select(x => x.ImportedAt)
                         .FirstOrDefaultAsync();

                        if (lastImportedDate == default)
                        {
                            return new List<MeasurementHistory>();
                        }
                        var startDate = lastImportedDate;
                        var endDate = lastImportedDate.AddDays(1);

                        return await _context.MeasurementHistories
                            .Where(x => x.FileType == "002" && x.ImportedAt >= startDate && x.ImportedAt < endDate)
                            .ToListAsync();
                    }

                case "003":
                    {
                        var lastImportedDate = await _context.MeasurementHistories
                        .Where(x => x.FileType == "003")
                        .OrderByDescending(x => x.ImportedAt)
                        .Select(x => x.ImportedAt)
                        .FirstOrDefaultAsync();

                        if (lastImportedDate == default)
                        {
                            return new List<MeasurementHistory>();
                        }
                        var startDate = lastImportedDate;
                        var endDate = lastImportedDate.AddDays(1);

                        return await _context.MeasurementHistories
                            .Where(x => x.FileType == "003" && x.ImportedAt >= startDate && x.ImportedAt < endDate)
                            .ToListAsync();
                    }

                default:
                    {
                        throw new NotFoundException("Tem que ser 001, 002, 003");
                    }

                    //case "039":
                    //    {
                    //        return await _context.MeasurementHistories
                    //            .OrderByDescending(x => x.ImportedAt)
                    //            .Where(x => x.FileType == "039")
                    //           .ToListAsync();
                    //    }
            }


        }


        public async Task<List<MeasurementHistory>> GetProductionOfTheDayByImportId(Guid importId)
        {
            return await _context.MeasurementHistories
                .Include(x => x.Measurements)
                .Where(x => x.Id == importId)
                .ToListAsync();
        }
    }
}
