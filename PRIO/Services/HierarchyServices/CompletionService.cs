using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS.HierarchyDTOS.CompletionDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.Exceptions;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.HierarchyViewModels.Completions;

namespace PRIO.Services.HierarchyServices
{
    public class CompletionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CompletionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompletionDTO> CreateCompletion(CreateCompletionViewModel body, User user)
        {
            var well = await _context.Wells
              .Include(x => x.Field)
              .FirstOrDefaultAsync(x => x.Id == body.WellId);

            if (well is null)
                throw new NotFoundException($"Well with id: {body.WellId} not found");

            var reservoir = await _context.Reservoirs
                .Include(x => x.Zone)
                .ThenInclude(z => z.Field)
                .FirstOrDefaultAsync(x => x.Id == body.ReservoirId);

            if (reservoir is null)
                throw new NotFoundException($"Reservoir with id: {body.ReservoirId} not found");

            if (reservoir.Zone?.Field?.Id != well.Field?.Id)
                throw new ConflictException($"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field");

            var completion = await _context.Completions
                .FirstOrDefaultAsync(x => x.Well.Id == well.Id && x.Reservoir.Id == reservoir.Id);

            if (completion is not null)
                throw new ConflictException($"Completion with name: {well.Name}_{reservoir.Zone?.CodZone} already exists.");

            var completionName = $"{well.Name}_{reservoir.Zone?.CodZone}";
            var completionId = Guid.NewGuid();

            completion = new Completion
            {
                Id = completionId,
                Name = completionName,
                CodCompletion = body.CodCompletion is not null ? body.CodCompletion : GenerateCode.Generate(completionName),
                Description = body.Description,
                User = user,
                Well = well,
                Reservoir = reservoir,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            var currentData = _mapper.Map<Completion, CompletionHistoryDTO>(completion);
            currentData.createdAt = DateTime.UtcNow;
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCompletions,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = completionId,
                CurrentData = currentData,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.Completions.AddAsync(completion);

            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);

            return completionDTO;
        }

        public async Task<List<CompletionDTO>> GetCompletions()
        {
            var completions = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .Include(x => x.User)
                .ToListAsync();

            var completionsDTO = _mapper.Map<List<Completion>, List<CompletionDTO>>(completions);

            return completionsDTO;
        }

        public async Task<CompletionDTO> GetCompletionById(Guid id)
        {
            var completion = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null)
                throw new NotFoundException("Completion not found");

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task<CompletionDTO> UpdateCompletion(UpdateCompletionViewModel body, Guid id, User user)
        {
            var completion = await _context.Completions
               .Include(x => x.Well)
               .Include(x => x.Reservoir)
               .ThenInclude(x => x.Zone)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null)
                throw new NotFoundException("Completion not found");

            var well = await _context.Wells
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Id == body.WellId);

            var reservoir = await _context.Reservoirs
                    .Include(x => x.Zone)
                    .ThenInclude(z => z.Field)
                    .FirstOrDefaultAsync(z => z.Id == body.ReservoirId);

            var beforeChangesCompletion = _mapper.Map<CompletionHistoryDTO>(completion);

            var updatedProperties = UpdateFields.CompareAndUpdateCompletion(completion, body);

            if (body.WellId is not null)
            {
                if (well is null)
                    throw new NotFoundException("Well not found");

                if (reservoir is null && body.ReservoirId is not null)
                    throw new NotFoundException("Reservoir not found");

                if (reservoir is not null && well.Field?.Id != reservoir.Zone?.Field?.Id)
                    throw new ConflictException($"Well: {well.Name} and Reservoir: {reservoir.Name} doesn't belong to the same Field");

                completion.Name = $"{well.Name}_{completion.Reservoir?.Zone?.CodZone}";
                completion.Well = well;
                updatedProperties[nameof(CompletionHistoryDTO.wellId)] = well.Id;
                updatedProperties[nameof(CompletionHistoryDTO.name)] = completion.Name;
            }

            if (body.ReservoirId is not null)
            {
                if (reservoir is null)
                    throw new NotFoundException("Reservoir not found");


                if (well is null && body.WellId is not null)
                    throw new NotFoundException("Well not found");

                if (well is not null && reservoir.Zone?.Field?.Id != well.Field?.Id)
                    throw new ConflictException($"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field");

                completion.Name = $"{completion.Well?.Name}_{reservoir.Zone?.CodZone}";
                completion.Reservoir = reservoir;
                updatedProperties[nameof(CompletionHistoryDTO.reservoirId)] = reservoir.Id;
                updatedProperties[nameof(CompletionHistoryDTO.name)] = completion.Name;
            }

            _context.Completions.Update(completion);

            var firstHistory = await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == id)
               .FirstOrDefaultAsync();

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<Completion, CompletionHistoryDTO>(completion);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCompletions,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = completion.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesCompletion,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task DeleteCompletion(Guid id, User user)
        {
            var completion = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null || !completion.IsActive)
                throw new NotFoundException("Completion not found or inactive already");

            var lastHistory = await _context.SystemHistories
              .OrderBy(x => x.CreatedAt)
              .Where(x => x.TableItemId == completion.Id)
              .LastOrDefaultAsync();

            completion.IsActive = false;
            completion.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<Completion, CompletionHistoryDTO>(completion);
            currentData.updatedAt = (DateTime)completion.DeletedAt;
            currentData.deletedAt = completion.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCompletions,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = completion.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = completion.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    completion.IsActive,
                    completion.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

            _context.Completions.Update(completion);
            await _context.SaveChangesAsync();
        }

        public async Task<CompletionDTO> RestoreCompletion(Guid id, User user)
        {
            var completion = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null || completion.IsActive is true)
                throw new NotFoundException("Completion not found or inactive already");

            var lastHistory = await _context.SystemHistories
             .Where(x => x.TableItemId == completion.Id)
             .OrderBy(x => x.CreatedAt)
             .LastOrDefaultAsync();

            completion.IsActive = true;
            completion.DeletedAt = null;

            var currentData = _mapper.Map<Completion, CompletionHistoryDTO>(completion);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCompletions,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = completion.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = completion.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    completion.IsActive,
                    completion.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);
            _context.Completions.Update(completion);

            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task<List<SystemHistory>> GetCompletionHistory(Guid id)
        {
            var fieldHistories = await _context.SystemHistories
                  .Where(x => x.TableItemId == id)
                  .OrderByDescending(x => x.CreatedAt)
                  .ToListAsync();

            if (fieldHistories is null)
                throw new NotFoundException("Completion not found");

            foreach (var history in fieldHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return fieldHistories;
        }
    }
}
