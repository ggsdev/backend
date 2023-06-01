using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels.Zones;
using System.Security.Policy;

namespace PRIO.Controllers
{
    [ApiController]
    public class ReservoirController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReservoirController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpPost("reservoirs")]
        public async Task<IActionResult> Create([FromBody] CreateReservoirViewModel body)
        {
            var reservoirInDatabase = await _context.Reservoirs.FirstOrDefaultAsync(x => x.CodReservoir == body.CodReservoir);
            if (reservoirInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Reservoir with code: {body.CodReservoir} already exists, try another code."
                });

            var ZoneFound = await _context.Zones.FirstOrDefaultAsync(x => x.Id == body.ZoneId);
            if (ZoneFound is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Zone is not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var reservoir = new Reservoir
            {
                Name = body.Name,
                Description = body.Description is not null ? body.Description : null,
                CodReservoir = body.CodReservoir,
                Zone = ZoneFound,
                User = user
            };

            await _context.AddAsync(reservoir);
            await _context.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return Created($"reservoirs/{reservoir.Id}", reservoirDTO);
        }

        [HttpGet("reservoirs")]
        public async Task<IActionResult> Get()
        {
            var reservoirs = await _context.Reservoirs.Include(x => x.Completions).Include(x => x.User).ToListAsync();
            var reservoirsDTO = _mapper.Map<List<Reservoir>, List<ReservoirDTO>>(reservoirs);
            return Ok(reservoirsDTO);
        }

        [HttpGet("reservoirs/{reservoirId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid reservoirId)
        {
            Console.WriteLine("oi");
            var reservoir = await _context.Reservoirs.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == reservoirId);
            Console.WriteLine(reservoir);
            if (reservoir is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found"
                });
            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return Ok(reservoirDTO);
        }

        [HttpPatch("reservoirs/{reservoirsId}")]
        public async Task<IActionResult> Update([FromRoute] Guid reservoirsId, [FromBody] UpdateReservoirViewModel body)
        {
            var reservoir = await _context.Reservoirs.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == reservoirsId);
            if (reservoir is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found"
                });

            if (body.ZoneId is not null)
            {
                var zoneInDatabase = await _context.Zones.FirstOrDefaultAsync(x => x.Id == body.ZoneId);

                if (zoneInDatabase is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Zone not found"
                    });

                reservoir.Zone = zoneInDatabase is not null ? zoneInDatabase : reservoir.Zone;

            }

            reservoir.Name = body.Name is not null ? body.Name : reservoir.Name;
            reservoir.Description = body.Description is not null ? body.Description : reservoir.Description;
            reservoir.CodReservoir = body.CodReservoir is not null ? body.CodReservoir : reservoir.CodReservoir;

            _context.Update(reservoir);
            await _context.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);
            return Ok(reservoirDTO);
        }

        [HttpDelete("reservoirs/{reservoirId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid reservoirId)
        {
            var reservoir = await _context.Reservoirs.FirstOrDefaultAsync(x => x.Id == reservoirId);
            if (reservoir is null || !reservoir.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found or inactive already"
                });

            reservoir.IsActive = false;
            reservoir.DeletedAt = DateTime.UtcNow;

            _context.Update(reservoir);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
