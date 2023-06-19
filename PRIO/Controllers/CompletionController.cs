using Microsoft.AspNetCore.Mvc;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services.HierarchyServices;
using PRIO.ViewModels.Completions;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("completions")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class CompletionController : ControllerBase
    {
        private readonly CompletionService _completionService;

        public CompletionController(CompletionService service)

        {
            _completionService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompletionViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var completionDTO = await _completionService.CreateCompletion(body, user);
            return Created($"completions/{completionDTO.Id}", completionDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var completionsDTO = await _completionService.GetCompletions();
            return Ok(completionsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var completionDTO = await _completionService.GetCompletionById(id);
            return Ok(completionDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCompletionViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var completionDTO = await _completionService.UpdateCompletion(body, id, user);
            return Ok(completionDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            await _completionService.DeleteCompletion(id, user);

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var completionDTO = await _completionService.RestoreCompletion(id, user);
            return Ok(completionDTO);
        }

        //[HttpGet("{id:Guid}/history")]
        //public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        //{
        //    var completionHistories = await _context.CompletionHistories.Include(x => x.User)
        //                                              .Include(x => x.Reservoir)
        //                                              .Include(x => x.Well)
        //                                              .Where(x => x.Completion.Id == id)
        //                                              .OrderByDescending(x => x.CreatedAt)
        //                                              .ToListAsync();

        //    if (completionHistories is null)
        //        return NotFound(new ErrorResponseDTO
        //        {
        //            Message = "Well not found"
        //        });

        //    var wellHistoryDTO = _mapper.Map<List<CompletionHistory>, List<CompletionHistoryDTO>>(completionHistories);

        //    return Ok(wellHistoryDTO);
        //}
    }
}
