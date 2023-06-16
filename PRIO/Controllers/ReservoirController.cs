using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.ReservoirDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.Filters;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Reservoirs;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("reservoirs")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ReservoirController : BaseApiController
    {
        public ReservoirController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservoirViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            //var reservoirInDatabase = await _context.Reservoirs
            //    .FirstOrDefaultAsync(x => x.CodReservoir == body.CodReservoir);

            //if (reservoirInDatabase is not null)
            //    return Conflict(new ErrorResponseDTO
            //    {
            //        Message = $"Reservoir with code: {body.CodReservoir} already exists, try another code."
            //    });

            var zoneInDatabase = await _context.Zones
                .FirstOrDefaultAsync(x => x.Id == body.ZoneId);

            if (zoneInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Zone not found"
                });

            var reservoirId = Guid.NewGuid();

            var reservoir = new Reservoir
            {
                Id = reservoirId,
                Name = body.Name,
                Description = body.Description,
                CodReservoir = body.CodReservoir is not null ? body.CodReservoir : GenerateCode.Generate(body.Name),
                Zone = zoneInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _context.Reservoirs.AddAsync(reservoir);

            var currentData = _mapper.Map<Reservoir, ReservoirHistoryDTO>(reservoir);
            currentData.createdAt = DateTime.UtcNow;
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableReservoirs,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = reservoirId,
                CurrentData = currentData,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);

            return Created($"reservoirs/{reservoir.Id}", reservoirDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reservoirs = await _context.Reservoirs
                .Include(x => x.User)
                .Include(x => x.Zone)
                .ThenInclude(x => x!.Field)
                .ThenInclude(x => x!.Installation)
                .ThenInclude(x => x!.Cluster)
                .ToListAsync();

            var reservoirsDTO = _mapper.Map<List<Reservoir>, List<ReservoirDTO>>(reservoirs);
            return Ok(reservoirsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var reservoir = await _context.Reservoirs
                .Include(x => x.User)
                .Include(x => x.Zone)
                .ThenInclude(x => x!.Field)
                .ThenInclude(x => x!.Installation)
                .ThenInclude(x => x!.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (reservoir is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found"
                });

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return Ok(reservoirDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateReservoirViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var reservoir = await _context.Reservoirs
                .Include(x => x.User)
                .Include(x => x.Zone)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (reservoir is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found"
                });

            var beforeChangesReservoir = _mapper.Map<ReservoirHistoryDTO>(reservoir);

            var updatedProperties = ControllerUtils.CompareAndUpdateReservoir(reservoir, body);

            if (updatedProperties.Any() is false && reservoir.Zone?.Id == body.ZoneId)
                return BadRequest(new ErrorResponseDTO
                {
                    Message = "This reservoir already has these values, try to update to other values."
                });

            var zoneInDatabase = await _context.Zones
                .FirstOrDefaultAsync(x => x.Id == body.ZoneId);

            if (body.ZoneId is not null && zoneInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Zone not found"
                });

            _context.Reservoirs.Update(reservoir);

            var firstHistory = await _context.SystemHistories
              .OrderBy(x => x.CreatedAt)
              .Where(x => x.TableItemId == id)
              .FirstOrDefaultAsync();

            var changedFields = ControllerUtils.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<Reservoir, ReservoirHistoryDTO>(reservoir);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableReservoirs,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = reservoir.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesReservoir,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);
            return Ok(reservoirDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var reservoir = await _context.Reservoirs
                .Include(x => x.Zone)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (reservoir is null || reservoir.IsActive is false)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found or inactive already"
                });

            var lastHistory = await _context.SystemHistories
                .OrderBy(x => x.CreatedAt)
                .Where(x => x.TableItemId == reservoir.Id)
                .LastOrDefaultAsync();

            reservoir.IsActive = false;
            reservoir.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<Reservoir, ReservoirHistoryDTO>(reservoir);
            currentData.updatedAt = (DateTime)reservoir.DeletedAt;
            currentData.deletedAt = reservoir.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableReservoirs,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = reservoir.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = reservoir.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    reservoir.IsActive,
                    reservoir.DeletedAt,
                }
            };
            await _context.SystemHistories.AddAsync(history);

            _context.Reservoirs.Update(reservoir);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var reservoir = await _context.Reservoirs
                .Include(x => x.Zone)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (reservoir is null || reservoir.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found or active already"
                });

            var lastHistory = await _context.SystemHistories
               .Where(x => x.TableItemId == reservoir.Id)
               .OrderBy(x => x.CreatedAt)
               .LastOrDefaultAsync();

            reservoir.IsActive = true;
            reservoir.DeletedAt = null;

            var currentData = _mapper.Map<Reservoir, ReservoirHistoryDTO>(reservoir);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableReservoirs,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = reservoir.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = reservoir.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    reservoir.IsActive,
                    reservoir.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

            _context.Reservoirs.Update(reservoir);

            await _context.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);

            return Ok(reservoirDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var reservoirHistories = await _context.SystemHistories
                  .Where(x => x.TableItemId == id)
                  .OrderByDescending(x => x.CreatedAt)
                  .ToListAsync();

            if (reservoirHistories is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Reservoir not found"
                });

            foreach (var history in reservoirHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return Ok(reservoirHistories);
        }
    }
}
