using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.FieldDTOS;
using PRIO.Filters;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.ViewModels.Fields;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("fields")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class FieldController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FieldController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFieldViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var fieldInDatabase = await _context.Fields.FirstOrDefaultAsync(x => x.CodField == body.CodField);
            if (fieldInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Field with code: {body.CodField} already exists."
                });

            var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.Id == body.InstallationId);
            if (installationInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Installation not found"
                });

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
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _context.Fields.AddAsync(field);
            await _context.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);
            return Created($"fields/{field.Id}", fieldDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fields = await _context.Fields.Include(x => x.Zones).Include(x => x.User).ToListAsync();
            var fieldsDTO = _mapper.Map<List<Field>, List<FieldDTO>>(fields);

            return Ok(fieldsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var field = await _context.Fields.FirstOrDefaultAsync(x => x.Id == id);
            if (field is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });
            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);

            return Ok(fieldDTO);

        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFieldViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var field = await _context.Fields.Include(x => x.User).Include(x => x.Installation).FirstOrDefaultAsync(x => x.Id == id);
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
                field.Installation = installationInDatabase;
            }
            else
            {
                field.Installation = field.Installation;
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

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var field = await _context.Fields.Include(x => x.Installation).FirstOrDefaultAsync(x => x.Id == id);
            if (field is null || !field.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found or inactive already"
                });

            field.IsActive = false;
            field.DeletedAt = DateTime.UtcNow;

            _context.Update(field);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid fieldId)
        {
            var user = HttpContext.Items["User"] as User;

            var field = await _context.Fields.Include(x => x.Installation).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == fieldId);
            if (field is null || field.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found or active already"
                });

            field.IsActive = true;
            field.DeletedAt = null;

            _context.Update(field);
            await _context.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);
            return Ok(fieldDTO);
        }

        //[HttpGet("{id:Guid}/history")]
        //public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        //{
        //    var fieldHistories = await _context.FieldHistories.Include(x => x.User)
        //                                              .Include(x => x.Field)
        //                                              .Where(x => x.Field.Id == id)
        //                                              .OrderByDescending(x => x.CreatedAt)
        //                                              .ToListAsync();
        //    if (fieldHistories is null)
        //        return NotFound(new ErrorResponseDTO
        //        {
        //            Message = "Field not found"
        //        });


        //    var fieldHistoryDTO = _mapper.Map<List<FieldHistory>, List<FieldHistoryDTO>>(fieldHistories);

        //    return Ok(fieldHistoryDTO);
        //}

    }
}
