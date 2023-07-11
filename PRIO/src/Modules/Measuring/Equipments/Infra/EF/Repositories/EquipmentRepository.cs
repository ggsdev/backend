using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataContext _context;
        public EquipmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MeasuringEquipment?> GetByIdAsync(Guid? id)
        {
            return await _context.MeasuringEquipments
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(MeasuringEquipment installation)
        {
            await _context.MeasuringEquipments.AddAsync(installation);
        }

        public async Task<MeasuringEquipment?> GetExistingEquipment(string? serieNumber, string? tagEquipment)
        {
            return await _context.MeasuringEquipments
               .FirstOrDefaultAsync(x => x.SerieNumber == serieNumber || x.TagEquipment == tagEquipment);
        }
        public async Task<List<MeasuringEquipment>> GetAsync()
        {
            return await _context.MeasuringEquipments
                .Include(x => x.MeasuringPoint)
                .ToListAsync();
        }
        public async Task<MeasuringEquipment?> GetWithInstallationAsync(Guid? id)
        {
            return await _context.MeasuringEquipments
                .Include(x => x.MeasuringPoint)
                .ThenInclude(x => x.Installation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<MeasuringEquipment?> GetByTypeWithMeasuringPointAsync(Guid? id, string type)
        {
            return await _context.MeasuringEquipments
                .Include(x => x.MeasuringPoint)
                .FirstOrDefaultAsync(x => x.MeasuringPoint.Id == id && x.Type == type && x.InOperation == true);
        }

        public async Task<MeasuringEquipment?> GetByTagsSerialChannel(string? tagPoint, string? tagEquipment, string? serial, string channel)
        {
            return await _context.MeasuringEquipments
                .FirstOrDefaultAsync(x => x.TagMeasuringPoint == tagPoint && x.TagEquipment == tagEquipment && x.SerieNumber == serial && x.ChannelNumber == channel);
        }

        public async Task<MeasuringEquipment?> GetByTagMeasuringPoint(string tagPoint, string acronym)
        {
            switch (acronym)
            {
                case "039":
                    return await _context.MeasuringEquipments
                     .FirstOrDefaultAsync(x => x.TagMeasuringPoint == tagPoint);

                case "001":
                    return await _context.MeasuringEquipments
                        .FirstOrDefaultAsync(x => x.TagMeasuringPoint == tagPoint);

                case "002":
                    return await _context.MeasuringEquipments
                        .FirstOrDefaultAsync(x => x.TagMeasuringPoint == tagPoint);

                case "003":
                    return await _context.MeasuringEquipments
                        .FirstOrDefaultAsync(x => x.TagMeasuringPoint == tagPoint);

                default:
                    throw new BadRequestException("Acronym values are: 001, 002, 003, 039");
            }
        }

        public void Update(MeasuringEquipment installation)
        {
            _context.MeasuringEquipments.Update(installation);
        }

        public void Delete(MeasuringEquipment installation)
        {
            _context.MeasuringEquipments.Update(installation);
        }

        public void Restore(MeasuringEquipment installation)
        {
            _context.MeasuringEquipments.Update(installation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
