using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.MeasuringEquipment;
using PRIO.Filters;
using PRIO.Models.MeasuringEquipments;
using PRIO.ViewModels.MeasuringEquipment;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("equipments")]
    [ServiceFilter(typeof(AuthorizationFilter))]
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
            var fluidsAllowed = new List<string>
            {
                "gás","óleo","água"
            };

            if (body.Fluid is not null && !fluidsAllowed.Contains(body.Fluid.ToLower()))
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Message = "Fluids allowed are: gás, óleo,água"
                });
            }

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return Unauthorized(new ErrorResponseDTO { Message = "User not identified, please login first" });

            var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.Id == body.InstallationId);
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
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };
            await _context.MeasuringEquipments.AddAsync(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return Created($"equipments/{equipment.Id}", equipmentDTO);
        }

        [HttpGet("equipments")]
        public async Task<IActionResult> Get()
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return Unauthorized(new ErrorResponseDTO { Message = "User not identified, please login first" });

            var equipments = await _context.MeasuringEquipments.ToListAsync();
            var equipmentDTO = _mapper.Map<List<MeasuringEquipment>, List<MeasuringEquipmentDTO>>(equipments);
            return Ok(equipmentDTO);

        }

        [HttpGet("equipments/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return Unauthorized(new ErrorResponseDTO { Message = "User not identified, please login first" });

            var equipment = _context.MeasuringEquipments.FirstOrDefault(x => x.Id == id);
            if (equipment is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Equipment not found"
                });
            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return Ok(equipmentDTO);
        }

        [HttpPatch("equipments/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEquipmentViewModel body)
        {
            var equipment = _context.MeasuringEquipments.Include(x => x.Installation).FirstOrDefault(x => x.Id == id);
            if (equipment is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Equipment not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.Id == body.InstallationId);

            if (body.InstallationId is not null && installationInDatabase is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });
            }

            equipment.TagEquipment = body.TagEquipment is not null ? body.TagEquipment : equipment.TagEquipment;
            equipment.TagMeasuringPoint = body.TagMeasuringPoint is not null ? body.TagMeasuringPoint : equipment.TagMeasuringPoint;
            equipment.SerieNumber = body.SerieNumber is not null ? body.SerieNumber : equipment.SerieNumber;
            equipment.Type = body.Type is not null ? body.Type : equipment.Type;
            equipment.TypeEquipment = body.TagEquipment is not null ? body.TagEquipment : equipment.TagEquipment;
            equipment.Model = body.Model is not null ? body.Model : equipment.Model;
            equipment.HasSeal = body.HasSeal is not null ? body.HasSeal : equipment.HasSeal;
            equipment.MVS = body.MVS is not null ? body.MVS : equipment.MVS;
            equipment.CommunicationProtocol = body.CommunicationProtocol is not null ? body.CommunicationProtocol : equipment.CommunicationProtocol;
            equipment.TypePoint = body.TypePoint is not null ? body.TypePoint : equipment.TypePoint;
            equipment.ChannelNumber = body.ChannelNumber is not null ? body.ChannelNumber : equipment.ChannelNumber;
            equipment.InOperation = body.InOperation is not null ? body.InOperation : equipment.InOperation;
            equipment.Fluid = body.Fluid is not null ? body.Fluid : equipment.Fluid;
            equipment.Installation = installationInDatabase is not null ? installationInDatabase : equipment.Installation;
            equipment.Description = body.Description is not null ? body.Description : equipment.Description;

            _context.MeasuringEquipments.Update(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return Ok(equipmentDTO);
        }

        [HttpDelete("equipments/{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return Unauthorized(new ErrorResponseDTO { Message = "User not identified, please login first" });

            var equipment = _context.MeasuringEquipments.FirstOrDefault(x => x.Id == id);
            if (equipment is null || !equipment.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Equipment not found or inactive already"
                });

            equipment.IsActive = false;
            equipment.DeletedAt = DateTime.UtcNow;

            _context.MeasuringEquipments.Update(equipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("equipments/{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return Unauthorized(new ErrorResponseDTO { Message = "User not identified, please login first" });

            var equipment = _context.MeasuringEquipments.FirstOrDefault(x => x.Id == id);
            if (equipment is null || equipment.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Equipment not found or active already"
                });

            equipment.IsActive = true;
            equipment.DeletedAt = null;

            _context.MeasuringEquipments.Update(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return Ok(equipmentDTO);

        }

        //[HttpGet("equipment/{id}/history")]
        //public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        //{

        //}

    }

}
