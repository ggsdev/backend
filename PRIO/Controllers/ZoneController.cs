using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.ZoneDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.Filters;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Zones;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("zones")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ZoneController : BaseApiController
    {
        public ZoneController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateZoneViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var zoneInDatabase = await _context.Zones
                .FirstOrDefaultAsync(x => x.CodZone == body.CodZone);

            if (zoneInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Zone with this codZone is alredy registered: {body.CodZone}"
                });

            var field = await _context.Fields.FirstOrDefaultAsync(x => x.Id == body.FieldId);
            if (field is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });

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

            return Created($"zones/{zone.Id}", zoneDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var zones = await _context.Zones
                .Include(x => x.User)
                .Include(x => x.Field)
                .ThenInclude(f => f!.Installation)
                .ThenInclude(i => i!.Cluster)
                .ToListAsync();

            var zonesDTO = _mapper.Map<List<Zone>, List<ZoneDTO>>(zones);

            return Ok(zonesDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var zone = await _context.Zones
                .Include(x => x.User)
                .Include(x => x.Field)
                .ThenInclude(f => f!.Installation)
                .ThenInclude(i => i!.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (zone is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);

            return Ok(zoneDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateZoneViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var zone = await _context.Zones
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (zone is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });

            var beforeChangesZone = _mapper.Map<ZoneHistoryDTO>(zone);

            var updatedProperties = ControllerUtils.CompareAndUpdateZone(zone, body);

            if (updatedProperties.Any() is false && zone.Field?.Id == body.FieldId)
                return BadRequest(new ErrorResponseDTO
                {
                    Message = "This zone already has these values, try to update to other values."
                });

            var field = await _context.Fields
                .FirstOrDefaultAsync(x => x.Id == body.FieldId);

            if (body.FieldId is not null && field is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });


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

            var changedFields = ControllerUtils.DictionaryToObject(updatedProperties);

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
            return Ok(zoneDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var zone = await _context.Zones.Include(x => x.Field).FirstOrDefaultAsync(x => x.Id == id);
            if (zone is null || zone.IsActive is false)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found or inactive already"
                });

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

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var zone = await _context.Zones
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (zone is null || zone.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found or is active already"
                });

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

            return Ok(zoneDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistoryById([FromRoute] Guid id)
        {
            var zoneHistories = await _context.SystemHistories
                  .Where(x => x.TableItemId == id)
                  .OrderByDescending(x => x.CreatedAt)
                  .ToListAsync();

            if (zoneHistories is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });

            foreach (var history in zoneHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return Ok(zoneHistories);
        }
    }
}
