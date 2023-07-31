using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;

namespace PRIO.src.Modules.Hierarchy.Fields.Dtos
{
    public class FieldWithoutWellDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodField { get; set; }
        public string? Basin { get; set; }
        public string? State { get; set; }
        public string? Location { get; set; }
        public UserDTO? User { get; set; }
        public InstallationWithoutClusterDTO? Installation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
