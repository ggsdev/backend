using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Comments.ViewModels;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.CachePolicies;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.Comments.Infra.Http.Controllers
{
    [ApiController]
    [Route("comments-production")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _service;
        private readonly IOutputCacheStore _cache;

        public CommentController(CommentService service, IOutputCacheStore cache)
        {
            _service = service;
            _cache = cache;
        }

        [OutputCache(PolicyName = nameof(AuthProductionCachePolicy))]
        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromBody] CreateCommentViewModel body, [FromRoute] Guid id, CancellationToken ct)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var result = await _service.CreateComment(body, user, id);

            await _cache.EvictByTagAsync("ProductionTag", ct);

            return Created($"comments-production/{result.Id}", result);
        }

        [OutputCache(PolicyName = nameof(AuthProductionCachePolicy))]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] UpdateCommentViewModel body, [FromRoute] Guid id, CancellationToken ct)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var result = await _service.UpdateComment(body, id, user);

            await _cache.EvictByTagAsync("ProductionTag", ct);

            return Ok(result);
        }
    }
}
