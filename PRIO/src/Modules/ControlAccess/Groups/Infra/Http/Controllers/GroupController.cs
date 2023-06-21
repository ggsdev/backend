using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Groups.Infra.Http.Services;
using PRIO.src.Modules.ControlAccess.Groups.ViewModels;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.Http.Controllers
{
    [ApiController]
    [Route("groups")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _service;

        public GroupController(GroupService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupViewModel body)
        {
            var returnGroupDTO = await _service.CreateGroup(body);

            return Created($"groups/{returnGroupDTO.Id}", returnGroupDTO);
        }

        [HttpPost("{groupId}/user/{userId}")]
        public async Task<IActionResult> InsertUser([FromRoute] Guid groupId, [FromRoute] Guid userId)
        {
            var userLoggedIn = HttpContext.Items["User"] as User;

            var userDTO = await _service.InsertUserInGroup(groupId, userId, userLoggedIn);
            return Ok(userDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var groupsDTO = await _service.GetGroups();
            return Ok(groupsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var groupDTO = await _service.GetGroupById(id);
            return Ok(groupDTO);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateGroupViewModel body)
        {
            var groupDTO = await _service.UpdateGroup(id, body);
            return Ok(groupDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _service.DeleteGroup(id);
            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            await _service.RestoreGroup(id);
            return NoContent();
        }
    }
}