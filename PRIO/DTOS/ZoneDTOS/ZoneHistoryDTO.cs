using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.ZoneDTOS
{
    public class ZoneHistoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodInstallation { get; set; }
        public string? CodInstallationOld { get; set; }
        public string? FieldName { get; set; }
        public string? FieldNameOld { get; set; }
        public Guid? FieldId { get; set; }
        public Guid? FieldOldId { get; set; }
        public UserDTO? User { get; set; }
        public string? Description { get; set; }
        public string? DescriptionOld { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsActiveOld { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TypeOperation { get; set; }
    }
}
