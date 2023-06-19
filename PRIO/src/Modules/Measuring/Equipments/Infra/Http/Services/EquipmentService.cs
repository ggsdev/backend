using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS.HierarchyDTOS.MeasuringEquipment;
using PRIO.DTOS.HistoryDTOS;
using PRIO.Exceptions;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.src.Shared.Utils;
using PRIO.ViewModels.HierarchyViewModels.MeasuringEquipment;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.Http.Services
{
    public class EquipmentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly List<string> _fluidsAllowed = new()
        {
                "gás","óleo","água"
            };

        public EquipmentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MeasuringEquipmentDTO> CreateEquipment(CreateEquipmentViewModel body, User user)
        {
            if (body.Fluid is not null && !_fluidsAllowed.Contains(body.Fluid.ToLower()))
                throw new BadRequestException("Fluids allowed are: gás, óleo, água");

            var installationInDatabase = await _context.Installations
                .FirstOrDefaultAsync(x => x.Id == body.InstallationId);

            if (installationInDatabase is null)
                throw new NotFoundException("Installation not found");

            var equipmentId = Guid.NewGuid();
            var equipment = new MeasuringEquipment
            {
                Id = equipmentId,
                TagEquipment = body.TagEquipment,
                TagMeasuringPoint = body.TagMeasuringPoint,
                SerieNumber = body.SerieNumber,
                Type = body.Type,
                TypeEquipment = body.TypeEquipment,
                Model = body.Model,
                HasSeal = body.HasSeal,
                MVS = body.MVS,
                CommunicationProtocol = body.CommunicationProtocol,
                TypePoint = body.TypePoint,
                ChannelNumber = body.ChannelNumber,
                InOperation = body.InOperation,
                Fluid = body.Fluid,
                Installation = installationInDatabase,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            var currentData = _mapper.Map<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(equipment);
            currentData.createdAt = DateTime.UtcNow;
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableEquipments,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = equipmentId,
                CurrentData = currentData,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.MeasuringEquipments.AddAsync(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return equipmentDTO;
        }

        public async Task<List<MeasuringEquipmentDTO>> GetEquipments()
        {
            var equipments = await _context.MeasuringEquipments
                .ToListAsync();

            var equipmentsDTO = _mapper.Map<List<MeasuringEquipment>, List<MeasuringEquipmentDTO>>(equipments);
            return equipmentsDTO;
        }

        public async Task<MeasuringEquipmentDTO> GetEquipmentById(Guid id)
        {
            var equipment = await _context.MeasuringEquipments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (equipment is null)
                throw new NotFoundException("Equipment not found");

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return equipmentDTO;
        }

        public async Task<MeasuringEquipmentDTO> UpdateEquipment(UpdateEquipmentViewModel body, Guid id, User user)
        {

            var equipment = _context.MeasuringEquipments
                .Include(x => x.Installation)
                .FirstOrDefault(x => x.Id == id);
            if (equipment is null)
                throw new NotFoundException("Equipment not found");

            if (body.Fluid is not null && !_fluidsAllowed.Contains(body.Fluid.ToLower()))
                throw new BadRequestException("Fluids allowed are: gás, óleo, água");

            var installationInDatabase = await _context.Installations
                .FirstOrDefaultAsync(x => x.Id == body.InstallationId);

            if (body.InstallationId is not null && installationInDatabase is null)
                throw new NotFoundException("Installation not found");

            var beforeChangesEquipment = _mapper.Map<MeasuringEquipmentHistoryDTO>(equipment);

            var updatedProperties = UpdateFields.CompareAndUpdateEquipment(equipment, body);

            if (updatedProperties.Any() is false && equipment.Installation?.Id == body.InstallationId)
                throw new BadRequestException("This equipment already has these values, try to update to other values.");

            if (installationInDatabase is not null)
            {
                equipment.Installation = installationInDatabase;
                updatedProperties[nameof(MeasuringEquipmentHistoryDTO.installationId)] = installationInDatabase.Id;
            }

            _context.MeasuringEquipments.Update(equipment);

            var firstHistory = await _context.SystemHistories
              .OrderBy(x => x.CreatedAt)
              .Where(x => x.TableItemId == id)
              .FirstOrDefaultAsync();

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(equipment);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableEquipments,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = equipment.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesEquipment,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return equipmentDTO;
        }

        public async Task DeleteEquipment(Guid id, User user)
        {
            var equipment = _context.MeasuringEquipments.FirstOrDefault(x => x.Id == id);
            if (equipment is null || equipment.IsActive is false)
                throw new NotFoundException("Equipment not found or inactive already");

            var lastHistory = await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == equipment.Id)
               .LastOrDefaultAsync();

            equipment.IsActive = false;
            equipment.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(equipment);
            currentData.updatedAt = (DateTime)equipment.DeletedAt;
            currentData.deletedAt = equipment.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableEquipments,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = equipment.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = equipment.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    equipment.IsActive,
                    equipment.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);
            _context.MeasuringEquipments.Update(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task<MeasuringEquipmentDTO> RestoreEquipment(Guid id, User user)
        {
            var equipment = _context.MeasuringEquipments.FirstOrDefault(x => x.Id == id);
            if (equipment is null || equipment.IsActive)
                throw new NotFoundException("Equipment not found or active already");

            var lastHistory = await _context.SystemHistories
              .Where(x => x.TableItemId == equipment.Id)
              .OrderBy(x => x.CreatedAt)
              .LastOrDefaultAsync();

            equipment.IsActive = true;
            equipment.DeletedAt = null;

            var currentData = _mapper.Map<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(equipment);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableEquipments,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = equipment.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = equipment.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    equipment.IsActive,
                    equipment.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

            _context.MeasuringEquipments.Update(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return equipmentDTO;
        }

        public async Task<List<SystemHistory>> GetEquipmentHistory(Guid id)
        {
            var equipmentHistories = await _context.SystemHistories
                  .Where(x => x.TableItemId == id)
                  .OrderByDescending(x => x.CreatedAt)
                  .ToListAsync();

            if (equipmentHistories is null)
                throw new NotFoundException("Measuring Equipment not found");

            foreach (var history in equipmentHistories)
            {
                history.PreviousData = history.PreviousData is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString())
                    : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()) : null;
            }

            return equipmentHistories;
        }
    }
}
