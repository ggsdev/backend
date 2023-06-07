using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.MeasuringEquipment;
using PRIO.Models.MeasuringEquipments;
using PRIO.ViewModels.MeasuringEquipment;

namespace PRIO.Controllers
{
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EquipmentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpPost("equipments")]
        public async Task<IActionResult> Create([FromBody] CreateEquipmentViewModel body)
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });
            var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.Id == body.InstallationId);
            Console.WriteLine(body.InstallationId);
            Console.WriteLine(installationInDatabase);
            if (installationInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Installation not found"
                });

            var equipment = new MeasuringEquipment
            {
                TagEquipment = body.TagEquipment,
                TagMeasuringPoint = body.TagMeasuringPoint,
                SerieNumber = body.SerieNumber,
                Type = body.Type,
                TypeEquipment = body.TypeEquipment,
                Model = body.Model,
                HasSeal = body.HasSeal,
                MVS = body.MVS,
                CommunicationProtocol = body.CommunicationProtocol,
                TypePoint = body.TypePoint,
                ChannelNumber = body.ChannelNumber,
                InOperation = body.InOperation,
                Fluid = body.Fluid,
                Installation = installationInDatabase,
                Description = body.Description is not null ? body.Description : null,
                User = user,
            };

            await _context.MeasuringEquipments.AddAsync(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return Created($"equipments/{equipment.Id}", equipmentDTO);
        }

        //[HttpGet("equipment")]
        //public async Task<IActionResult> Get()
        //{

        //}

        //[HttpGet("equipment/{id}")]
        //public async Task<IActionResult> GetById([FromRoute] Guid id)
        //{

        //}

        //[HttpPatch("equipment/{id}")]
        //public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClusterViewModel body)
        //{

        //}

        //[HttpPatch("equipment/{id}/restore")]
        //public async Task<IActionResult> Restore([FromRoute] Guid id)
        //{

        //}

        //[HttpDelete("equipment/{id}")]
        //public async Task<IActionResult> Delete([FromRoute] Guid id)
        //{

        //}
        //[HttpGet("equipment/{id}/history")]
        //public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        //{

        //}
    }

}
