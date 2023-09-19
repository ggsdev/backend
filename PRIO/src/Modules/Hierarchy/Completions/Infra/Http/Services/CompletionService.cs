using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Dtos;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Completions.ViewModels;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
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
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (well.IsActive is false && well.StatusOperator is false)
                throw new ConflictException(ErrorMessages.Inactive<Well>());

            if (well.Completions is not null && well.Completions.Count == 2)
                throw new ConflictException("Este poço já possui duas completações.");

            if (well.Completions is not null && well.Completions.Count == 1)
            {
                var result = well.Completions[0].AllocationReservoir + body.AllocationReservoir;
                if (result != 1)
                    throw new ConflictException("A soma das alocações por reservatório deve ser 1.");
            }

            var reservoir = await _reservoirRepository.GetWithZoneFieldAsync(body.ReservoirId);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (reservoir.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Reservoir>());

            if (reservoir.Zone?.Field?.Id != well.Field?.Id)
                throw new ConflictException(ErrorMessages.DifferentFieldsCompletion());

            var completion = await _completionRepository
                .GetExistingCompletionAsync(well.Id, reservoir.Id);

            if (completion is not null)
                throw new ConflictException(ErrorMessages.WellAndReservoirAlreadyCompletion());

            var completionName = $"{well.Name}_{reservoir.Zone?.CodZone}";
            var completionId = Guid.NewGuid();

            completion = new Completion
            {
                Id = completionId,
                Name = completionName,
                Description = body.Description,
                User = user,
                Well = well,
                TopOfPerforated = body.TopOfPerforated is not null ? body.TopOfPerforated : null,
                BaseOfPerforated = body.BaseOfPerforated is not null ? body.BaseOfPerforated : null,
                Reservoir = reservoir,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                AllocationReservoir = body.AllocationReservoir is not null ? body.AllocationReservoir : 1,
            };

            await _systemHistoryService
                .Create<Completion, CompletionHistoryDTO>(_tableName, user, completionId, completion);

            await _completionRepository.AddAsync(completion);

            await _completionRepository.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CreateUpdateCompletionDTO>(completion);

            return completionDTO;
        }
        public async Task<CreateUpdateCompletionDTO> CreateDoubleCompletion(CreateDoubleCompletionViewModel body, User user)
        {

            var well = await _wellRepository.GetWithFieldAsync(body.WellId);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (well.IsActive is false && well.StatusOperator is false)
                throw new ConflictException(ErrorMessages.Inactive<Well>());

            if (well.Completions is not null && well.Completions.Count == 2)
                throw new ConflictException("Este poço já possui duas completações.");

            var reservoir = await _reservoirRepository.GetWithZoneFieldAsync(body.ReservoirId);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (reservoir.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Reservoir>());

            if (reservoir.Zone?.Field?.Id != well.Field?.Id)
                throw new ConflictException(ErrorMessages.DifferentFieldsCompletion());

            var completion = await _completionRepository
                .GetExistingCompletionAsync(well.Id, reservoir.Id);

            if (completion is not null)
                throw new ConflictException(ErrorMessages.WellAndReservoirAlreadyCompletion());

            if (body.AllocationReservoir is null || body.AllocationReservoirUpdate is null)
                throw new NotFoundException("Dados de alocações incompletos.");

            //VERIFICAR SOMA
            var result = body.AllocationReservoir + body.AllocationReservoirUpdate;
            if (result != 1)
                throw new ConflictException("Soma das alocações deve representar 1");

            //VERIFICAR SE OUTRRA COMPLETACAO EXISTE
            var completionUpdate = await _completionRepository.GetByIdAsync(body.CompletionUpdateId) ?? throw new NotFoundException("Completação não encontrada");

            //UPDATE COMPLETACAO 1
            var beforeChangesCompletion = _mapper.Map<CompletionHistoryDTO>(completionUpdate);
            var bodyUpdated = new UpdateCompletionViewModel
            {
                AllocationReservoir = body.AllocationReservoirUpdate
            };

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(completionUpdate, bodyUpdated);

            _completionRepository.Update(completionUpdate);
            if (updatedProperties.Any() is true)
            {
                await _systemHistoryService
                    .Update(_tableName, user, updatedProperties, completionUpdate.Id, completionUpdate, beforeChangesCompletion);
            }

            //CREATE COMPLETACAO 2
            var completionName = $"{well.Name}_{reservoir.Zone?.CodZone}";
            var completionId = Guid.NewGuid();

            completion = new Completion
            {
                Id = completionId,
                Name = completionName,
                Description = body.Description,
                TopOfPerforated = body.TopOfPerforated,
                BaseOfPerforated = body.BaseOfPerforated,
                User = user,
                Well = well,
                Reservoir = reservoir,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                AllocationReservoir = body.AllocationReservoir
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
                throw new NotFoundException(ErrorMessages.NotFound<Completion>());

            var completionDTO = _mapper.Map<Completion, CompletionWithWellAndReservoirDTO>(completion);
            return completionDTO;
        }

        public async Task<CompletionDTO> UpdateCompletion(UpdateCompletionViewModel body, Guid id, User user)
        {
            var completion = await _completionRepository.GetWithWellReservoirZoneAsync(id);

            if (completion is null)
                throw new NotFoundException(ErrorMessages.NotFound<Completion>());

            if (completion.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Completion>());

            var well = await _wellRepository.GetWithFieldAsync(body.WellId);

            if (well.Completions is not null && well.Completions.Count == 2)
            {
                var otherCompletion = well.Completions.Where(completion => completion.Id != id)
                .FirstOrDefault();

                if (otherCompletion is not null)
                    if (body.AllocationReservoir + otherCompletion.AllocationReservoir != 1)
                        throw new ConflictException("A soma das alocações por reservatório deve ser 1.");
            }

            var reservoir = await _reservoirRepository.GetWithZoneFieldAsync(body.ReservoirId);

            var beforeChangesCompletion = _mapper.Map<CompletionHistoryDTO>(completion);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(completion, body);

            if (updatedProperties.Any() is false && (body?.WellId == completion.Well?.Id || body?.WellId is null) && (body?.ReservoirId == completion.Reservoir?.Id || body?.ReservoirId is null))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Completion>());

            if (body?.WellId is not null && completion.Well?.Id != body.WellId)
            {
                if (well is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Well>());

                if (reservoir is null && body?.ReservoirId is not null)
                    throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

                if (reservoir is not null && well.Field?.Id != reservoir.Zone?.Field?.Id)
                    throw new ConflictException(ErrorMessages.DifferentFieldsCompletion());

                completion.Name = $"{well.Name}_{completion.Reservoir?.Zone?.CodZone}";
                completion.Well = well;

                updatedProperties[nameof(CompletionHistoryDTO.wellId)] = completion.Well.Id;
                updatedProperties[nameof(CompletionHistoryDTO.name)] = completion.Name;

            }
            if (body?.ReservoirId is not null && completion.Reservoir?.Id != body.ReservoirId)
            {
                if (reservoir is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

                if (well is null && body.WellId is not null)
                    throw new NotFoundException(ErrorMessages.NotFound<Well>());

                if (well is not null && reservoir.Zone?.Field?.Id != well.Field?.Id)
                    throw new ConflictException(ErrorMessages.DifferentFieldsCompletion());

                completion.Name = $"{completion.Well?.Name}_{reservoir.Zone?.CodZone}";
                completion.Reservoir = reservoir;

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

        public async Task DeleteCompletion(Guid id, User user, string StatusDate)
        {
            DateTime date;
            if (StatusDate is null)
            {
                throw new ConflictException("Data da inativação não informada");
            }
            else
            {
                var checkDate = DateTime.TryParse(StatusDate, out DateTime day);
                if (checkDate is false)
                    throw new ConflictException("Data não é válida.");

                var dateToday = DateTime.UtcNow.AddHours(-3);
                if (dateToday < day)
                    throw new NotFoundException("Data fornecida é maior que a data atual.");

                date = day;
            }

            var completion = await _completionRepository.GetOnlyCompletion(id);

            if (completion is null)
                throw new NotFoundException(ErrorMessages.NotFound<Completion>());

            if (completion.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Completion>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
                InactivatedAt = date
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

            if (completion is null)
                throw new NotFoundException(ErrorMessages.NotFound<Completion>());

            if (completion.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Completion>());

            if (completion.Well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (completion.Well.IsActive is false && completion.Well.StatusOperator is false)
                throw new ConflictException(ErrorMessages.Inactive<Well>());

            if (completion.Reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (completion.Reservoir.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Reservoir>());

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
                throw new NotFoundException(ErrorMessages.NotFound<Completion>());

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
