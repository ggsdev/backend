﻿using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Interfaces
{
    public interface IEquipmentRepository
    {
        Task AddAsync(MeasuringEquipment equipment);
        void Update(MeasuringEquipment equipment);
        void Delete(MeasuringEquipment equipment);
        void Restore(MeasuringEquipment equipment);
        Task<MeasuringEquipment?> GetByIdAsync(Guid? id);
        Task<MeasuringEquipment?> GetByTypeWithMeasuringPointAsync(Guid? id, string type);
        Task<MeasuringEquipment?> GetExistingEquipment(string? serieNumber, string? tagEquipment);
        Task<MeasuringEquipment?> GetByTagsSerialChannel(string? tagPoint, string? tagEquipment, string? serial, string channel);
        Task<List<MeasuringEquipment>> GetAsync();
        Task<MeasuringEquipment?> GetWithInstallationAsync(Guid? id);
        Task<MeasuringEquipment?> GetByTagMeasuringPoint(string tagPoint, string acronym);
        Task SaveChangesAsync();

    }
}
