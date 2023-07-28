using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.FileImport.XML.Dtos
{
    public class MeasurementHistoryDto
    {
        public Guid? ImportId { get; set; }
        public string? ImportedAt { get; set; }
        public UserDTO ImportedBy { get; set; }
        public string FileContent { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
    }
}
