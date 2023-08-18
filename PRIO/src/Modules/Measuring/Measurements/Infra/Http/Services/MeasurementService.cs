using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Shared.Utils;
using System.Globalization;

namespace PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services
{
    public class MeasurementService
    {
        private readonly IMeasurementHistoryRepository _measurementHistoryRepository;
        private readonly IMeasurementRepository _repository;
        private readonly IMapper _mapper;


        public MeasurementService(IMeasurementHistoryRepository measurementHistoryRepository, IMeasurementRepository repository, IMapper mapper)
        {
            _measurementHistoryRepository = measurementHistoryRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MeasurementHistory?> Import(User user, FileBasicInfoDTO File, string base64, DateTime? dateMeasurement)
        {
            var existingHistory = await _measurementHistoryRepository
                .GetAnyByContent(base64);

            DateTime result;

            if (dateMeasurement.HasValue && DateTime.TryParseExact(dateMeasurement.Value.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                var history = new MeasurementHistory
                {
                    Id = Guid.NewGuid(),
                    TypeOperation = HistoryColumns.Import,
                    ImportedBy = user,
                    ImportedAt = DateTime.UtcNow.AddHours(-3),
                    FileAcronym = File.Acronym,
                    FileName = File.Name,
                    FileType = File.Type,
                    FileContent = base64,
                    MeasuredAt = result,
                };
                if (existingHistory is false)
                    await _measurementHistoryRepository.AddAsync(history);

                return history;
            }
            return null;

            //public async Task<DTOFilesClient> GetProduction(Guid importId)
            //{
            //    var measurement = await _repository
            //        .GetProductionByImportId(importId);

            //    if (measurement is null)
            //        throw new NotFoundException("Medição não encontrada");

            //    var response = new DTOFilesClient();


            //    var fileType = measurement.MeasurementHistory.FileType;

            //    switch (fileType)
            //    {
            //        case "001":
            //            {
            //                var measurementDto = _mapper.Map<Client001DTO>(measurement);
            //                response._001File.Add(measurementDto);
            //                break;
            //            }
            //    }




            //    return response;
            //}
        }
    }
}