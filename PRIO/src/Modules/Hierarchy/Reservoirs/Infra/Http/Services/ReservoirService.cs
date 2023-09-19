using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Infra.Http.Services
{

    public class ReservoirService
    {
        private readonly IMapper _mapper;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly IZoneRepository _zoneRepository;
        private readonly SystemHistoryService _systemHistoryService;

        public ReservoirService(IMapper mapper, IReservoirRepository reservoirRepository, IZoneRepository zoneRepository, SystemHistoryService systemHistoryService)
        {
            _mapper = mapper;
            _reservoirRepository = reservoirRepository;
            _zoneRepository = zoneRepository;
            _systemHistoryService = systemHistoryService;
        }

        public async Task<CreateUpdateReservoirDTO> CreateReservoir(CreateReservoirViewModel body, User user)
        {

            var zoneInDatabase = await _zoneRepository.GetOnlyZone(body.ZoneId);

            if (zoneInDatabase is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (zoneInDatabase.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Zone>());

            var reservoirSameName = await _reservoirRepository.GetByNameAsync(body.Name);
            if (reservoirSameName is not null)
                throw new ConflictException($"Já existe um reservatório com o nome: {body.Name}.");

            var reservoirId = Guid.NewGuid();

            var reservoir = new Reservoir
            {
                Id = reservoirId,
                Name = body.Name,
                Description = body.Description,
                Zone = zoneInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _reservoirRepository.AddAsync(reservoir);

            await _systemHistoryService
                .Create<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, reservoirId, reservoir);

            await _reservoirRepository.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);

            return reservoirDTO;
        }

        public async Task<List<ReservoirDTO>> GetReservoirs()
        {
            var reservoirs = await _reservoirRepository.GetAsync();

            var reservoirsDTO = _mapper.Map<List<Reservoir>, List<ReservoirDTO>>(reservoirs);
            return reservoirsDTO;
        }

        public async Task<ReservoirDTO> GetReservoirById(Guid id)
        {
            var reservoir = await _reservoirRepository.GetByIdAsync(id);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return reservoirDTO;
        }

        public async Task<CreateUpdateReservoirDTO> UpdateReservoir(UpdateReservoirViewModel body, Guid id, User user)
        {
            var reservoir = await _reservoirRepository
                .GetByIdWithCompletionsAsync(id);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (reservoir.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Reservoir>());


            var beforeChangesReservoir = _mapper.Map<ReservoirHistoryDTO>(reservoir);

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(reservoir, body);

            if (updatedProperties.Any() is false && (body.ZoneId is null || body.ZoneId == reservoir.Zone?.Id))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Zone>());

            if (body.ZoneId is not null && reservoir.Zone?.Id != body.ZoneId)
            {
                var zoneInDatabase = await _zoneRepository.GetOnlyZone(body.ZoneId);

                if (zoneInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Zone>());

                reservoir.Zone = zoneInDatabase;
                updatedProperties[nameof(ReservoirHistoryDTO.zoneId)] = zoneInDatabase.Id;
            }

            _reservoirRepository.Update(reservoir);

            await _systemHistoryService
                .Update(HistoryColumns.TableReservoirs, user, updatedProperties, reservoir.Id, reservoir, beforeChangesReservoir);

            await _reservoirRepository.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);
            return reservoirDTO;
        }

        public async Task DeleteReservoir(Guid id, User user, string StatusDate)
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

                var dateToday = DateTime.UtcNow.AddHours(-3).Date;
                if (dateToday <= day)
                    throw new NotFoundException("Data fornecida é maior que a data atual.");

                date = day;
            }

            var reservoir = await _reservoirRepository
                .GetReservoirAndChildren(id);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (reservoir.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Reservoir>());

            var propertiesUpdated = new
            {
                IsActive = false,
                InactivatedAt = date,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
            };

            if (reservoir.Completions is not null)
                foreach (var completion in reservoir.Completions)
                {
                    if (completion.IsActive is true)
                    {
                        var completionPropertiesToUpdate = new
                        {
                            IsActive = false,
                            InactivatedAt = date,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                        };

                        var completionUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableReservoirs, user, completionUpdatedProperties, completion.Id, completion);

                        _reservoirRepository.Delete(reservoir);
                    }
                }
            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(reservoir, propertiesUpdated);

            await _systemHistoryService
                .Delete<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, updatedProperties, reservoir.Id, reservoir);

            _reservoirRepository.Update(reservoir);

            await _reservoirRepository.SaveChangesAsync();
        }

        public async Task<CreateUpdateReservoirDTO> RestoreReservoir(Guid id, User user)
        {
            var reservoir = await _reservoirRepository.GetWithZoneAsync(id);

            if (reservoir is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            if (reservoir.IsActive is true)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Reservoir>());

            if (reservoir.Zone is null)
                throw new NotFoundException(ErrorMessages.NotFound<Zone>());

            if (reservoir.Zone.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Zone>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(reservoir, propertiesUpdated);

            await _systemHistoryService
                .Restore<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, updatedProperties, reservoir.Id, reservoir);

            _reservoirRepository.Update(reservoir);

            await _reservoirRepository.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);
            return reservoirDTO;
        }

        public async Task<List<SystemHistory>> GetReservoirHistory(Guid id)
        {
            var reservoirHistories = await _systemHistoryService.GetAll(id);

            if (reservoirHistories is null)
                throw new NotFoundException(ErrorMessages.NotFound<Reservoir>());

            foreach (var history in reservoirHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return reservoirHistories;
        }
    }
}
