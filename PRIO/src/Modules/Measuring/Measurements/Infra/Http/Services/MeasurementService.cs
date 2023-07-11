using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
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

        public async Task Import(Measurement measurement, User user, FileBasicInfoDTO File)
        {
            var history = new MeasurementHistory
            {
                TypeOperation = HistoryColumns.Import,
                ImportedBy = user,
                ImportedAt = DateTime.UtcNow,
                Measurement = measurement,
                FileAcronym = File.Acronym,
                FileName = File.Name,
                FileType = File.Type,
            };

            await _measurementHistoryRepository.AddAsync(history);
        }

    }
}
