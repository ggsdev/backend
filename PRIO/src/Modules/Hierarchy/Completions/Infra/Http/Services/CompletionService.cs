﻿using AutoMapper;
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
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Completions.Infra.Http.Services
{
    public class CompletionService
    {
        private readonly IMapper _mapper;
        private readonly ICompletionRepository _completionRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableCompletions;
        public CompletionService(IMapper mapper, ICompletionRepository completionRepository, IWellRepository wellRepository, IReservoirRepository reservoirRepository, SystemHistoryService systemHistoryService)
        {
            _mapper = mapper;
            _completionRepository = completionRepository;
            _wellRepository = wellRepository;
            _reservoirRepository = reservoirRepository;
            _systemHistoryService = systemHistoryService;
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

            await _systemHistoryService
                .Create<Completion, CompletionHistoryDTO>(_tableName, user, completionId, completion);

            await _completionRepository.AddAsync(completion);

            await _completionRepository.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CreateUpdateCompletionDTO>(completion);

            return completionDTO;
        }

        public async Task<List<CompletionWithWellAndReservoirDTO>> GetCompletions()
        {
            var completions = await _completionRepository.GetAsync();

            var completionsDTO = _mapper.Map<List<Completion>, List<CompletionWithWellAndReservoirDTO>>(completions);

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

            if (updatedProperties.Any() is false && (body?.WellId == completion.Well?.Id || body?.WellId is null) && (body?.ReservoirId == completion.Reservoir?.Id || body?.ReservoirId is null))
                throw new BadRequestException("This completion already has these values, try to update to other values.");

            if (body?.WellId is not null && completion.Well?.Id != body.WellId)
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
            Console.WriteLine(completion.Reservoir?.Id);
            Console.WriteLine(body.ReservoirId);
            if (body?.ReservoirId is not null && completion.Reservoir?.Id != body.ReservoirId)
            {
                if (reservoir is null)
                    throw new NotFoundException("Reservoir not found");

                if (well is null && body.WellId is not null)
                    throw new NotFoundException("Well not found");

                if (well is not null && reservoir.Zone?.Field?.Id != well.Field?.Id)
                    throw new ConflictException($"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field");

                completion.Name = $"{completion.Well?.Name}_{reservoir.Zone?.CodZone}";
                completion.Reservoir = reservoir;

                Console.WriteLine("oi");
                updatedProperties[nameof(CompletionHistoryDTO.reservoirId)] = completion.Reservoir.Id;
                updatedProperties[nameof(CompletionHistoryDTO.name)] = completion.Name;

            }

            _completionRepository.Update(completion);

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, completion.Id, completion, beforeChangesCompletion);

            await _completionRepository.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task DeleteCompletion(Guid id, User user)
        {
            var completion = await _completionRepository.GetOnlyCompletion(id);

            if (completion is null || !completion.IsActive)
                throw new NotFoundException("Completion not found or inactive already");

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(completion, propertiesUpdated);

            await _systemHistoryService
                .Delete<Completion, CompletionHistoryDTO>(_tableName, user, updatedProperties, completion.Id, completion);

            _completionRepository.Update(completion);
            await _completionRepository.SaveChangesAsync();
        }

        public async Task<CompletionDTO> RestoreCompletion(Guid id, User user)
        {
            var completion = await _completionRepository.GetByIdAsync(id);

            if (completion is null || completion.IsActive is true)
                throw new NotFoundException("Completion not found or inactive already");

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(completion, propertiesUpdated);

            await _systemHistoryService
                .Restore<Completion, CompletionHistoryDTO>(_tableName, user, updatedProperties, completion.Id, completion);

            _completionRepository.Update(completion);

            await _completionRepository.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task<List<SystemHistory>> GetCompletionHistory(Guid id)
        {
            var fieldHistories = await _systemHistoryService.GetAll(id);

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
