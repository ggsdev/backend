using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Dtos;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.ViewModels;
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
                throw new NotFoundException("Zone not found");

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
                throw new NotFoundException("Reservoir not found");

            var reservoirDTO = _mapper.Map<Reservoir, ReservoirDTO>(reservoir);

            return reservoirDTO;
        }

        public async Task<CreateUpdateReservoirDTO> UpdateReservoir(UpdateReservoirViewModel body, Guid id, User user)
        {
            var reservoir = await _reservoirRepository
                .GetWithZoneAsync(id);

            if (reservoir is null)
                throw new NotFoundException("Reservoir not found");

            var beforeChangesReservoir = _mapper.Map<ReservoirHistoryDTO>(reservoir);

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(reservoir, body);

            if (updatedProperties.Any() is false && reservoir.Zone?.Id == body.ZoneId)
                throw new BadRequestException("This reservoir already has these values, try to update to other values.");

            if (body?.ZoneId is not null)
            {
                var zoneInDatabase = await _zoneRepository.GetOnlyZone(body.ZoneId);

                if (zoneInDatabase is null)
                    throw new NotFoundException("Zone not found");

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

        public async Task DeleteReservoir(Guid id, User user)
        {
            var reservoir = await _reservoirRepository
                .GetWithZoneAsync(id);

            if (reservoir is null || reservoir.IsActive is false)
                throw new NotFoundException("Reservoir not found or inactive already");

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

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

            if (reservoir is null || reservoir.IsActive is true)
                throw new NotFoundException("Reservoir not found or active already");

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
                throw new NotFoundException("Reservoir not found");

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
