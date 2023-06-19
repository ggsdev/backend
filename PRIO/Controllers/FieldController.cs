using Microsoft.AspNetCore.Mvc;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services.HierarchyServices;
using PRIO.ViewModels.HierarchyViewModels.Fields;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("fields")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class FieldController : ControllerBase
    {
        private readonly FieldService _fieldService;
        public FieldController(FieldService service)
        {
            _fieldService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFieldViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var fieldDTO = await _fieldService.CreateField(body, user);

            return Created($"fields/{fieldDTO.Id}", fieldDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fieldsDTO = await _fieldService.GetFields();
            return Ok(fieldsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var fieldDTO = await _fieldService.GetFieldById(id);
            return Ok(fieldDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFieldViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var fieldDTO = await _fieldService.UpdateField(id, body, user);
            return Ok(fieldDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _fieldService.DeleteField(id, user);

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

            var fieldDTO = await _fieldService.RestoreField(id, user);
            return Ok(fieldDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var fieldHistories = await _fieldService.GetFieldHistory(id);
            return Ok(fieldHistories);
        }
    }
}
