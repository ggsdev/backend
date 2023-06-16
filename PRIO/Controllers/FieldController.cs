using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.FieldDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.Filters;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Fields;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("fields")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class FieldController : BaseApiController
    {
        public FieldController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFieldViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            //var fieldInDatabase = await _context.Fields
            //    .FirstOrDefaultAsync(x => x.CodField == body.CodField);

            //if (fieldInDatabase is not null)
            //    return Conflict(new ErrorResponseDTO
            //    {
            //        Message = $"Field with code: {body.CodField} already exists."
            //    });

            var installationInDatabase = await _context.Installations
                .FirstOrDefaultAsync(x => x.Id == body.InstallationId);

            if (installationInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Installation not found"
                });

            var fieldId = Guid.NewGuid();

            var field = new Field
            {
                Id = fieldId,
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

            var currentData = _mapper.Map<Field, FieldHistoryDTO>(field);
            currentData.createdAt = DateTime.UtcNow;
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableFields,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = fieldId,
                CurrentData = currentData,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);

            return Created($"fields/{field.Id}", fieldDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fields = await _context.Fields
                .Include(x => x.Installation)
                .ThenInclude(i => i!.Cluster)
                .Include(x => x.User)
                .ToListAsync();

            var fieldsDTO = _mapper.Map<List<Field>, List<FieldDTO>>(fields);

            return Ok(fieldsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var field = await _context.Fields
                .Include(x => x.Installation)
                .ThenInclude(i => i!.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);

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

            var field = await _context.Fields
                .Include(x => x.User)
                .Include(x => x.Installation)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (field is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });

            var beforeChangesField = _mapper.Map<FieldHistoryDTO>(field);

            var updatedProperties = ControllerUtils.CompareAndUpdateField(field, body);

            if (updatedProperties.Any() is false && field.Installation?.Id == body.InstallationId)
                return BadRequest(new ErrorResponseDTO
                {
                    Message = "This field already has these values, try to update to other values."
                });

            var installationInDatabase = await _context.Installations
               .FirstOrDefaultAsync(x => x.Id == body.InstallationId);

            if (body.InstallationId is not null && installationInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            if (body.InstallationId is not null && installationInDatabase is not null && field.Installation?.Id == body.InstallationId)
            {
                field.Installation = installationInDatabase;
                updatedProperties[nameof(FieldHistoryDTO.installationId)] = installationInDatabase.Id;
            }

            _context.Fields.Update(field);

            var firstHistory = await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == id)
               .FirstOrDefaultAsync();

            var changedFields = ControllerUtils.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<Field, FieldHistoryDTO>(field);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableFields,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = field.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesField,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);

            return Ok(fieldDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var field = await _context.Fields.Include(x => x.Installation).FirstOrDefaultAsync(x => x.Id == id);
            if (field is null || field.IsActive is false)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found or inactive already"
                });

            var lastHistory = await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == field.Id)
               .LastOrDefaultAsync();

            field.IsActive = false;
            field.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<Field, FieldHistoryDTO>(field);
            currentData.updatedAt = (DateTime)field.DeletedAt;
            currentData.deletedAt = field.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableFields,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = field.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = field.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    field.IsActive,
                    field.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

            _context.Update(field);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid fieldId)
        {
            var user = HttpContext.Items["User"] as User;

            var field = await _context.Fields
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == fieldId);

            if (field is null || field.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found or active already"
                });

            var lastHistory = await _context.SystemHistories
              .Where(x => x.TableItemId == field.Id)
              .OrderBy(x => x.CreatedAt)
              .LastOrDefaultAsync();

            field.IsActive = true;
            field.DeletedAt = null;

            var currentData = _mapper.Map<Field, FieldHistoryDTO>(field);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableFields,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = field.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = field.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    field.IsActive,
                    field.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

            _context.Update(field);
            await _context.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);
            return Ok(fieldDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var fieldHistories = await _context.SystemHistories
                   .Where(x => x.TableItemId == id)
                   .OrderByDescending(x => x.CreatedAt)
                   .ToListAsync();

            if (fieldHistories is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Field not found"
                });

            foreach (var history in fieldHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()) : null;
            }

            return Ok(fieldHistories);
        }

    }
}
