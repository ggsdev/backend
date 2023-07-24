using AutoMapper;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;
//        new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "HPFlare",  DateTime.UtcNow
//    },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "LPFlare",  DateTime.UtcNow
//},
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "HighPressureGas", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "LowPressureGas", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas1", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas2", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ExportGas3", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas1", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas2", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "ImportGas3", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "AssistanceGas", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "PilotGas", DateTime.UtcNow },
//                new object[] { Guid.NewGuid(), "/cadastrosBasicos", "MeasuringPoint", "LocalPoint", "PurgeGas", DateTime.UtcNow },

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.Http.Services
{
    public class GasVolumeCalculationService
    {

        private readonly SystemHistoryService _systemHistoryService;
        private readonly IGasVolumeCalculationRepository _repository;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;
        private readonly string _tableName = HistoryColumns.TableGasVolume;


        public GasVolumeCalculationService(SystemHistoryService systemHistoryService, IGasVolumeCalculationRepository repository, IMapper mapper, IMeasuringPointRepository measuringPointRepository, IInstallationRepository installationRepository)
        {
            _systemHistoryService = systemHistoryService;
            _repository = repository;
            _mapper = mapper;
            _measuringPointRepository = measuringPointRepository;
            _installationRepository = installationRepository;
        }

        //public async Task<CreateUpdateGasVolumeCalculationDto> CreateGasCalculaton(CreateGasVolumeCalculationViewModel body, User user)
        //{
        //    var installation = await _installationRepository
        //        .GetByUEPCod(body.UepCode);

        //    if (installation is null)
        //        throw new NotFoundException(ErrorMessages.NotFound<Installation>());

        //    if (installation.GasVolumeCalculation is null)
        //        throw new ConflictException("Cálculo de volume não registrado nessa instalação.");

        //    foreach (var assistanceGas in body.AssistanceGases)
        //    {
        //        var measuringPoint = await _measuringPointRepository.GetByIdAsync(assistanceGas.MeasuringPointId);

        //        if (measuringPoint is null)
        //            throw new NotFoundException(ErrorMessages.NotFound<MeasuringPoint>());

        //        var assistanceGasInDatabase = await _repository
        //            .GetAssistanceGas(measuringPoint.Id);
        //        if (assistanceGasInDatabase is not null)
        //            throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {assistanceGasInDatabase.GasVolumeCalculation.Installation.Name}");
        //    }

        //    foreach (var exportGas in body.ExportGases)
        //    {
        //        var exportGasInDatabase = await _repository
        //            .GetExportGas(exportGas.StaticMeasuringPointName);
        //        var measuringPoint = await _measuringPointRepository
        //            .GetByIdAsync(exportGas.MeasuringPointId);

        //        if (measuringPoint is null)
        //            throw new NotFoundException(ErrorMessages.NotFound<MeasuringPoint>());

        //    }

        //    foreach (var assistanceGas in body.AssistanceGases)

        //    {
        //        var measuringPoint = await _measuringPointRepository
        //                .GetByIdAsync(assistanceGas.MeasuringPointId);

        //        var createdAssistanceGas = new AssistanceGas
        //        {
        //            Id = Guid.NewGuid(),
        //            MeasuringPoint = measuringPoint,
        //            StaticLocalMeasuringPoint = assistanceGas.StaticMeasuringPointName,
        //            GasVolumeCalculation = installation.GasVolumeCalculation,
        //        };

        //        await _repository.AddAssistanceGas(createdAssistanceGas);
        //    }

        //    foreach (var exportGas in body.ExportGases)
        //    {
        //        var measuringPoint = await _measuringPointRepository
        //                .GetByIdAsync(exportGas.MeasuringPointId);

        //        var createdExportGas = new ExportGas
        //        {
        //            Id = Guid.NewGuid(),
        //            MeasuringPoint = measuringPoint,
        //            StaticLocalMeasuringPoint = exportGas.StaticMeasuringPointName,
        //            GasVolumeCalculation = installation.GasVolumeCalculation,
        //        };

        //        await _repository.AddExportGas(createdExportGas);

        //    }
        //}
    }
}
