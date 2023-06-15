using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.WellDTOS;
using PRIO.Filters;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Wells;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("wells")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class WellController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WellController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWellViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var wellInDatabase = await _context.Wells.FirstOrDefaultAsync(x => x.CodWell == body.CodWell);
            if (wellInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Well with code: {body.CodWell} already exists, try another code."
                });

            var fieldFound = await _context.Fields.FirstOrDefaultAsync(x => x.Id == body.FieldId);
            if (fieldFound is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Field is not found"
                });

            var well = new Well
            {
                CodWell = body.CodWell is not null ? body.CodWell : GenerateCode.Generate(body.Name),
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
                Field = fieldFound,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _context.Wells.AddAsync(well);

            await _context.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, WellDTO>(well);

            return Created($"wells/{well.Id}", wellDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var wells = await _context.Wells.Include(x => x.Completions).Include(x => x.User).ToListAsync();
            var wellsDTO = _mapper.Map<List<Well>, List<WellDTO>>(wells);
            return Ok(wellsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var well = await _context.Wells.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (well is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found"
                });
            var wellDTO = _mapper.Map<Well, WellDTO>(well);

            return Ok(wellDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWellViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var well = await _context.Wells.Include(x => x.Field).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
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
            else
            {
                well.Field = well.Field;
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

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var well = await _context.Wells.FirstOrDefaultAsync(x => x.Id == id);
            if (well is null || !well.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found or inactive already"
                });

            well.IsActive = false;
            well.DeletedAt = DateTime.UtcNow;

            Console.WriteLine(well.WaterDepth);
            _context.Update(well);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var well = await _context.Wells.Include(x => x.Field).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (well is null || well.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found or active already"
                });


            well.IsActive = true;
            well.DeletedAt = null;

            _context.Update(well);
            await _context.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, WellDTO>(well);
            return Ok(wellDTO);
        }

        //[HttpGet("{id:Guid}/history")]
        //public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        //{
        //    var wellHistories = await _context.WellHistories.Include(x => x.User)
        //                                              .Include(x => x.Field)
        //                                              .Include(x => x.Well)
        //                                              .Where(x => x.Well.Id == id)
        //                                              .OrderByDescending(x => x.CreatedAt)
        //                                              .ToListAsync();

        //    if (wellHistories is null)
        //        return NotFound(new ErrorResponseDTO
        //        {
        //            Message = "Well not found"
        //        });


        //    var wellHistoryDTO = _mapper.Map<List<WellHistory>, List<WellHistoryDTO>>(wellHistories);

        //    return Ok(wellHistoryDTO);
        //}
    }
}
