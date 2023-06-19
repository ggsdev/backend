using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS.HierarchyDTOS.ZoneDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.Exceptions;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.src.Shared.Utils;
using PRIO.ViewModels.HierarchyViewModels.Zones;

namespace PRIO.src.Modules.Hierarchy.Zones.Infra.Http.Services
{
    public class ZoneService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ZoneService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateUpdateZoneDTO> CreateZone(CreateZoneViewModel body, User user)
        {
            var zoneInDatabase = await _context.Zones
               .FirstOrDefaultAsync(x => x.CodZone == body.CodZone);

            if (zoneInDatabase is not null)
                throw new ConflictException($"Zone with this codZone is alredy registered: {body.CodZone}");

            var field = await _context.Fields
                .FirstOrDefaultAsync(x => x.Id == body.FieldId);

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

            await _context.Zones.AddAsync(zone);

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

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);

            return zoneDTO;

        }

        public async Task<List<ZoneDTO>> GetZones()
        {
            var zones = await _context.Zones
               .Include(x => x.User)
               .Include(x => x.Field)
               .ThenInclude(f => f!.Installation)
               .ThenInclude(i => i!.Cluster)
               .ToListAsync();

            var zonesDTO = _mapper.Map<List<Zone>, List<ZoneDTO>>(zones);
            return zonesDTO;
        }

        public async Task<ZoneDTO> GetZoneById(Guid id)
        {
            var zone = await _context.Zones
                .Include(x => x.User)
                .Include(x => x.Field)
                .ThenInclude(f => f!.Installation)
                .ThenInclude(i => i!.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (zone is null)
                throw new NotFoundException("Zone not found");

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task<CreateUpdateZoneDTO> UpdateZone(UpdateZoneViewModel body, Guid id, User user)
        {

            var zone = await _context.Zones
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (zone is null)
                throw new NotFoundException("Zone not found");

            var beforeChangesZone = _mapper.Map<ZoneHistoryDTO>(zone);

            var updatedProperties = UpdateFields.CompareAndUpdateZone(zone, body);

            if (updatedProperties.Any() is false && zone.Field?.Id == body.FieldId)
                throw new BadRequestException("This zone already has these values, try to update to other values.");

            var field = await _context.Fields
                .FirstOrDefaultAsync(x => x.Id == body.FieldId);

            if (body.FieldId is not null && field is null)
                throw new NotFoundException("Field not found");

            if (body.FieldId is not null && field is not null && zone.Field?.Id != body.FieldId)
            {
                zone.Field = field;
                updatedProperties[nameof(ZoneHistoryDTO.fieldId)] = field.Id;
            }

            _context.Zones.Update(zone);

            var firstHistory = await _context.SystemHistories
              .OrderBy(x => x.CreatedAt)
              .Where(x => x.TableItemId == id)
              .FirstOrDefaultAsync();

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

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task DeleteZone(Guid id, User user)
        {
            var zone = await _context.Zones
                .FirstOrDefaultAsync(x => x.Id == id);

            if (zone is null || zone.IsActive is false)
                throw new NotFoundException("Zone not found or inactive already");

            var lastHistory = await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == zone.Id)
               .LastOrDefaultAsync();

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
            await _context.SystemHistories.AddAsync(history);

            _context.Zones.Update(zone);

            await _context.SaveChangesAsync();
        }


        public async Task<CreateUpdateZoneDTO> RestoreZone(Guid id, User user)
        {
            var zone = await _context.Zones
              .Include(x => x.User)
              .FirstOrDefaultAsync(x => x.Id == id);

            if (zone is null || zone.IsActive is true)
                throw new NotFoundException("Zone not found or is active already");

            var lastHistory = await _context.SystemHistories
               .Where(x => x.TableItemId == zone.Id)
               .OrderBy(x => x.CreatedAt)
               .LastOrDefaultAsync();

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

            await _context.SystemHistories.AddAsync(history);

            _context.Zones.Update(zone);

            await _context.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, CreateUpdateZoneDTO>(zone);
            return zoneDTO;
        }

        public async Task<List<SystemHistory>> GetZoneHistory(Guid id)
        {
            var zoneHistories = await _context.SystemHistories
                  .Where(x => x.TableItemId == id)
                  .OrderByDescending(x => x.CreatedAt)
                  .ToListAsync();

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
