using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.HierarchyDTOS.MeasuringEquipment;
using PRIO.Exceptions;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.ViewModels.MeasuringEquipment;

namespace PRIO.Services.HierarchyServices
{
    public class EquipmentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EquipmentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MeasuringEquipmentDTO> CreateEquipment(CreateEquipmentViewModel body, User user)
        {
            var fluidsAllowed = new List<string>
            {
                "gás","óleo","água"
            };

            if (body.Fluid is not null && !fluidsAllowed.Contains(body.Fluid.ToLower()))
                throw new BadRequestException("Fluids allowed are: gás, óleo,água");

            var installationInDatabase = await _context.Installations
                .FirstOrDefaultAsync(x => x.Id == body.InstallationId);

            if (installationInDatabase is null)
                throw new NotFoundException("Installation not found");

            var equipment = new MeasuringEquipment
            {
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
            await _context.MeasuringEquipments.AddAsync(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return equipmentDTO;
        }

        public async Task<List<MeasuringEquipmentDTO>> GetEquipments()
        {
            var equipments = await _context.MeasuringEquipments.ToListAsync();
            var equipmentsDTO = _mapper.Map<List<MeasuringEquipment>, List<MeasuringEquipmentDTO>>(equipments);
            return equipmentsDTO;
        }

        public async Task<MeasuringEquipmentDTO> GetEquipmentById(Guid id)
        {
            var equipment = await _context.MeasuringEquipments.FirstOrDefaultAsync(x => x.Id == id);
            if (equipment is null)
                throw new NotFoundException("Equipment not found");

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return equipmentDTO;
        }

        public async Task<MeasuringEquipmentDTO> UpdateEquipment(UpdateEquipmentViewModel body, Guid id, User user)
        {
            var equipment = _context.MeasuringEquipments.Include(x => x.Installation).FirstOrDefault(x => x.Id == id);
            if (equipment is null)
                throw new NotFoundException("Equipment not found");

            var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.Id == body.InstallationId);

            if (body.InstallationId is not null && installationInDatabase is null)
                throw new NotFoundException("Installation not found");

            equipment.TagEquipment = body.TagEquipment is not null ? body.TagEquipment : equipment.TagEquipment;
            equipment.TagMeasuringPoint = body.TagMeasuringPoint is not null ? body.TagMeasuringPoint : equipment.TagMeasuringPoint;
            equipment.SerieNumber = body.SerieNumber is not null ? body.SerieNumber : equipment.SerieNumber;
            equipment.Type = body.Type is not null ? body.Type : equipment.Type;
            equipment.TypeEquipment = body.TagEquipment is not null ? body.TagEquipment : equipment.TagEquipment;
            equipment.Model = body.Model is not null ? body.Model : equipment.Model;
            equipment.HasSeal = body.HasSeal is not null ? body.HasSeal : equipment.HasSeal;
            equipment.MVS = body.MVS is not null ? body.MVS : equipment.MVS;
            equipment.CommunicationProtocol = body.CommunicationProtocol is not null ? body.CommunicationProtocol : equipment.CommunicationProtocol;
            equipment.TypePoint = body.TypePoint is not null ? body.TypePoint : equipment.TypePoint;
            equipment.ChannelNumber = body.ChannelNumber is not null ? body.ChannelNumber : equipment.ChannelNumber;
            equipment.InOperation = body.InOperation is not null ? body.InOperation : equipment.InOperation;
            equipment.Fluid = body.Fluid is not null ? body.Fluid : equipment.Fluid;
            equipment.Installation = installationInDatabase is not null ? installationInDatabase : equipment.Installation;
            equipment.Description = body.Description is not null ? body.Description : equipment.Description;

            _context.MeasuringEquipments.Update(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);

            return equipmentDTO;
        }

        public async Task DeleteEquipment(Guid id, User user)
        {
            var equipment = _context.MeasuringEquipments.FirstOrDefault(x => x.Id == id);
            if (equipment is null || !equipment.IsActive)
                throw new NotFoundException("Equipment not found or inactive already");

            equipment.IsActive = false;
            equipment.DeletedAt = DateTime.UtcNow;

            _context.MeasuringEquipments.Update(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task<MeasuringEquipmentDTO> RestoreEquipment(Guid id, User user)
        {
            var equipment = _context.MeasuringEquipments.FirstOrDefault(x => x.Id == id);
            if (equipment is null || equipment.IsActive)
                throw new NotFoundException("Equipment not found or active already");

            equipment.IsActive = true;
            equipment.DeletedAt = null;

            _context.MeasuringEquipments.Update(equipment);
            await _context.SaveChangesAsync();

            var equipmentDTO = _mapper.Map<MeasuringEquipment, MeasuringEquipmentDTO>(equipment);
            return equipmentDTO;
        }
    }
}
