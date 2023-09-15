using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos
{
    public class BTPBase64DTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public UserDTO User { get; set; }
        public string FileContent { get; set; }
    }
}
