using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Comments.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.Comments.Infra.Http.Controllers
{
    [ApiController]
    [Route("comments-production")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _service;

        public CommentController(CommentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommentViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var result = await _service.CreateComment(body, user);

            return Created($"comments-production/{result.Id}", result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] UpdateCommentViewModel body, [FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var result = await _service.UpdateComment(body, id, user);

            return Ok(result);
        }
    }
}
