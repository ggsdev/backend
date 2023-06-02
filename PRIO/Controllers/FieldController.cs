using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.ClusterDTOS;
using PRIO.DTOS.FieldDTOS;
using PRIO.Models.Clusters;
using PRIO.Models.Fields;
using PRIO.Models.Installations;
using PRIO.Services;
using PRIO.Utils;
using PRIO.ViewModels.Fields;
using System.Diagnostics.Metrics;

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

            var fieldHistory = new FieldHistory
            {
                Name = body.Name,
                NameOld = null,
                CodField = body.CodField,
                CodFieldOld = null,
                Basin = body.Basin,
                BasinOld = null,
                Location = body.Location,
                LocationOld = null,
                State = body.State,
                StateOld = null,
                Description = body.Description,
                DescriptionOld = null,
                IsActiveOld = null,
                IsActive = true,
                User = user,
                Field = field,
                Installation = installationInDatabase,
                InstallationOld = installationInDatabase.Id,
                Type = TypeOperation.Create,
                
            };
            await _context.FieldHistories.AddAsync(fieldHistory);
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

        [HttpPatch("fields/{fieldId}")]
        public async Task<IActionResult> Update([FromRoute] Guid fieldId, [FromBody] UpdateFieldViewModel body)
        {
            var field = await _context.Fields.Include(x => x.User).Include(x => x.Installation).FirstOrDefaultAsync(x => x.Id == fieldId);
            Console.WriteLine(field.Installation.Id);
            if (field is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });




            var fieldHistory = new FieldHistory
            {
                Name = body.Name is not null ? body.Name : field.Name,
                NameOld = field.Name,
                CodField = body.CodField is not null ? body.CodField : field.CodField,
                CodFieldOld = field.CodField,
                Basin = body.Basin is not null ? body.Basin : field.Basin,
                BasinOld = field.Basin,
                Location = body.Location is not null ? body.Location : field.Location,
                LocationOld = field.Location,
                State = body.State is not null ? body.State : field.State,
                StateOld = field.State,
                Description = body.Description is not null ? body.Description : field.Description,
                DescriptionOld = field.Description,
                IsActiveOld = true,
                IsActive = field.IsActive,
                User = user,
                Field = field,
                InstallationOld = field.Installation.Id,
                Type = TypeOperation.Update,
            };

            if (body.InstallationId is not null)
            {
                var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.Id == body.InstallationId);

                if (installationInDatabase is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Installation not found"
                    });
                fieldHistory.Installation = installationInDatabase;
                field.Installation = installationInDatabase;
            }
            else { 
                fieldHistory.Installation = field.Installation;
                field.Installation = field.Installation;
            }

            await _context.FieldHistories.AddAsync(fieldHistory);

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

        [HttpDelete("fields/{fieldId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid fieldId)
        {
            var field = await _context.Fields.Include(x => x.Installation).FirstOrDefaultAsync(x => x.Id == fieldId);
            if (field is null || !field.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found or inactive already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var fieldHistory = new FieldHistory
            {
                Name = field.Name,
                NameOld = field.Name,
                CodField = field.CodField,
                CodFieldOld = field.CodField,
                Basin = field.Basin,
                BasinOld = field.Basin,
                Location = field.Location,
                LocationOld = field.Location,
                State = field.State,
                StateOld = field.State,
                Description = field.Description,
                DescriptionOld = field.Description,
                IsActiveOld = field.IsActive,
                IsActive = false,
                User = user,
                Field = field,
                Installation = field.Installation,
                InstallationOld = field.Installation.Id,
                Type = TypeOperation.Delete,
            };
            _context.Update(fieldHistory);

            field.IsActive = false;
            field.DeletedAt = DateTime.UtcNow;

            _context.Update(field);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("fields/{fieldId}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid fieldId)
        {
            var field = await _context.Fields.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == fieldId);
            if (field is null || field.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found or active already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });
            Console.WriteLine(field);
            var fieldHistory = new FieldHistory
            {
                Name = field.Name,
                NameOld = field.Name,
                CodField = field.CodField,
                CodFieldOld = field.CodField,
                Basin = field.Basin,
                BasinOld = field.Basin,
                Location = field.Location,
                LocationOld = field.Location,
                State = field.State,
                StateOld = field.State,
                Description = field.Description,
                DescriptionOld = field.Description,
                IsActiveOld = field.IsActive,
                IsActive = true,
                User = user,
                Field = field,
                Installation = field.Installation,
                InstallationOld = field.Installation.Id,
                Type = TypeOperation.Restore,
            };
            _context.Update(fieldHistory);

            field.IsActive = true;
            field.DeletedAt = null;

            _context.Update(field);
            await _context.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);
            return Ok(fieldDTO);
        }

    }
}
