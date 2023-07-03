using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Infra.Http.Services
{
    public class MeasuringPointService
    {
        private readonly IMapper _mapper;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly IInstallationRepository _installationRepository;
        private readonly string _tableName = HistoryColumns.TableEquipments;


        public MeasuringPointService(IMapper mapper, IMeasuringPointRepository measuringPoint, IInstallationRepository installation, SystemHistoryService systemHistoryService)
        {
            _mapper = mapper;
            _measuringPointRepository = measuringPoint;
            _installationRepository = installation;
            _systemHistoryService = systemHistoryService;
        }
        public async Task<MeasuringPointDTO> CreateMeasuringPoint(CreateMeasuringPointViewModel body, User user)
        {
            var installationInDatabase = await _installationRepository.GetByIdAsync(body.InstallationId) ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            var measuringPointByTagInDatabase = await _measuringPointRepository.GetByTagMeasuringPoint(body.TagPointMeasuring);
            if (measuringPointByTagInDatabase != null)
                throw new ConflictException("Tag do ponto de medição já cadastrado.");

            var measuringPointByNameInDatabase = await _measuringPointRepository.GetByMeasuringPointNameWithInstallation(body.Name, body.InstallationId);
            if (measuringPointByNameInDatabase != null)
                throw new ConflictException("Nome do ponto de medição já cadastrado na instalação.");

            var measuringPointId = Guid.NewGuid();
            var measuringPoint = new MeasuringPoint
            {
                Id = measuringPointId,
                Name = body.Name,
                TagPointMeasuring = body.TagPointMeasuring,
                Installation = installationInDatabase
            };
            await _measuringPointRepository.AddAsync(measuringPoint);

            await _systemHistoryService
               .Create<MeasuringPoint, MeasuringPointHistoryDTO>(_tableName, user, measuringPointId, measuringPoint);

            await _measuringPointRepository.SaveChangesAsync();
            var measuringPointDTO = _mapper.Map<MeasuringPoint, MeasuringPointDTO>(measuringPoint);

            return measuringPointDTO;
        }
        public async Task<List<MeasuringPointDTO>> ListAll()
        {
            var measuringPoints = await _measuringPointRepository.ListAllAsync();
            var measuringPointDTO = _mapper.Map<List<MeasuringPoint>, List<MeasuringPointDTO>>(measuringPoints);

            return measuringPointDTO;
        }
        public async Task<MeasuringPointDTO> GetById(Guid id)
        {
            var measuringPoints = await _measuringPointRepository.GetByIdAsync(id);
            if (measuringPoints == null)
                throw new NotFoundException("Ponto de medição não encontrado.");

            var measuringPointDTO = _mapper.Map<MeasuringPoint, MeasuringPointDTO>(measuringPoints);
            return measuringPointDTO;
        }
        public async Task<MeasuringPointDTO> Update(Guid id, UpdateMeasuringPointViewModel body, User user)
        {
            var measuringPoint = await _measuringPointRepository.GetByIdAsync(id);
            if (measuringPoint == null)
                throw new NotFoundException("Ponto de medição não encontrado.");

            var measuringPointByTagInDatabase = await _measuringPointRepository.GetByTagMeasuringPointUpdate(body.TagPointMeasuring, measuringPoint.Installation.Id, measuringPoint.Id);
            if (measuringPointByTagInDatabase != null)
                throw new ConflictException("Tag do ponto de medição já cadastrado.");

            var measuringPointByNameInDatabase = await _measuringPointRepository.GetByMeasuringPointNameWithInstallationUpdate(body.Name, measuringPoint.Installation.Id, measuringPoint.Id);
            if (measuringPointByNameInDatabase != null)
                throw new ConflictException("Nome do ponto de medição já cadastrado na instalação.");

            var beforeChangesMeasuringPoint = _mapper.Map<MeasuringPointHistoryDTO>(measuringPoint);
            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(measuringPoint, body);
            if (updatedProperties.Any() is false)
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<MeasuringPointDTO>());

            await _measuringPointRepository.Update(measuringPoint);

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, measuringPoint.Id, measuringPoint, beforeChangesMeasuringPoint);

            await _measuringPointRepository.SaveChangesAsync();
            var measuringPointDTO = _mapper.Map<MeasuringPoint, MeasuringPointDTO>(measuringPoint);
            return measuringPointDTO;
        }
        public async Task Delete(Guid id, User user)
        {
            var measuringPoint = await _measuringPointRepository.GetByIdAsync(id);
            if (measuringPoint is null)
                throw new NotFoundException(ErrorMessages.NotFound<MeasuringPoint>());

            if (measuringPoint.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<MeasuringPoint>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(measuringPoint, propertiesUpdated);

            await _systemHistoryService
                .Delete<MeasuringPoint, MeasuringPointHistoryDTO>(_tableName, user, updatedProperties, measuringPoint.Id, measuringPoint);

            await _measuringPointRepository.Delete(measuringPoint);

            await _measuringPointRepository.SaveChangesAsync();
        }

        public async Task<MeasuringPointDTO> Restore(Guid id, User user)
        {
            var measuringPoint = await _measuringPointRepository.GetByIdAsync(id);

            if (measuringPoint is null)
                throw new NotFoundException(ErrorMessages.NotFound<MeasuringPoint>());

            if (measuringPoint.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<MeasuringPoint>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(measuringPoint, propertiesUpdated);
            await _systemHistoryService
                .Restore<MeasuringPoint, MeasuringPointHistoryDTO>(_tableName, user, updatedProperties, measuringPoint.Id, measuringPoint);

            await _measuringPointRepository.Restore(measuringPoint);
            await _measuringPointRepository.SaveChangesAsync();

            var measuringPointDTO = _mapper.Map<MeasuringPoint, MeasuringPointDTO>(measuringPoint);

            return measuringPointDTO;
        }
        public async Task<List<SystemHistory>> GetHistory(Guid id)
        {
            var measuringPointHistories = await _systemHistoryService
                .GetAll(id);

            foreach (var history in measuringPointHistories)
            {
                history.PreviousData = history.PreviousData is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!)
                    : null;

                history.CurrentData = history.CurrentData is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!)
                    : null;

                history.FieldsChanged = history.FieldsChanged is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!)
                    : null;
            }

            return measuringPointHistories;
        }
    }
}
