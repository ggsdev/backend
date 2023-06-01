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
                Field = FieldFound
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
            well.CodWell = body.CodWell is not null ? body.CodWell : well.CodWell;
            well.WellOperatorName = body.WellOperatorName is not null ? body.WellOperatorName : well.WellOperatorName;
            well.CodWellAnp = body.CodWellAnp is not null ? body.CodWellAnp : well.CodWellAnp;
            well.CategoryAnp = body.CategoryAnp is not null ? body.CategoryAnp : well.CategoryAnp;
            well.CategoryReclassificationAnp = body.CategoryReclassificationAnp is not null ? body.CategoryReclassificationAnp : well.CategoryReclassificationAnp;
            well.CategoryOperator = body.CategoryOperator is not null ? body.CategoryOperator : well.CategoryOperator;
            well.StatusOperator = body.StatusOperator is not null ? body.StatusOperator : well.StatusOperator;
            well.Type = body.Type is not null ? body.Type : well.Type;
            well.WaterDepth = body.WaterDepth is not null ? body.WaterDepth : well.WaterDepth;
            well.TopOfPerforated = body.TopOfPerforated is not null ? body.TopOfPerforated : well.TopOfPerforated;
            well.BaseOfPerforated = body.BaseOfPerforated is not null ? body.BaseOfPerforated : well.BaseOfPerforated;
            well.ArtificialLift = body.ArtificialLift is not null ? body.ArtificialLift : well.ArtificialLift;
            well.Latitude4C = body.Latitude4C is not null ? body.Latitude4C : well.Latitude4C;
            well.Longitude4C = body.Longitude4C is not null ? body.Longitude4C : well.Longitude4C;
            well.LatitudeDD = body.LatitudeDD is not null ? body.LatitudeDD : well.LatitudeDD;
            well.LongitudeDD = body.LongitudeDD is not null ? body.LongitudeDD : well.LongitudeDD;
            well.DatumHorizontal = body.DatumHorizontal is not null ? body.DatumHorizontal : well.DatumHorizontal;
            well.TypeBaseCoordinate = body.TypeBaseCoordinate is not null ? body.TypeBaseCoordinate : well.TypeBaseCoordinate;
            well.CoordX = body.CoordX is not null ? body.CoordX : well.CoordX;
            well.CoordY = body.CoordY is not null ? body.CoordY : well.CoordY;
            well.Description = body.Description is not null ? body.Description : well.Description;

            _context.Update(well);
            await _context.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, WellDTO>(well);
            return Ok(wellDTO);
        }

        [HttpDelete("wells/{wellId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid wellId)
        {
            var well = await _context.Wells.FirstOrDefaultAsync(x => x.Id == wellId);
            if (well is null || !well.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found or inactive already"
                });

            well.IsActive = false;
            well.DeletedAt = DateTime.UtcNow;

            _context.Update(well);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
