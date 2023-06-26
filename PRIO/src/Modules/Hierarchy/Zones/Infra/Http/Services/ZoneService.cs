using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
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

        public ZoneService(IMapper mapper, SystemHistoryService systemHistoryService, IFieldRepository fieldRepository, IZoneRepository zoneRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
            _zoneRepository = zoneRepository;
            _systemHistoryService = systemHistoryService;
        }

        public async Task<CreateUpdateZoneDTO> CreateZone(CreateZoneViewModel body, User user)
        {
            var zoneInDatabase = await _zoneRepository.GetByCode(body.CodZone);

            if (zoneInDatabase is not null)
                throw new ConflictException($"Zone with this codZone is alredy registered: {body.CodZone}");

            var field = await _fieldRepository.GetByIdAsync(body.FieldId);

            if (field is null)
                throw new NotFoundException("Field not found");

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
                throw new NotFoundException("Zone not found");

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task<CreateUpdateZoneDTO> UpdateZone(UpdateZoneViewModel body, Guid id, User user)
        {
            var zone = await _zoneRepository.GetWithField(id);

            if (zone is null)
                throw new NotFoundException("Zone not found");

            var beforeChangesZone = _mapper.Map<ZoneHistoryDTO>(zone);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(zone, body);

            if (updatedProperties.Any() is false && (zone.Field?.Id == body.FieldId || body.FieldId is null))
                throw new BadRequestException("This zone already has these values, try to update to other values.");

            if (body.FieldId is not null)
            {
                var field = await _fieldRepository.GetOnlyField(body.FieldId);

                if (field is null)
                    throw new NotFoundException("Field not found");

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
            var zone = await _zoneRepository.GetWithUser(id);

            if (zone is null || zone.IsActive is false)
                throw new NotFoundException("Zone not found or inactive already");

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(zone, propertiesUpdated);

            await _systemHistoryService
                .Delete<Zone, ZoneHistoryDTO>(_tableName, user, updatedProperties, zone.Id, zone);

            _zoneRepository.Update(zone);

            await _zoneRepository.SaveChangesAsync();
        }


        public async Task<CreateUpdateZoneDTO> RestoreZone(Guid id, User user)
        {
            var zone = await _zoneRepository.GetWithUser(id);

            if (zone is null || zone.IsActive is true)
                throw new NotFoundException("Zone not found or is active already");

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
                throw new NotFoundException("Zone not found");

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
