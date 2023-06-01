using PRIO.Models;

namespace PRIO.DTOS
{
    public class InstallationDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CodInstallation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}
