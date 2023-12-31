﻿using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Dtos;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.ViewModels;
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
        private readonly List<string> _typesAllowed = new()
        {
            "Elemento Primário","Elemento Secundário"
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
                throw new BadRequestException("Fluidos permitidos são: gás, óleo, água");

            if (body.Type is not null && !_typesAllowed.Contains(body.Type))
                throw new BadRequestException("Tipos permitidos são: Elemento Primário ou Elemento Secundário.");

            var existingMeasuringEquipment = await _equipmentRepository.GetExistingEquipment(body.SerieNumber, body.TagEquipment);

            if (existingMeasuringEquipment is not null)
            {
                if (existingMeasuringEquipment.SerieNumber == body.SerieNumber && existingMeasuringEquipment.TagEquipment == body.TagEquipment)
                    throw new ConflictException($"Já existe um equipmento com a TAG do equipmento: {existingMeasuringEquipment.TagEquipment} e com número de série: {existingMeasuringEquipment.SerieNumber} cadastrado");

                if (existingMeasuringEquipment.TagEquipment == body.TagEquipment)
                    throw new ConflictException($"Já existe um equipmento com a TAG do equipmento: {existingMeasuringEquipment.TagEquipment} cadastrado");

                if (existingMeasuringEquipment.SerieNumber == body.SerieNumber)
                    throw new ConflictException($"Já existe um equipmento com o número de série: {existingMeasuringEquipment.SerieNumber} cadastrado");
            }

            var measuringPointInDatabase = await _measuringPointRepository.GetByTagMeasuringPoint(body.TagMeasuringPoint);
            if (measuringPointInDatabase == null)
                throw new NotFoundException("Ponto de medição não encontrado.");

            if (body.Type == "Elemento Primário")
            {
                var checkUniqueEquipment = await _equipmentRepository.GetByTypeWithMeasuringPointAsync(measuringPointInDatabase.Id, body.Type);
                if (checkUniqueEquipment != null)
                    throw new ConflictException($"Já existe um {body.Type} nesse ponto de medição.");
            }

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
                throw new NotFoundException(ErrorMessages.NotFound<MeasuringEquipment>());

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return equipmentDTO;
        }

        public async Task<MeasuringEquipmentDTO> UpdateEquipment(UpdateEquipmentViewModel body, Guid id, User user)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(id);

            if (equipment is null)
                throw new NotFoundException(ErrorMessages.NotFound<MeasuringEquipment>());

            if (body.Fluid is not null && !_fluidsAllowed.Contains(body.Fluid.ToLower()))
                throw new BadRequestException("Fluidos permitidos são: gás, óleo, água");

            if (body.Type is not null && !_typesAllowed.Contains(body.Type))
                throw new BadRequestException("Tipos permitidos são: Elemento Primário, Elemento Secundário.");

            if (body.SerieNumber is not null || body.TagEquipment is not null)
            {
                var existingMeasuringEquipment = await _equipmentRepository.GetExistingEquipment(body.SerieNumber, body.TagEquipment);

                if (existingMeasuringEquipment is not null)
                {
                    if (existingMeasuringEquipment.SerieNumber == body.SerieNumber && existingMeasuringEquipment.TagEquipment == body.TagEquipment)
                        throw new ConflictException($"Já existe um equipmento com a TAG do equipmento: {existingMeasuringEquipment.TagEquipment} e com número de série: {existingMeasuringEquipment.SerieNumber} cadastrado");

                    if (existingMeasuringEquipment.TagEquipment == body.TagEquipment)
                        throw new ConflictException($"Já existe um equipmento com a TAG do equipmento: {existingMeasuringEquipment.TagEquipment} cadastrado");

                    if (existingMeasuringEquipment.SerieNumber == body.SerieNumber)
                        throw new ConflictException($"Já existe um equipmento com o número de série: {existingMeasuringEquipment.SerieNumber} cadastrado");
                }
            }

            if (body.Type == "Elemento Primário" && equipment.MeasuringPoint is not null)
            {
                var checkUniqueEquipment = await _equipmentRepository.GetByTypeWithMeasuringPointAsync(equipment.MeasuringPoint.Id, body.Type);
                if (checkUniqueEquipment != null)
                    throw new ConflictException($"Já existe um {body.Type} nesse ponto de medição.");
            }

            var beforeChangesEquipment = _mapper.Map<MeasuringEquipmentHistoryDTO>(equipment);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(equipment, body);
            if (updatedProperties.Any() is false && (equipment.MeasuringPoint?.Id == body.MeasuringId || body.MeasuringId is null))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<MeasuringEquipmentDTO>());

            if (body.TagMeasuringPoint is not null)
            {
                var measuringPointByTagMeasuringPointInDatabase = await _measuringPointRepository.GetByTagMeasuringPoint(body.TagMeasuringPoint);
                if (measuringPointByTagMeasuringPointInDatabase == null)
                    throw new NotFoundException("Ponto de medição não encontrado.");

                if (body.Type == "Elemento Primário")
                {
                    var checkUniqueEquipment = await _equipmentRepository.GetByTypeWithMeasuringPointAsync(measuringPointByTagMeasuringPointInDatabase.Id, body.Type);
                    if (checkUniqueEquipment != null)
                        throw new ConflictException($"Já existe um {body.Type} nesse ponto de medição.");
                }

                equipment.MeasuringPoint = measuringPointByTagMeasuringPointInDatabase;
                updatedProperties[nameof(MeasuringEquipmentHistoryDTO.installationId)] = measuringPointByTagMeasuringPointInDatabase.Id;
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

            if (equipment is null)
                throw new NotFoundException(ErrorMessages.NotFound<MeasuringEquipment>());

            if (equipment.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<MeasuringEquipment>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
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
            var equipment = await _equipmentRepository
                .GetWithInstallationAsync(id);

            if (equipment is null)
                throw new NotFoundException(ErrorMessages.NotFound<MeasuringEquipment>());

            if (equipment.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<MeasuringEquipment>());


            if (equipment.MeasuringPoint is null)
                throw new NotFoundException("Ponto de medição não encontrado.");

            if (equipment.MeasuringPoint.IsActive is false)
                throw new ConflictException("Ponto de medição não está ativo");


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
                throw new NotFoundException(ErrorMessages.NotFound<MeasuringEquipment>());

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
