using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels.Completions;

namespace PRIO.Controllers
{
    [ApiController]
    public class CompletionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CompletionController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("completions")]
        public async Task<IActionResult> Create([FromBody] CreateCompletionViewModel body)
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var well = await _context.Wells.Include(x => x.Field).FirstOrDefaultAsync(x => x.Id == body.WellId);
            if (well is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Well with id: {body.WellId} not found"
                });

            var reservoir = await _context.Reservoirs.Include(x => x.Zone).ThenInclude(z => z.Field).FirstOrDefaultAsync(x => x.Id == body.ReservoirId);
            if (reservoir is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Reservoir with id: {body.ReservoirId} not found"
                });

            if (reservoir.Zone.Field.Id != well.Field.Id)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field"
                });

            var completion = await _context.Completions.FirstOrDefaultAsync(x => x.Name == $"{well.Name}_{reservoir.Zone.CodZone}");
            if (completion is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Completion with name: {well.Name}_{reservoir.Zone.CodZone} already exists."
                });

            completion = new Completion
            {
                Name = $"{well.Name}_{reservoir.Zone.CodZone}",
                CodCompletion = body.CodCompletion,
                Description = body.Description,
                User = user,
                Well = well,
                Reservoir = reservoir,
            };

            await _context.Completions.AddAsync(completion);
            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return Created($"completions/{completion.Id}", completionDTO);
        }

        [HttpGet("completions")]
        public async Task<IActionResult> Get()
        {
            var completions = await _context.Completions.Include(x => x.Well).Include(x => x.Reservoir).Include(x => x.User).ToListAsync();

            var completionsDTO = _mapper.Map<List<Completion>, List<CompletionDTO>>(completions);

            return Ok(completionsDTO);
        }

        [HttpGet("completions/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var completion = await _context.Completions.Include(x => x.Well).Include(x => x.Reservoir).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (completion is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Completion not found"
                });

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return Ok(completionDTO);
        }


        [HttpPatch("completions/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCompletionViewModel body)
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var completion = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Completion not found"
                });

            var well = await _context.Wells.Include(x => x.Field).FirstOrDefaultAsync(x => x.Id == body.WellId);
            var reservoir = await _context.Reservoirs
                    .Include(x => x.Zone)
                    .ThenInclude(z => z.Field)
                    .FirstOrDefaultAsync(z => z.Id == body.ReservoirId);

            if (body.WellId is not null)
            {
                if (well is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Well not found"
                    });

                if (reservoir is null && body.ReservoirId is not null)
                {
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Reservoir not found"
                    });
                }

                if (reservoir is not null && well.Field.Id != reservoir.Zone.Field.Id)
                    return Conflict(new ErrorResponseDTO
                    {
                        Message = $"Well: {well.Name} and Reservoir: {reservoir.Name} doesn't belong to the same Field"
                    });

                completion.Name = $"{well.Name}_{completion.Reservoir.Zone.CodZone}";
            }

            if (body.ReservoirId is not null)
            {
                if (reservoir is null)
                {
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Reservoir not found"
                    });
                }

                if (well is null && body.WellId is not null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Well not found"
                    });

                if (well is not null && reservoir.Zone.Field.Id != well.Field.Id)
                    return Conflict(new ErrorResponseDTO
                    {
                        Message = $"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field"
                    });

                var findUnderline = completion.Name.IndexOf("_");
                completion.Name = completion.Name.Replace(completion.Name[findUnderline..], reservoir.Zone.CodZone);
            }

            completion.Description = body.Description is not null ? body.Description : completion.Description;
            completion.CodCompletion = body.CodCompletion is not null ? body.CodCompletion : completion.CodCompletion;

            _context.Completions.Update(completion);
            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);

            return Ok(completionDTO);
        }

        [HttpDelete("completions/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var completion = await _context.Completions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Completion not found"
                });

            completion.IsActive = false;
            completion.DeletedAt = DateTime.UtcNow;

            _context.Completions.Update(completion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
