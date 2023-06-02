using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models.Fields;
using PRIO.Services;
using PRIO.ViewModels.Fields;

namespace PRIO.Controllers
{
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly FieldServices _fieldServices;
        private readonly IMapper _mapper;

        public FieldController(DataContext context, FieldServices fieldService, IMapper mapper)
        {
            _context = context;
            _fieldServices = fieldService;
            _mapper = mapper;

        }

        [HttpPost("fields")]
        public async Task<IActionResult> Create([FromBody] CreateFieldViewModel body)
        {
            var fieldInDatabase = await _context.Fields.FirstOrDefaultAsync(x => x.CodField == body.CodField);
            if (fieldInDatabase is not null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Field with code: {body.CodField} already exists."
                });

            var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.Id == body.InstallationId);
            if (installationInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Installation not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = "User not found"
                });
            }

            var field = new Field
            {
                Name = body.Name,
                User = user,
                Description = body.Description,
                Basin = body.Basin,
                Location = body.Location,
                State = body.State,
                CodField = body.CodField,
                Installation = installationInDatabase,
            };

            await _context.Fields.AddAsync(field);
            await _context.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);

            return Created($"fields/{field.Id}", fieldDTO);

        }

        [HttpGet("fields")]
        public async Task<IActionResult> Get()
        {
            var fields = await _context.Fields.Include(x => x.Zones).Include(x => x.User).ToListAsync();
            var fieldsDTO = _mapper.Map<List<Field>, List<FieldDTO>>(fields);

            return Ok(fieldsDTO);
        }

        [HttpGet("fields/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var field = await _fieldServices.GetFieldById(id);
            if (field is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });
            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);

            return Ok(fieldDTO);

        }

        [HttpPatch("fields/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFieldViewModel body)
        {
            var field = await _fieldServices.GetFieldById(id);
            if (field is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });

            if (body.InstallationId is not null)
            {
                var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.Id == body.InstallationId);

                if (installationInDatabase is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Installation not found"
                    });
                field.Installation = installationInDatabase is not null ? installationInDatabase : field.Installation;
            }

            field.State = body.State is not null ? body.State : field.State;
            field.Name = body.Name is not null ? body.Name : field.Name;
            field.CodField = body.CodField is not null ? body.CodField : field.CodField;
            field.Basin = body.Basin is not null ? body.Basin : field.Basin;
            field.Location = body.Location is not null ? body.Location : field.Location;
            field.Description = body.Description is not null ? body.Description : field.Description;

            _context.Fields.Update(field);
            await _context.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);

            return Ok(fieldDTO);
        }

        [HttpDelete("fields/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var field = await _fieldServices.GetFieldById(id);
            if (field is null || !field.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found or inactive already"
                });

            field.IsActive = false;
            field.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
