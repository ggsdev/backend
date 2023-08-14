namespace PRIO.src.Modules.FileImport.XML.Dtos
{
    public class DTOFiles
    {
        public List<_001DTO> _001File { get; set; } = new();
        public List<_002DTO> _002File { get; set; } = new();
        public List<_003DTO> _003File { get; set; } = new();

    }

    public class DTOFilesClient
    {

        public List<Client001DTO> _001File { get; set; } = new();
        public List<Client002DTO> _002File { get; set; } = new();
        public List<Client003DTO> _003File { get; set; } = new();
    }

    public class ImportXMLResponseDTO
    {
        public Guid Id { get; set; }
        public string UepCode { get; set; }
        public List<ClientInfo> Measurements { get; set; } = new();
        public MeasurementHistoryDto? FileInfo { get; set; }
    }

}
