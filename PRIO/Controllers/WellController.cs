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
    public class WellController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WellController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("wells")]
        public async Task<IActionResult> Create([FromBody] CreateWellViewModel body)
        {
            var wellInDatabase = await _context.Wells.FirstOrDefaultAsync(x => x.CodWell == body.CodWell);
            if (wellInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Well with code: {body.CodWell} already exists, try another code."
                });

            var FieldFound = await _context.Fields.FirstOrDefaultAsync(x => x.Id == body.FieldId);
            if (FieldFound is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Field is not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var well = new Well
            {
                CodWell = body.CodWell,
                Name = body.Name,
                WellOperatorName = body.WellOperatorName,
                CodWellAnp = body.CodWellAnp,
                CategoryAnp = body.CategoryAnp,
                CategoryReclassificationAnp = body.CategoryReclassificationAnp,
                CategoryOperator = body.CategoryOperator,
                StatusOperator = body.StatusOperator,
                Type = body.Type,
                WaterDepth = body.WaterDepth,
                TopOfPerforated = body.TopOfPerforated,
                BaseOfPerforated = body.BaseOfPerforated,
                ArtificialLift = body.ArtificialLift,
                Latitude4C = body.Latitude4C,
                Longitude4C = body.Longitude4C,
                LongitudeDD = body.LongitudeDD,
                LatitudeDD = body.LatitudeDD,
                DatumHorizontal = body.DatumHorizontal,
                TypeBaseCoordinate = body.TypeBaseCoordinate,
                CoordX = body.CoordX,
                CoordY = body.CoordY,
                Description = body.Description,
                Field = FieldFound,
                User = user,
            };

            await _context.AddAsync(well);
            await _context.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, WellDTO>(well);

            return Created($"reservoirs/{well.Id}", wellDTO);
        }

        [HttpGet("wells")]
        public async Task<IActionResult> Get()
        {
            var wells = await _context.Wells.Include(x => x.Completions).Include(x => x.User).ToListAsync();
            var wellsDTO = _mapper.Map<List<Well>, List<WellDTO>>(wells);
            return Ok(wellsDTO);
        }

        [HttpGet("wells/{wellId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid wellId)
        {
            Console.WriteLine("oi");
            var well = await _context.Wells.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == wellId);
            if (well is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found"
                });
            var wellDTO = _mapper.Map<Well, WellDTO>(well);

            return Ok(wellDTO);
        }

        [HttpPatch("wells/{wellId}")]
        public async Task<IActionResult> Update([FromRoute] Guid wellId, [FromBody] UpdateWellViewModel body)
        {
            var well = await _context.Wells.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == wellId);
            if (well is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found"
                });

            if (body.FieldId is not null)
            {
                var fieldInDatabase = await _context.Fields.FirstOrDefaultAsync(x => x.Id == body.FieldId);

                if (fieldInDatabase is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Field not found"
                    });

                well.Field = fieldInDatabase is not null ? fieldInDatabase : well.Field;

            }

            well.Name = body.Name is not null ? body.Name : well.Name;
            well.Description = body.Description is not null ? body.Description : well.Description;
            well.CodWell = body.CodWell is not null ? body.CodWell : well.CodWell;

            _context.Update(well);
            await _context.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, WellDTO>(well);
            return Ok(wellDTO);
        }

        [HttpDelete("wells/{wellsId}")]
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
