using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.ReservoirDTOS;
using PRIO.Models.Reservoirs;
using PRIO.Utils;
using PRIO.ViewModels.Zones;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("reservoirs")]
    public class ReservoirController : BaseApiController
    {
        public ReservoirController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservoirViewModel body)
        {
            var reservoirInDatabase = await _context.Reservoirs.FirstOrDefaultAsync(x => x.CodReservoir == body.CodReservoir);
            if (reservoirInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Reservoir with code: {body.CodReservoir} already exists, try another code."
                });

            var zoneInDatabase = await _context.Zones.FirstOrDefaultAsync(x => x.Id == body.ZoneId);
            if (zoneInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Zone not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var reservoir = new Reservoir
            {
                Name = body.Name,
                Description = body.Description,
                CodReservoir = body.CodReservoir,

                Zone = zoneInDatabase,
                User = user,
            };

            await _context.Reservoirs.AddAsync(reservoir);

            var reservoirHistory = new ReservoirHistory
            {
                CodReservoir = reservoir.CodReservoir,
                Name = reservoir.Name,
                Description = reservoir.Description,

                User = user,
                Reservoir = reservoir,

                Zone = zoneInDatabase,

                TypeOperation = TypeOperation.Create,
            };
            await _context.ReservoirHistories.AddAsync(reservoirHistory);
            await _context.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return Created($"reservoirs/{reservoir.Id}", reservoirDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reservoirs = await _context.Reservoirs.Include(x => x.Completions).Include(x => x.User).ToListAsync();
            var reservoirsDTO = _mapper.Map<List<Reservoir>, List<ReservoirDTO>>(reservoirs);
            return Ok(reservoirsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var reservoir = await _context.Reservoirs.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (reservoir is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found"
                });
            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return Ok(reservoirDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetBHistory([FromRoute] Guid id)
        {
            var reservoirHistories = await _context.ReservoirHistories
                .Include(x => x.User)
                .Include(x => x.Zone)
                .Include(x => x.Reservoir)
                .Where(x => x.Reservoir.Id == id)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            if (reservoirHistories is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found"
                });

            var reservoirHistoriesDTO = _mapper.Map<List<ReservoirHistory>, List<ReservoirHistoryDTO>>(reservoirHistories);
            return Ok(reservoirHistoriesDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateReservoirViewModel body)
        {
            var reservoir = await _context.Reservoirs.Include(x => x.User).Include(x => x.Zone).FirstOrDefaultAsync(x => x.Id == id);
            if (reservoir is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var zoneInDatabase = await _context.Zones.FirstOrDefaultAsync(x => x.Id == body.ZoneId);

            var reservoirHistory = new ReservoirHistory
            {
                Name = reservoir.Name,
                NameOld = reservoir.Name,

                CodReservoir = body.CodReservoir is not null ? body.CodReservoir : reservoir.CodReservoir,
                CodReservoirOld = reservoir.CodReservoir,

                Description = body.Description is not null ? body.Description : reservoir.Description,
                DescriptionOld = reservoir.Description,

                Zone = zoneInDatabase is not null ? zoneInDatabase : reservoir.Zone,
                ZoneOldId = reservoir.Zone?.Id,

                User = user,

                Reservoir = reservoir,

                TypeOperation = TypeOperation.Update
            };

            await _context.ReservoirHistories.AddAsync(reservoirHistory);

            if (body.ZoneId is not null && zoneInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });

            reservoir.Name = body.Name is not null ? body.Name : reservoir.Name;
            reservoir.Description = body.Description is not null ? body.Description : reservoir.Description;
            reservoir.CodReservoir = body.CodReservoir is not null ? body.CodReservoir : reservoir.CodReservoir;
            reservoir.Zone = zoneInDatabase is not null ? zoneInDatabase : reservoir.Zone;

            _context.Reservoirs.Update(reservoir);
            await _context.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);
            return Ok(reservoirDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var reservoir = await _context.Reservoirs.Include(x => x.Zone).FirstOrDefaultAsync(x => x.Id == id);
            if (reservoir is null || !reservoir.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found or inactive already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var reservoirHistory = new ReservoirHistory
            {
                Name = reservoir.Name,
                NameOld = reservoir.Name,

                CodReservoir = reservoir.Name,
                CodReservoirOld = reservoir.Name,

                Description = reservoir.Description,
                DescriptionOld = reservoir.Description,

                IsActive = false,
                IsActiveOld = reservoir.IsActive,

                Zone = reservoir.Zone,
                ZoneOldId = reservoir.Zone?.Id,

                User = user,

                Reservoir = reservoir,

                TypeOperation = TypeOperation.Delete
            };

            await _context.ReservoirHistories.AddAsync(reservoirHistory);

            reservoir.IsActive = false;
            reservoir.DeletedAt = DateTime.UtcNow;

            _context.Reservoirs.Update(reservoir);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var reservoir = await _context.Reservoirs.Include(x=> x.Zone).FirstOrDefaultAsync(x => x.Id == id);
            if (reservoir is null || reservoir.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found or active already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var reservoirHistory = new ReservoirHistory
            {
                Name = reservoir.Name,
                NameOld = reservoir.Name,

                CodReservoir = reservoir.Name,
                CodReservoirOld = reservoir.Name,

                Description = reservoir.Description,
                DescriptionOld = reservoir.Description,

                IsActive = true,
                IsActiveOld = reservoir.IsActive,

                Zone = reservoir.Zone,
                ZoneOldId = reservoir.Zone?.Id,

                User = user,

                Reservoir = reservoir,
                TypeOperation = TypeOperation.Restore
            };

            await _context.ReservoirHistories.AddAsync(reservoirHistory);

            reservoir.IsActive = true;
            reservoir.DeletedAt = null;

            _context.Reservoirs.Update(reservoir);
            await _context.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return Ok(reservoirDTO);
        }
    }
}
