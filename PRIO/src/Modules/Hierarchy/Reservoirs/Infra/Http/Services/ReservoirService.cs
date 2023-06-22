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
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Infra.Http.Services
{

    public class ReservoirService
    {
        private readonly IMapper _mapper;
        private readonly ISystemHistoryRepository _systemHistoryRepository;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly IZoneRepository _zoneRepository;

        public ReservoirService(IMapper mapper, ISystemHistoryRepository systemHistoryRepository, IReservoirRepository reservoirRepository, IZoneRepository zoneRepository)
        {
            _mapper = mapper;
            _systemHistoryRepository = systemHistoryRepository;
            _reservoirRepository = reservoirRepository;
            _zoneRepository = zoneRepository;
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

            await _systemHistoryRepository.AddAsync(history);

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

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(reservoir, body);

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

            var firstHistory = await _systemHistoryRepository.GetFirst(id);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

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

            await _systemHistoryRepository.AddAsync(history);

            await _reservoirRepository.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);
            return reservoirDTO;
        }

        public async Task DeleteReservoir(Guid id, User user)
        {
            var reservoir = await _reservoirRepository.GetOnlyReservoirAsync(id);

            if (reservoir is null || reservoir.IsActive is false)
                throw new NotFoundException("Reservoir not found or inactive already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

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
            await _systemHistoryRepository.AddAsync(history);

            _reservoirRepository.Update(reservoir);
            await _reservoirRepository.SaveChangesAsync();
        }

        public async Task<CreateUpdateReservoirDTO> RestoreReservoir(Guid id, User user)
        {
            var reservoir = await _reservoirRepository.GetWithZoneAsync(id);

            if (reservoir is null || reservoir.IsActive is true)
                throw new NotFoundException("Reservoir not found or active already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

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

            await _systemHistoryRepository.AddAsync(history);

            _reservoirRepository.Update(reservoir);

            await _reservoirRepository.SaveChangesAsync();

            var reservoirDTO = _mapper.Map<Reservoir, CreateUpdateReservoirDTO>(reservoir);
            return reservoirDTO;
        }

        public async Task<List<SystemHistory>> GetReservoirHistory(Guid id)
        {
            var reservoirHistories = await _systemHistoryRepository.GetAll(id);

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
