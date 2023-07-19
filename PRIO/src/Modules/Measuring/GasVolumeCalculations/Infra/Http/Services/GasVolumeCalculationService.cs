using AutoMapper;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.Http.Services
{
    public class GasVolumeCalculationService
    {

        private readonly SystemHistoryService _systemHistoryService;
        private readonly IGasVolumeCalculationRepository _repository;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly IMapper _mapper;
        private readonly string _tableName = HistoryColumns.TableGasVolume;

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

        public GasVolumeCalculationService(SystemHistoryService systemHistoryService, IGasVolumeCalculationRepository repository, IMapper mapper, IMeasuringPointRepository measuringPointRepository)
        {
            _systemHistoryService = systemHistoryService;
            _repository = repository;
            _mapper = mapper;
            _measuringPointRepository = measuringPointRepository;

        }


        //public async Task<CreateUpdateGasVolumeCalculationDto> CreateGasCalculaton(CreateGasVolumeCalculationViewModel body, User user)
        //{
        //    var measuringPoint = await _measuringPointRepository.GetByTagMeasuringPoint(body.TagMeasuringPoint);

        //    if (measuringPoint is null)
        //        throw new NotFoundException("Ponto de medição não encontrado");

        //    //var localPoint = 

        //}
    }
}
