using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Dtos;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.ViewModels;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.Http.Services
{
    public class EquipmentService
    {
        private readonly IMapper _mapper;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableEquipments;

        private readonly List<string> _fluidsAllowed = new()
        {
            "gás","óleo","água"
        };

        public EquipmentService(IMapper mapper, IEquipmentRepository equipmentRepository, IInstallationRepository installationRepository, SystemHistoryService systemHistoryService, IMeasuringPointRepository measuringPoint)
        {
            _mapper = mapper;
            _measuringPointRepository = measuringPoint;
            _equipmentRepository = equipmentRepository;
            _installationRepository = installationRepository;
            _systemHistoryService = systemHistoryService;
        }

        public async Task<MeasuringEquipmentDTO> CreateEquipment(CreateEquipmentViewModel body, User user)
        {
            if (body.Fluid is not null && !_fluidsAllowed.Contains(body.Fluid.ToLower()))
                throw new BadRequestException("Fluids allowed are: gás, óleo, água");

            var measuringPointInDatabase = await _measuringPointRepository.GetByTagMeasuringPoint(body.TagMeasuringPoint);

            if (measuringPointInDatabase is null)
            {
                if (body.TagMeasuringPoint is not null && body.MeasuringPointName is not null)
                {

                    var installationInDatabase = await _installationRepository.GetByIdAsync(body.InstallationId) ?? throw new NotFoundException("Installation is not found.");
                    var createMeasuringPoint = new MeasuringPoint
                    {
                        Id = Guid.NewGuid(),
                        Name = body.MeasuringPointName,
                        TagPointMeasuring = body.TagMeasuringPoint,
                        Installation = installationInDatabase
                    };
                    await _measuringPointRepository.AddAsync(createMeasuringPoint);

                    var equipmentId = Guid.NewGuid();
                    var equipment = new MeasuringEquipment
                    {
                        Id = equipmentId,
                        TagEquipment = body.TagEquipment,
                        TagMeasuringPoint = body.TagMeasuringPoint,
                        SerieNumber = body.SerieNumber,
                        Type = body.Type,
                        TypeEquipment = body.TypeEquipment,
                        Model = body.Model,
                        HasSeal = body.HasSeal,
                        MVS = body.MVS,
                        CommunicationProtocol = body.CommunicationProtocol,
                        TypePoint = body.TypePoint,
                        ChannelNumber = body.ChannelNumber,
                        InOperation = body.InOperation,
                        Fluid = body.Fluid.ToLower(),
                        MeasuringPoint = createMeasuringPoint,
                        Description = body.Description is not null ? body.Description : null,
                        User = user,
                        IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                    };
                    await _equipmentRepository.AddAsync(equipment);

                    await _systemHistoryService
                        .Create<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(_tableName, user, equipmentId, equipment);
                    await _equipmentRepository.SaveChangesAsync();

                    var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
                    return equipmentDTO;
                }
                else
                {
                    throw new BadRequestException("Measurement point not found or data for registration of measurement point not found");
                }
            }
            else
            {
                var checkEquipment = await _equipmentRepository.getByTagsSerialChannel(body.TagMeasuringPoint, body.TagEquipment, body.SerieNumber, body.ChannelNumber);
                if (checkEquipment != null)
                    throw new ConflictException("This device is already registered");

                var equipmentId = Guid.NewGuid();
                var equipment = new MeasuringEquipment
                {
                    Id = equipmentId,
                    TagEquipment = body.TagEquipment,
                    TagMeasuringPoint = body.TagMeasuringPoint,
                    SerieNumber = body.SerieNumber,
                    Type = body.Type,
                    TypeEquipment = body.TypeEquipment,
                    Model = body.Model,
                    HasSeal = body.HasSeal,
                    MVS = body.MVS,
                    CommunicationProtocol = body.CommunicationProtocol,
                    TypePoint = body.TypePoint,
                    ChannelNumber = body.ChannelNumber,
                    InOperation = body.InOperation,
                    Fluid = body.Fluid.ToLower(),
                    MeasuringPoint = measuringPointInDatabase,
                    Description = body.Description is not null ? body.Description : null,
                    User = user,
                    IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                };
                await _equipmentRepository.AddAsync(equipment);
                await _systemHistoryService
                    .Create<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(_tableName, user, equipmentId, equipment);

                await _equipmentRepository.SaveChangesAsync();

                var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
                return equipmentDTO;
            }
        }

        public async Task<List<MeasuringEquipmentDTO>> GetEquipments()
        {
            var equipments = await _equipmentRepository.GetAsync();

            var equipmentsDTO = _mapper.Map<List<MeasuringEquipment>, List<MeasuringEquipmentDTO>>(equipments);
            return equipmentsDTO;
        }

        public async Task<MeasuringEquipmentDTO> GetEquipmentById(Guid id)
        {
            var equipment = await _equipmentRepository
                .GetWithInstallationAsync(id);

            if (equipment is null)
                throw new NotFoundException("Equipment not found");

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return equipmentDTO;
        }

        public async Task<MeasuringEquipmentDTO> UpdateEquipment(UpdateEquipmentViewModel body, Guid id, User user)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment is null)
                throw new NotFoundException("Equipment not found");

            if (body.Fluid is not null && !_fluidsAllowed.Contains(body.Fluid.ToLower()))
                throw new BadRequestException("Fluids allowed are: gás, óleo, água");

            var beforeChangesEquipment = _mapper.Map<MeasuringEquipmentHistoryDTO>(equipment);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(equipment, body);

            if (updatedProperties.Any() is false && (equipment.MeasuringPoint?.Id == body.MeasuringId || body.MeasuringId is null))
                throw new BadRequestException("This equipment already has these values, try to update to other values.");

            if (body.MeasuringId is not null)
            {
                var measuringPointInDatabase = await _measuringPointRepository.GetByIdAsync(body.MeasuringId);

                if (measuringPointInDatabase is null)
                    throw new NotFoundException("Installation not found");

                equipment.MeasuringPoint = measuringPointInDatabase;
                updatedProperties[nameof(MeasuringEquipmentHistoryDTO.installationId)] = measuringPointInDatabase.Id;
            }

            _equipmentRepository.Update(equipment);

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, equipment.Id, equipment, beforeChangesEquipment);

            await _equipmentRepository.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return equipmentDTO;
        }

        public async Task DeleteEquipment(Guid id, User user)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment is null || equipment.IsActive is false)
                throw new NotFoundException("Equipment not found or inactive already");

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(equipment, propertiesUpdated);

            await _systemHistoryService
                .Delete<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(_tableName, user, updatedProperties, equipment.Id, equipment);

            _equipmentRepository.Update(equipment);

            await _equipmentRepository.SaveChangesAsync();
        }

        public async Task<MeasuringEquipmentDTO> RestoreEquipment(Guid id, User user)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment is null || equipment.IsActive is true)
                throw new NotFoundException("Equipment not found or active already");

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(equipment, propertiesUpdated);

            await _systemHistoryService
                .Restore<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(_tableName, user, updatedProperties, equipment.Id, equipment);

            _equipmentRepository.Update(equipment);

            await _equipmentRepository.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return equipmentDTO;
        }

        public async Task<List<SystemHistory>> GetEquipmentHistory(Guid id)
        {
            var equipmentHistories = await _systemHistoryService.GetAll(id);

            if (equipmentHistories is null)
                throw new NotFoundException("Measuring Equipment not found");

            foreach (var history in equipmentHistories)
            {
                history.PreviousData = history.PreviousData is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString())
                    : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()) : null;
            }

            return equipmentHistories;
        }
    }
}
