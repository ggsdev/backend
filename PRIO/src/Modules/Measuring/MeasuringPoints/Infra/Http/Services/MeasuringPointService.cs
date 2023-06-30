using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.ViewModels;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Infra.Http.Services
{
    public class MeasuringPointService
    {
        private readonly IMapper _mapper;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly IInstallationRepository _installationRepository;


        public MeasuringPointService(IMapper mapper, IMeasuringPointRepository measuringPoint, IInstallationRepository installation)
        {
            _mapper = mapper;
            _measuringPointRepository = measuringPoint;
            _installationRepository = installation;
        }
        public async Task<MeasuringPointDTO> CreateMeasuringPoint(CreateMeasuringPointViewModel body, User user)
        {
            var installationInDatabase = await _installationRepository.GetByIdAsync(body.InstallationId) ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            var measuringPointByTagInDatabase = await _measuringPointRepository.GetByTagMeasuringPoint(body.TagMeasuringPoint);

            if (measuringPointByTagInDatabase != null)
                throw new ConflictException("Tag do ponto de medição já cadastrado.");

            var measuringPointByNameInDatabase = await _measuringPointRepository.GetByMeasuringPointNameWithInstallation(body.MeasuringPointName);
            if (measuringPointByNameInDatabase != null)
                throw new ConflictException("Nome do ponto de medição já cadastrado na instalação.");

            var createMeasuringPoint = new MeasuringPoint
            {
                Id = Guid.NewGuid(),
                Name = body.MeasuringPointName,
                TagPointMeasuring = body.TagMeasuringPoint,
                Installation = installationInDatabase
            };
            await _measuringPointRepository.AddAsync(createMeasuringPoint);

            var measuringPointDTO = _mapper.Map<MeasuringPoint, MeasuringPointDTO>(createMeasuringPoint);

            return measuringPointDTO;
        }
    }
}
