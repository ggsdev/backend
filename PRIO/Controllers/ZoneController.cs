using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels.Zones;

namespace PRIO.Controllers
{
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ZoneController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpPost("zones")]
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
                return NotFound("Field not found");

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            var zone = new Zone
            {
                CodZone = body.CodZone,
                Field = field,
                Description = body.Description,
                User = user,
            };

            await _context.AddAsync(zone);
            await _context.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);

            return Created($"zones/{zone.Id}", zoneDTO);
        }

        [HttpGet("zones")]
        public async Task<IActionResult> Get()
        {
            var zones = await _context.Zones.Include(x => x.Reservoirs).Include(x => x.User).ToListAsync();
            var zonesDTO = _mapper.Map<List<Zone>, List<ZoneDTO>>(zones);

            return Ok(zonesDTO);
        }

        [HttpGet("zones/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var zone = await _context.Zones.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (zone is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });
            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);

            return Ok(zoneDTO);

        }

        [HttpPatch("zones/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateZoneViewModel body)
        {
            var zone = await _context.Zones.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (zone is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });

            if (body.FieldId is not null)
            {
                var fieldInDatabase = await _context.Fields.FirstOrDefaultAsync(x => x.Id == body.FieldId);

                if (fieldInDatabase is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Field not found"
                    });

                zone.Field = fieldInDatabase is not null ? fieldInDatabase : zone.Field;

            }

            zone.Description = body.Description is not null ? body.Description : zone.Description;
            zone.CodZone = body.CodZone is not null ? body.CodZone : zone.CodZone;


            _context.Update(zone);
            await _context.SaveChangesAsync();

            var zoneDTO = _mapper.Map<Zone, ZoneDTO>(zone);

            return Ok(zoneDTO);

        }

        [HttpDelete("zones/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var zone = await _context.Zones.FirstOrDefaultAsync(x => x.Id == id);
            if (zone is null || !zone.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found or inactive already"
                });

            zone.IsActive = false;
            zone.DeletedAt = DateTime.UtcNow;

            _context.Update(zone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
