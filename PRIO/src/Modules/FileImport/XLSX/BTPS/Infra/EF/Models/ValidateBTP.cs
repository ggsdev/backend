namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models
{
    public class ValidateBTP
    {
        public Guid Id { get; set; }
        public Guid WellId { get; set; }
        public Guid BTPId { get; set; }
        public Guid ContentId { get; set; }
        public Guid DataId { get; set; }
        public string Filename { get; set; }
        public bool IsValid { get; set; }
        public string ApplicationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string content { get; set; }
    }
}
