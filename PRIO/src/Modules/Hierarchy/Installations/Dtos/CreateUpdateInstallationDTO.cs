using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.Hierarchy.Installations.Dtos
{
    public class CreateUpdateInstallationDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? CodInstallationUep { get; set; }
        public string? Description { get; set; }
        public UserDTO? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }

    }
}
