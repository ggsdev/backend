using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Dtos;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Zones.Infra.Http.Services
{
    public class ZoneService
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly IMapper _mapper;
        private readonly string _tableName = HistoryColumns.TableZones;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly ICompletionRepository _completionRepository;
        private readonly IWellRepository _wellRepository;

        public ZoneService(IMapper mapper, SystemHistoryService systemHistoryService, IFieldRepository fieldRepository, IZoneRepository zoneRepository, IReservoirRepository reservoirRepository, ICompletionRepository completionRepository, IWellRepository wellRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
            _zoneRepository = zoneRepository;
            _systemHistoryService = systemHistoryService;
            _reservoirRepository = reservoirRepository;
            _completionRepository = completionRepository;
            _wellRepository = wellRepository;
        }

        public async Task<CreateUpdateZoneDTO> CreateZone(CreateZoneViewModel body, User user)
        {
            var zoneInDatabase = await _zoneRepository.GetByCode(body.CodZone);

            if (zoneInDatabase is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Zone>());

            var field = await _fieldRepository.GetByIdAsync(body.FieldId);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Field>());

            var zoneId = Guid.NewGuid();

            var zone = new Zone
            {
                Id = zoneId,
                CodZone = body.CodZone,
                Field = field,
                Description = body.Description,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };
            await _zoneRepository.AddAsync(zone);

            await _systemHistoryService
                .Create<Zone, ZoneHistoryDTO>(_tableName, user, zoneId, zone);

            await _zoneRepository.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);

            return zoneDTO;
        }

        public async Task<List<ZoneDTO>> GetZones()
        {
            var zones = await _zoneRepository.GetAsync();

            var zonesDTO = _mapper.Map<List<Zone>, List<ZoneDTO>>(zones);
            return zonesDTO;
        }

        public async Task<ZoneDTO> GetZoneById(Guid id)
        {
            var zone = await _zoneRepository.GetByIdAsync(id);

            if (zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task<CreateUpdateZoneDTO> UpdateZone(UpdateZoneViewModel body, Guid id, User user)
        {
            var zone = await _zoneRepository.GetByIdWithReservoirsAsync(id);
            Console.WriteLine(zone.Id);

            if (zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (zone.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Zone>());

            if (zone.Reservoirs is not null)
                if (zone.Reservoirs.Count > 0)
                    if (body.CodZone is not null)
                        if (body.CodZone != zone.CodZone)
                            throw new ConflictException(ErrorMessages.CodCantBeUpdated<Zone>());

            if (zone.Reservoirs is not null)
                if (zone.Reservoirs.Count > 0)
                    if (body.FieldId is not null)
                        if (body.FieldId != zone.Field.Id)
                            throw new ConflictException("Relacionamento não pode ser alterado.");

            if (body.CodZone is not null)
            {
                var zoneInDatabase = await _zoneRepository.GetByCode(body.CodZone);
                if (zoneInDatabase is not null && zoneInDatabase.Id != zone.Id)
                    throw new ConflictException(ErrorMessages.CodAlreadyExists<Zone>());
            }

            var beforeChangesZone = _mapper.Map<ZoneHistoryDTO>(zone);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(zone, body);

            if (updatedProperties.Any() is false && (zone.Field?.Id == body.FieldId || body.FieldId is null))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Zone>());

            if (body.FieldId is not null && zone.Field?.Id != body.FieldId)
            {
                var field = await _fieldRepository.GetOnlyField(body.FieldId);

                if (field is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Field>());

                zone.Field = field;
                updatedProperties[nameof(ZoneHistoryDTO.fieldId)] = field.Id;
            }


            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, zone.Id, zone, beforeChangesZone);

            _zoneRepository.Update(zone);
            await _zoneRepository.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task DeleteZone(Guid id, User user)
        {
            var zone = await _zoneRepository
                .GetZoneAndChildren(id);

            if (zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (zone.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Zone>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(zone, propertiesUpdated);

            _zoneRepository.Update(zone);

            await _systemHistoryService
                .Delete<Zone, ZoneHistoryDTO>(_tableName, user, updatedProperties, zone.Id, zone);

            if (zone.Reservoirs is not null)
                foreach (var reservoir in zone.Reservoirs)
                {
                    if (reservoir.IsActive is true)
                    {
                        var reservoirPropertiesToUpdate = new
                        {
                            IsActive = false,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                        };

                        var reservoirUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(reservoir, reservoirPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, reservoirUpdatedProperties, reservoir.Id, reservoir);
                        _reservoirRepository.Delete(reservoir);

                    }

                    if (reservoir.Completions is not null)
                        foreach (var completion in reservoir.Completions)
                        {
                            if (completion.IsActive is true)
                            {
                                var completionPropertiesToUpdate = new
                                {
                                    IsActive = false,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                };

                                var completionUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, completionUpdatedProperties, completion.Id, completion);

                                _completionRepository.Delete(completion);

                            }
                        }
                }

            await _zoneRepository.SaveChangesAsync();
        }


        public async Task<CreateUpdateZoneDTO> RestoreZone(Guid id, User user)
        {
            var zone = await _zoneRepository.GetWithField(id);

            if (zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (zone.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Zone>());

            if (zone.Field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (zone.Field.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Field>());


            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };
            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(zone, propertiesUpdated);

            await _systemHistoryService
                .Restore<Zone, ZoneHistoryDTO>(_tableName, user, updatedProperties, zone.Id, zone);

            _zoneRepository.Update(zone);

            await _zoneRepository.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task<List<SystemHistory>> GetZoneHistory(Guid id)
        {
            var zoneHistories = await _systemHistoryService.GetAll(id);

            if (zoneHistories is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            foreach (var history in zoneHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return zoneHistories;
        }
    }
}
