using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services
{
    public class MeasurementService
    {
        private readonly IMeasurementHistoryRepository _measurementHistoryRepository;

        public MeasurementService(IMeasurementHistoryRepository measurementHistoryRepository)
        {
            _measurementHistoryRepository = measurementHistoryRepository;

        }

        public async Task<MeasurementHistory> Import(User user, FileBasicInfoDTO File, string base64)
        {
            var history = new MeasurementHistory
            {
                Id = Guid.NewGuid(),
                TypeOperation = HistoryColumns.Import,
                ImportedBy = user,
                ImportedAt = DateTime.UtcNow,
                FileAcronym = File.Acronym,
                FileName = File.Name,
                FileType = File.Type,
                FileContent = base64,
            };

            await _measurementHistoryRepository.AddAsync(history);

            return history;
        }

    }
}
