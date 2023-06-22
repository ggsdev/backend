using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Dtos;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.Http.Services
{
    public class EquipmentService
    {
        private readonly IMapper _mapper;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly ISystemHistoryRepository _systemHistoryRepository;

        private readonly List<string> _fluidsAllowed = new()
        {
            "gás","óleo","água"
        };

        public EquipmentService(IMapper mapper, IEquipmentRepository equipmentRepository, IInstallationRepository installationRepository, ISystemHistoryRepository systemHistoryRepository)
        {
            _mapper = mapper;
            _equipmentRepository = equipmentRepository;
            _installationRepository = installationRepository;
            _systemHistoryRepository = systemHistoryRepository;
        }

        public async Task<MeasuringEquipmentDTO> CreateEquipment(CreateEquipmentViewModel body, User user)
        {
            if (body.Fluid is not null && !_fluidsAllowed.Contains(body.Fluid.ToLower()))
                throw new BadRequestException("Fluids allowed are: gás, óleo, água");

            var installationInDatabase = await _installationRepository.GetByIdAsync(body.InstallationId);

            if (installationInDatabase is null)
                throw new NotFoundException("Installation not found");

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
                Fluid = body.Fluid,
                Installation = installationInDatabase,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            var currentData = _mapper.Map<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(equipment);
            var currentDate = DateTime.UtcNow;
            currentData.createdAt = currentDate;
            currentData.updatedAt = currentDate;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableEquipments,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = equipmentId,
                CurrentData = currentData,
            };

            await _systemHistoryRepository.AddAsync(history);

            await _equipmentRepository.AddAsync(equipment);
            await _equipmentRepository.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return equipmentDTO;
        }

        public async Task<List<MeasuringEquipmentDTO>> GetEquipments()
        {
            var equipments = await _equipmentRepository.GetAsync();

            var equipmentsDTO = _mapper.Map<List<MeasuringEquipment>, List<MeasuringEquipmentDTO>>(equipments);
            return equipmentsDTO;
        }

        public async Task<MeasuringEquipmentDTO> GetEquipmentById(Guid id)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

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

            var updatedProperties = UpdateFields.CompareAndUpdateEquipment(equipment, body);

            if (updatedProperties.Any() is false && equipment.Installation?.Id == body.InstallationId)
                throw new BadRequestException("This equipment already has these values, try to update to other values.");

            if (body.InstallationId is not null)
            {
                var installationInDatabase = await _installationRepository.GetByIdAsync(body.InstallationId);

                if (installationInDatabase is null)
                    throw new NotFoundException("Installation not found");

                equipment.Installation = installationInDatabase;
                updatedProperties[nameof(MeasuringEquipmentHistoryDTO.installationId)] = installationInDatabase.Id;
            }

            _equipmentRepository.Update(equipment);

            var firstHistory = await _systemHistoryRepository.GetFirst(id);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(equipment);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableEquipments,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = equipment.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesEquipment,
            };

            await _systemHistoryRepository.AddAsync(history);

            await _equipmentRepository.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return equipmentDTO;
        }

        public async Task DeleteEquipment(Guid id, User user)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment is null || equipment.IsActive is false)
                throw new NotFoundException("Equipment not found or inactive already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            equipment.IsActive = false;
            equipment.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(equipment);
            currentData.updatedAt = (DateTime)equipment.DeletedAt;
            currentData.deletedAt = equipment.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableEquipments,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = equipment.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = equipment.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    equipment.IsActive,
                    equipment.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);
            _equipmentRepository.Update(equipment);

            await _equipmentRepository.SaveChangesAsync();
        }

        public async Task<MeasuringEquipmentDTO> RestoreEquipment(Guid id, User user)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment is null || equipment.IsActive is true)
                throw new NotFoundException("Equipment not found or active already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            equipment.IsActive = true;
            equipment.DeletedAt = null;

            var currentData = _mapper.Map<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(equipment);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableEquipments,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = equipment.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = equipment.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    equipment.IsActive,
                    equipment.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);

            _equipmentRepository.Update(equipment);
            await _equipmentRepository.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return equipmentDTO;
        }

        public async Task<List<SystemHistory>> GetEquipmentHistory(Guid id)
        {
            var equipmentHistories = await _systemHistoryRepository.GetAll(id);

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
