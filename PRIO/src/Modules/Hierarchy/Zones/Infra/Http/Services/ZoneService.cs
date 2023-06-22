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
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Zones.Infra.Http.Services
{
    public class ZoneService
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly ISystemHistoryRepository _systemHistoryRepository;
        private readonly IMapper _mapper;

        public ZoneService(IMapper mapper, ISystemHistoryRepository systemHistory, IFieldRepository fieldRepository, IZoneRepository zoneRepository)
        {
            _mapper = mapper;
            _systemHistoryRepository = systemHistory;
            _fieldRepository = fieldRepository;
            _zoneRepository = zoneRepository;
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

            var currentData = _mapper.Map<Zone, ZoneHistoryDTO>(zone);
            currentData.createdAt = DateTime.UtcNow;
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableZones,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = zoneId,
                CurrentData = currentData,
            };

            await _systemHistoryRepository.AddAsync(history);

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

            if (updatedProperties.Any() is false && zone.Field?.Id == body.FieldId)
                throw new BadRequestException("This zone already has these values, try to update to other values.");

            if (body.FieldId is not null)
            {
                var field = await _fieldRepository.GetOnlyField(body.FieldId);

                if (field is null)
                    throw new NotFoundException("Field not found");

                zone.Field = field;
                updatedProperties[nameof(ZoneHistoryDTO.fieldId)] = field.Id;
            }

            _zoneRepository.Update(zone);

            var firstHistory = await _systemHistoryRepository.GetFirst(id);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<Zone, ZoneHistoryDTO>(zone);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableZones,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = zone.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesZone,
            };

            await _systemHistoryRepository.AddAsync(history);

            await _zoneRepository.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task DeleteZone(Guid id, User user)
        {
            var zone = await _zoneRepository.GetWithUser(id);

            if (zone is null || zone.IsActive is false)
                throw new NotFoundException("Zone not found or inactive already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            zone.IsActive = false;
            zone.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<Zone, ZoneHistoryDTO>(zone);
            currentData.updatedAt = (DateTime)zone.DeletedAt;
            currentData.deletedAt = zone.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableZones,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = zone.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = zone.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    zone.IsActive,
                    zone.DeletedAt,
                }
            };
            await _systemHistoryRepository.AddAsync(history);

            _zoneRepository.Update(zone);

            await _zoneRepository.SaveChangesAsync();
        }


        public async Task<CreateUpdateZoneDTO> RestoreZone(Guid id, User user)
        {
            var zone = await _zoneRepository.GetWithUser(id);

            if (zone is null || zone.IsActive is true)
                throw new NotFoundException("Zone not found or is active already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            zone.IsActive = true;
            zone.DeletedAt = null;

            var currentData = _mapper.Map<Zone, ZoneHistoryDTO>(zone);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableZones,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = zone.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = zone.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    zone.IsActive,
                    zone.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);

            _zoneRepository.Update(zone);

            await _zoneRepository.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task<List<SystemHistory>> GetZoneHistory(Guid id)
        {
            var zoneHistories = await _systemHistoryRepository.GetAll(id);

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
