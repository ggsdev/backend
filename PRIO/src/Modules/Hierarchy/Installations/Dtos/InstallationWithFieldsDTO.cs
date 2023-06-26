using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Measuring.Equipments.Dtos;

namespace PRIO.src.Modules.Hierarchy.Installations.Dtos
{
    public class InstallationWithFieldsEquipmentsDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? UepCod { get; set; }
        public string? CodInstallation { get; set; }
        public double? GasSafetyBurnVolume { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
        public List<FieldWithoutInstallationDTO>? Fields { get; set; }
        public List<MeasuringEquipmentWithoutInstallationDTO>? MeasuringEquipments { get; set; }
    }
}
