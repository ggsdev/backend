using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.ZoneDTOS;
using PRIO.Models.Zones;
using PRIO.Utils;
using PRIO.ViewModels.Zones;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("zones")]
    public class ZoneController : BaseApiController
    {
        public ZoneController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateZoneViewModel body)
        {
            var zoneInDatabase = await _context.Zones.FirstOrDefaultAsync(x => x.CodZone == body.CodZone);
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

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });
            }
            var zone = new Zone
            {
                CodZone = body.CodZone,
                Field = field,
                Description = body.Description,
                User = user,
            };

            await _context.Zones.AddAsync(zone);

            var zoneHistory = new ZoneHistory
            {
                CodZone = zone.CodZone,

                Field = field,

                Description = zone.Description,

                User = user,
                Zone = zone,

                TypeOperation = TypeOperation.Create
            };

            await _context.ZoneHistories.AddAsync(zoneHistory);
            await _context.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);

            return Created($"zones/{zone.Id}", zoneDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var zones = await _context.Zones.Include(x => x.Reservoirs).Include(x => x.ZoneHistories).Include(x => x.User).ToListAsync();
            var zonesDTO = _mapper.Map<List<Zone>, List<ZoneDTO>>(zones);

            return Ok(zonesDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var zone = await _context.Zones.Include(x => x.Reservoirs).Include(x => x.ZoneHistories).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (zone is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });
            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);

            return Ok(zoneDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistoryById([FromRoute] Guid id)
        {
            var zoneHistories = await _context.ZoneHistories
                .Include(x => x.User)
                .Include(x => x.Field)
                .Include(x => x.Zone)
                .Where(x => x.Zone.Id == id)
                .ToListAsync();

            if (zoneHistories is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });

            var zoneHistoriesDTO = _mapper.Map<List<ZoneHistory>, List<ZoneHistoryDTO>>(zoneHistories);

            return Ok(zoneHistoriesDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateZoneViewModel body)
        {
            var zone = await _context.Zones.Include(x => x.Field).FirstOrDefaultAsync(x => x.Id == id);
            if (zone is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });
            }

            var field = await _context.Fields.FirstOrDefaultAsync(x => x.Id == body.FieldId);

            var zoneHistory = new ZoneHistory
            {
                CodZone = body.CodZone is not null ? body.CodZone : zone.CodZone,
                CodZoneOld = zone.CodZone,

                Field = field is not null ? field : zone.Field,
                FieldOldId = zone.Field.Id,

                Description = body.Description is not null ? body.Description : zone.Description,
                DescriptionOld = zone.Description,
                FieldName = field is not null ? field.Name : zone.Field?.Name,
                FieldNameOld = zone.Field?.Name,

                User = user,
                Zone = zone,

                TypeOperation = TypeOperation.Update
            };

            await _context.ZoneHistories.AddAsync(zoneHistory);

            if (body.FieldId is not null && field is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });
            }

            zone.Description = body.Description is not null ? body.Description : zone.Description;
            zone.CodZone = body.CodZone is not null ? body.CodZone : zone.CodZone;
            zone.Field = field is not null ? field : zone.Field;

            _context.Zones.Update(zone);
            await _context.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);

            return Ok(zoneDTO);

        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var zone = await _context.Zones.Include(x => x.Field).FirstOrDefaultAsync(x => x.Id == id);
            if (zone is null || !zone.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found or inactive already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var zoneHistory = new ZoneHistory
            {
                CodZone = zone.CodZone,
                CodZoneOld = zone.CodZone,

                Field = zone.Field,
                FieldOldId = zone.Field?.Id,

                Description = zone.Description,
                DescriptionOld = zone.Description,

                FieldName = zone.Field?.Name,
                FieldNameOld = zone.Field?.Name,

                User = user,
                Zone = zone,

                IsActive = false,
                IsActiveOld = zone.IsActive,

                TypeOperation = TypeOperation.Delete
            };

            await _context.ZoneHistories.AddAsync(zoneHistory);

            zone.IsActive = false;
            zone.DeletedAt = DateTime.UtcNow;

            _context.Update(zone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var zone = await _context.Zones.Include(x => x.Field).FirstOrDefaultAsync(x => x.Id == id);

            if (zone is null || zone.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found or is active already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var zoneHistory = new ZoneHistory
            {
                CodZone = zone.CodZone,
                CodZoneOld = zone.CodZone,

                Field = zone.Field,
                FieldOldId = zone.Field?.Id,

                Description = zone.Description,
                DescriptionOld = zone.Description,

                FieldName = zone.Field?.Name,
                FieldNameOld = zone.Field?.Name,

                IsActive = true,
                IsActiveOld = zone.IsActive,

                User = user,
                Zone = zone,

                TypeOperation = TypeOperation.Restore
            };

            await _context.ZoneHistories.AddAsync(zoneHistory);

            zone.IsActive = true;
            zone.DeletedAt = null;

            _context.Zones.Update(zone);
            await _context.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);
            return Ok(zoneDTO);
        }
    }
}
