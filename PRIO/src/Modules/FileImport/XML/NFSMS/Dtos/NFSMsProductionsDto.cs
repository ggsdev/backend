namespace PRIO.src.Modules.FileImport.XML.NFSMS.Dtos
{
    public class NFSMsProductionsDto
    {
        public Guid Id { get; set; }
        public string MeasuredAt { get; set; }
        public decimal? VolumeAfter { get; set; }
        public decimal? VolumeBefore { get; set; }
    }
}
