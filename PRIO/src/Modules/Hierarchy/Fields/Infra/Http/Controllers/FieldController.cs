﻿using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Fields.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Controllers
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
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            var fieldsDTO = await _fieldService.GetFields(user);
            return Ok(fieldsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, [FromQuery] string? wellFilter)
        {
            var fieldDTO = await _fieldService.GetFieldById(id, wellFilter);
            return Ok(fieldDTO);
        }

        [HttpPatch("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromHeader] string StatusDate)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _fieldService.DeleteField(id, user, StatusDate);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
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

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var fieldHistories = await _fieldService.GetFieldHistory(id);
            return Ok(fieldHistories);
        }
    }
}
