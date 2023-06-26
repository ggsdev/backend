using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Dtos;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Completions.ViewModels;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Completions.Infra.Http.Services
{
    public class CompletionService
    {
        private readonly IMapper _mapper;
        private readonly ICompletionRepository _completionRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly ISystemHistoryRepository _systemHistoryRepository;

        public CompletionService(IMapper mapper, ICompletionRepository completionRepository, IWellRepository wellRepository, IReservoirRepository reservoirRepository, ISystemHistoryRepository systemHistoryRepository)
        {
            _mapper = mapper;
            _completionRepository = completionRepository;
            _wellRepository = wellRepository;
            _reservoirRepository = reservoirRepository;
            _systemHistoryRepository = systemHistoryRepository;
        }

        public async Task<CreateUpdateCompletionDTO> CreateCompletion(CreateCompletionViewModel body, User user)
        {
            var well = await _wellRepository.GetWithFieldAsync(body.WellId);

            if (well is null)
                throw new NotFoundException($"Well with id: {body.WellId} not found");

            var reservoir = await _reservoirRepository.GetWithZoneFieldAsync(body.ReservoirId);

            if (reservoir is null)
                throw new NotFoundException($"Reservoir with id: {body.ReservoirId} not found");

            if (reservoir.Zone?.Field?.Id != well.Field?.Id)
                throw new ConflictException($"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field");

            var completion = await _completionRepository.GetExistingCompletionAsync(well.Id, reservoir.Id);

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

            await _systemHistoryRepository.AddAsync(history);

            await _completionRepository.AddAsync(completion);

            await _completionRepository.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CreateUpdateCompletionDTO>(completion);

            return completionDTO;
        }

        public async Task<List<CompletionDTO>> GetCompletions()
        {
            var completions = await _completionRepository.GetAsync();

            var completionsDTO = _mapper.Map<List<Completion>, List<CompletionDTO>>(completions);

            return completionsDTO;
        }

        public async Task<CompletionWithWellAndReservoirDTO> GetCompletionById(Guid id)
        {
            var completion = await _completionRepository.GetByIdAsync(id);

            if (completion is null)
                throw new NotFoundException("Completion not found");

            var completionDTO = _mapper.Map<Completion, CompletionWithWellAndReservoirDTO>(completion);
            return completionDTO;
        }

        public async Task<CompletionDTO> UpdateCompletion(UpdateCompletionViewModel body, Guid id, User user)
        {
            var completion = await _completionRepository.GetWithWellReservoirZoneAsync(id);

            if (completion is null)
                throw new NotFoundException("Completion not found");

            var well = await _wellRepository.GetWithFieldAsync(body.WellId);

            var reservoir = await _reservoirRepository.GetWithZoneFieldAsync(body.ReservoirId);

            var beforeChangesCompletion = _mapper.Map<CompletionHistoryDTO>(completion);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(completion, body);

            if (updatedProperties.Any() is false && body?.WellId == completion.Well?.Id && body?.ReservoirId == completion.Reservoir?.Id)
                throw new BadRequestException("This completion already has these values, try to update to other values.");

            if (body?.WellId is not null)
            {
                if (well is null)
                    throw new NotFoundException("Well not found");

                if (reservoir is null && body?.ReservoirId is not null)
                    throw new NotFoundException("Reservoir not found");

                if (reservoir is not null && well.Field?.Id != reservoir.Zone?.Field?.Id)
                    throw new ConflictException($"Well: {well.Name} and Reservoir: {reservoir.Name} doesn't belong to the same Field");

                completion.Name = $"{well.Name}_{completion.Reservoir?.Zone?.CodZone}";
                completion.Well = well;

                if (completion.Well.Id != body?.WellId)
                {
                    updatedProperties[nameof(CompletionHistoryDTO.wellId)] = completion.Well.Id;
                    updatedProperties[nameof(CompletionHistoryDTO.name)] = completion.Name;
                }
            }

            if (body?.ReservoirId is not null)
            {
                if (reservoir is null)
                    throw new NotFoundException("Reservoir not found");

                if (well is null && body.WellId is not null)
                    throw new NotFoundException("Well not found");

                if (well is not null && reservoir.Zone?.Field?.Id != well.Field?.Id)
                    throw new ConflictException($"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field");

                completion.Name = $"{completion.Well?.Name}_{reservoir.Zone?.CodZone}";
                completion.Reservoir = reservoir;
                if (completion.Reservoir.Id != body?.ReservoirId)
                {
                    updatedProperties[nameof(CompletionHistoryDTO.reservoirId)] = completion.Reservoir.Id;
                    updatedProperties[nameof(CompletionHistoryDTO.name)] = completion.Name;
                }
            }

            _completionRepository.Update(completion);

            var firstHistory = await _systemHistoryRepository.GetFirst(id);

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

            await _systemHistoryRepository.AddAsync(history);

            await _completionRepository.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task DeleteCompletion(Guid id, User user)
        {
            var completion = await _completionRepository.GetOnlyCompletion(id);

            if (completion is null || !completion.IsActive)
                throw new NotFoundException("Completion not found or inactive already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

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

            await _systemHistoryRepository.AddAsync(history);

            _completionRepository.Update(completion);
            await _completionRepository.SaveChangesAsync();
        }

        public async Task<CompletionDTO> RestoreCompletion(Guid id, User user)
        {
            var completion = await _completionRepository.GetByIdAsync(id);

            if (completion is null || completion.IsActive is true)
                throw new NotFoundException("Completion not found or inactive already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

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

            await _systemHistoryRepository.AddAsync(history);
            _completionRepository.Update(completion);

            await _completionRepository.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task<List<SystemHistory>> GetCompletionHistory(Guid id)
        {
            var fieldHistories = await _systemHistoryRepository.GetAll(id);

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
