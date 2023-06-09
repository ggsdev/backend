using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.CompletionDTOS;
using PRIO.Models.Completions;
using PRIO.Utils;
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

            //verificar se manualmente uma completação pode não ter um reservatorio associado igual ao XLS
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

            var completionHistory = new CompletionHistory
            {
                Name = $"{well.Name}_{reservoir.Zone.CodZone}",
                NameOld = null,
                CodCompletion = body.CodCompletion,
                CodCompletionOld = null,
                Reservoir = reservoir,
                ReservoirOld = null,
                Well = well,
                WellOld = null,
                User = user,
                Description = body.Description,
                DescriptionOld = null,
                IsActive = true,
                IsActiveOld = null,
                TypeOperation = TypeOperation.Create
            };

            await _context.CompletionHistories.AddAsync(completionHistory);
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
                .ThenInclude(x => x.Zone)
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

            var completionHistory = new CompletionHistory
            {
                User = user,
                NameOld = completion.Name,
                Name = completion.Name,
                TypeOperation = TypeOperation.Update,
                IsActive = completion.IsActive,
                IsActiveOld = completion.IsActive,
                Description = body.Description is not null ? body.Description : completion.Description,
                DescriptionOld = completion.Description,
                Completion = completion,
                CodCompletion = body.CodCompletion is not null ? body.CodCompletion : completion.CodCompletion,
                CodCompletionOld = completion.CodCompletion,
                ReservoirOld = completion.Reservoir?.Id,
                WellOld = completion.Well?.Id,
                Well = completion.Well,
                Reservoir = completion.Reservoir
            };

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
                completion.Well = well;
                completionHistory.Name = $"{well.Name}_{completion.Reservoir.Zone.CodZone}";
                completionHistory.Well = well;
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

                completion.Name = $"{completion.Well?.Name}_{reservoir.Zone?.CodZone}";
                completion.Reservoir = reservoir;
                completionHistory.Name = $"{completion.Well?.Name}_{reservoir.Zone?.CodZone}";
                completionHistory.Reservoir = reservoir;
            }


            completion.Description = body.Description is not null ? body.Description : completion.Description;
            completion.CodCompletion = body.CodCompletion is not null ? body.CodCompletion : completion.CodCompletion;

            await _context.CompletionHistories.AddAsync(completionHistory);
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

            var completion = await _context.Completions.Include(x => x.Well).Include(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null || !completion.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Completion not found or inactive already"
                });

            var completionHistory = new CompletionHistory
            {
                NameOld = completion.Name,
                Name = completion.Name,
                IsActive = false,
                IsActiveOld = completion.IsActive,
                Description = completion.Description,
                DescriptionOld = completion.Description,
                Completion = completion,
                CodCompletion = completion.CodCompletion,
                CodCompletionOld = completion.CodCompletion,
                WellOld = completion.Well?.Id,
                Well = completion.Well,
                Reservoir = completion.Reservoir,
                ReservoirOld = completion.Reservoir?.Id,
                User = user,
                TypeOperation = TypeOperation.Delete,

            };

            completion.IsActive = false;
            completion.DeletedAt = DateTime.UtcNow;

            await _context.CompletionHistories.AddAsync(completionHistory);
            _context.Completions.Update(completion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPatch("completions/{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var completion = await _context.Completions.Include(x => x.Well).Include(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null || completion.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Completion not found or inactive already"
                });

            var completionHistory = new CompletionHistory
            {
                NameOld = completion.Name,
                Name = completion.Name,
                IsActive = false,
                IsActiveOld = completion.IsActive,
                Description = completion.Description,
                DescriptionOld = completion.Description,
                Completion = completion,
                CodCompletion = completion.CodCompletion,
                CodCompletionOld = completion.CodCompletion,
                WellOld = completion.Well?.Id,
                Well = completion.Well,
                Reservoir = completion.Reservoir,
                ReservoirOld = completion.Reservoir?.Id,
                User = user,
                TypeOperation = TypeOperation.Restore,

            };

            completion.IsActive = true;
            completion.DeletedAt = null;

            await _context.CompletionHistories.AddAsync(completionHistory);
            _context.Completions.Update(completion);
            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return Ok(completionDTO);
        }

        [HttpGet("completions/{id}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var completionHistories = await _context.CompletionHistories.Include(x => x.User)
                                                      .Include(x => x.Reservoir)
                                                      .Include(x => x.Well)
                                                      .Where(x => x.Completion.Id == id)
                                                      .OrderByDescending(x => x.CreatedAt)
                                                      .ToListAsync();

            if (completionHistories is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found"
                });

            var wellHistoryDTO = _mapper.Map<List<CompletionHistory>, List<CompletionHistoryDTO>>(completionHistories);

            return Ok(wellHistoryDTO);
        }
    }
}
