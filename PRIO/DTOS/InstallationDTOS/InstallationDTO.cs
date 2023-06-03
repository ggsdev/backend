using PRIO.DTOS.FieldDTOS;
using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.InstallationDTOS
{
    public class InstallationDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? CodInstallation { get; set; }
        public UserDTO? User { get; set; }
        public List<FieldDTO>? Fields { get; set; }
        public List<InstallationHistoryDTO>? InstallationHistories { get; set; }
    }
}
