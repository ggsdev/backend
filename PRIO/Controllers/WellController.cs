using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.WellDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.Filters;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Wells;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("wells")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class WellController : BaseApiController
    {
        public WellController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWellViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            //var wellInDatabase = await _context.Wells.FirstOrDefaultAsync(x => x.CodWell == body.CodWell);
            //if (wellInDatabase is not null)
            //    return Conflict(new ErrorResponseDTO
            //    {
            //        Message = $"Well with code: {body.CodWell} already exists, try another code."
            //    });

            var field = await _context.Fields
                .FirstOrDefaultAsync(x => x.Id == body.FieldId);

            if (field is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Field not found"
                });

            var wellId = Guid.NewGuid();

            var well = new Well
            {
                Id = wellId,
                CodWell = body.CodWell is not null ? body.CodWell : GenerateCode.Generate(body.Name),
                Name = body.Name,
                WellOperatorName = body.WellOperatorName,
                CodWellAnp = body.CodWellAnp,
                CategoryAnp = body.CategoryAnp,
                CategoryReclassificationAnp = body.CategoryReclassificationAnp,
                CategoryOperator = body.CategoryOperator,
                StatusOperator = body.StatusOperator,
                Type = body.Type,
                WaterDepth = body.WaterDepth,
                TopOfPerforated = body.TopOfPerforated,
                BaseOfPerforated = body.BaseOfPerforated,
                ArtificialLift = body.ArtificialLift,
                Latitude4C = body.Latitude4C,
                Longitude4C = body.Longitude4C,
                LongitudeDD = body.LongitudeDD,
                LatitudeDD = body.LatitudeDD,
                DatumHorizontal = body.DatumHorizontal,
                TypeBaseCoordinate = body.TypeBaseCoordinate,
                CoordX = body.CoordX,
                CoordY = body.CoordY,
                Description = body.Description,
                Field = field,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _context.Wells.AddAsync(well);

            var currentData = _mapper.Map<Well, WellHistoryDTO>(well);
            currentData.createdAt = DateTime.UtcNow;
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableWells,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = wellId,
                CurrentData = currentData,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);

            return Created($"wells/{well.Id}", wellDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var wells = await _context.Wells
                .Include(x => x.User)
                .Include(x => x.Completions)
                .Include(x => x.Field)
                .ThenInclude(f => f.Installation)
                .ThenInclude(i => i.Cluster)
                .ToListAsync();

            var wellsDTO = _mapper.Map<List<Well>, List<WellDTO>>(wells);
            return Ok(wellsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var well = await _context.Wells
               .Include(x => x.User)
                .Include(x => x.Completions)
                .Include(x => x.Field)
                .ThenInclude(f => f.Installation)
                .ThenInclude(i => i.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (well is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found"
                });
            var wellDTO = _mapper.Map<Well, WellDTO>(well);

            return Ok(wellDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWellViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var well = await _context.Wells
                .Include(x => x.Field)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (well is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found"
                });

            var beforeChangesWell = _mapper.Map<WellHistoryDTO>(well);

            var updatedProperties = ControllerUtils.CompareAndUpdateWell(well, body);

            if (updatedProperties.Any() is false && well.Field?.Id == body.FieldId)
                return BadRequest(new ErrorResponseDTO
                {
                    Message = "This well already has these values, try to update to other values."
                });

            if (body.FieldId is not null)
            {
                var fieldInDatabase = await _context.Fields
                    .FirstOrDefaultAsync(x => x.Id == body.FieldId);

                if (fieldInDatabase is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Field not found"
                    });

                well.Field = fieldInDatabase;
                updatedProperties[nameof(WellHistoryDTO.fieldId)] = fieldInDatabase.Id;
            }

            var firstHistory = await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == id)
               .FirstOrDefaultAsync();

            var changedFields = ControllerUtils.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<Well, WellHistoryDTO>(well);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableWells,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = well.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesWell,
            };

            await _context.SystemHistories.AddAsync(history);

            _context.Wells.Update(well);

            await _context.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);
            return Ok(wellDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var well = await _context.Wells
                .FirstOrDefaultAsync(x => x.Id == id);

            if (well is null || well.IsActive is false)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found or inactive already"
                });

            var lastHistory = await _context.SystemHistories
                .OrderBy(x => x.CreatedAt)
                .Where(x => x.TableItemId == well.Id)
                .LastOrDefaultAsync();

            well.IsActive = false;
            well.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<Well, WellHistoryDTO>(well);
            currentData.updatedAt = (DateTime)well.DeletedAt;
            currentData.deletedAt = well.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableWells,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = well.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = well.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    well.IsActive,
                    well.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

            _context.Wells.Update(well);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var well = await _context.Wells
                .Include(x => x.Field)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (well is null || well.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found or active already"
                });

            var lastHistory = await _context.SystemHistories
               .Where(x => x.TableItemId == well.Id)
               .OrderBy(x => x.CreatedAt)
               .LastOrDefaultAsync();

            well.IsActive = true;
            well.DeletedAt = null;

            var currentData = _mapper.Map<Well, WellHistoryDTO>(well);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableWells,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = well.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = well.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    well.IsActive,
                    well.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

            _context.Wells.Update(well);

            await _context.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);
            return Ok(wellDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var wellHistories = await _context.SystemHistories
                   .Where(x => x.TableItemId == id)
                   .OrderByDescending(x => x.CreatedAt)
                   .ToListAsync();

            if (wellHistories is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Well not found"
                });

            foreach (var history in wellHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return Ok(wellHistories);
        }
    }
}
