using Microsoft.AspNetCore.Mvc;
using PRIO.DTOS.GlobalDTOS;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services.HierarchyServices;
using PRIO.ViewModels.MeasuringEquipment;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("equipments")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentService _equipmentService;
        public EquipmentController(EquipmentService service)
        {
            _equipmentService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEquipmentViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var equipmentDTO = await _equipmentService.CreateEquipment(body, user);
            return Created($"equipments/{equipmentDTO.Id}", equipmentDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var equipmentsDTO = await _equipmentService.GetEquipments();
            return Ok(equipmentsDTO);

        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var equipmentDTO = await _equipmentService.GetEquipmentById(id);
            return Ok(equipmentDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEquipmentViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var equipmentDTO = await _equipmentService.UpdateEquipment(body, id, user);
            return Ok(equipmentDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _equipmentService.DeleteEquipment(id, user);

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

            var equipmentDTO = await _equipmentService.RestoreEquipment(id, user);
            return Ok(equipmentDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var equipmentHistories = await _equipmentService.GetEquipmentHistory(id);

            return Ok(equipmentHistories);
        }
    }
}
