using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.FileImport.XML.Dtos
{
    public class MeasurementHistoryDto
    {
        public Guid ImportId { get; set; }
        public string? ImportedAt { get; set; }
        public UserDTO ImportedBy { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileContent { get; set; }
    }

    public class MeasurementHistoryWithMeasurementsDto
    {
        public Guid ImportId { get; set; }
        public string? ImportedAt { get; set; }
        public UserDTO ImportedBy { get; set; }
        public List<IClientInfo> Summary { get; set; } = new();
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileContent { get; set; }
    }
}
