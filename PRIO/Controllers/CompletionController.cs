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

            var completion = await _context.Completions.FirstOrDefaultAsync(x => x.Name == body.Name);
            if (completion is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Completion with name: {body.Name} already exists."
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

            completion = new Completion
            {
                Name = body.Name,
                CodCompletion = $"{well.Name}_{reservoir.Zone.CodZone}",
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
                .ThenInclude(r => r.Zone)
                .ThenInclude(z => z.Field)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Completion not found"
                });

            string codeCompletionUpdated = completion.CodCompletion;

            if (body.WellId is not null)
            {
                var well = await _context.Wells.Include(x => x.Field).FirstOrDefaultAsync(x => x.Id == body.WellId);
                if (well is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Well not found"
                    });

                if (well.Field.Id != completion.Well.Field.Id)
                    return Conflict(new ErrorResponseDTO
                    {
                        Message = $"Well: {well.Name} and Completion: {completion.Name} doesn't belong to the same Field"
                    });

                codeCompletionUpdated += $"{well.Name}_{completion.Reservoir.Zone.CodZone}";
            }

            if (body.ReservoirId is not null)
            {
                var reservoir = await _context.Reservoirs
                    .Include(x => x.Zone)
                    .ThenInclude(z => z.Field)
                    .FirstOrDefaultAsync(x => x.Id == body.ReservoirId);

                if (reservoir is null)
                {
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Reservoir not found"
                    });
                }

                if (reservoir.Zone.Field.Id != completion.Well.Field.Id)
                    return Conflict(new ErrorResponseDTO
                    {
                        Message = $"Reservoir: {reservoir.Name} and Completion: {completion.Name} doesn't belong to the same Field"
                    });

                //codeCompletionUpdated.Replace();
                //parei aqui
            }


            completion.Description = body.Description is not null ? body.Description : completion.Description;
            completion.Name = body.Name is not null ? body.Name : completion.Name;

            _context.Update(completion);
            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);

            return Ok(completionDTO);
        }
    }
}
