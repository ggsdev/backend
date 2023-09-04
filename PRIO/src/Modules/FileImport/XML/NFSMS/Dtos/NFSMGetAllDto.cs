using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Dtos
{
    public class NFSMGetAllDto
    {
        public Guid Id { get; set; }
        public string CodeFailure { get; set; }
        public DateTime DateOfOcurrence { get; set; }
        public DateTime DetectionDate { get; set; }
        public DateTime ReturnDateDetected { get; set; }
        public string ResponsibleReport { get; set; }
        public string? DescriptionFailure { get; set; }
        public short? TypeOfFailure { get; set; }
        public string? TypeOfNotification { get; set; }
        public string Action { get; set; }
        public string Methodology { get; set; }
        public bool IsApplied { get; set; }
        public CreateUpdateInstallationDTO Installation { get; set; }
        public List<NFSMsProductionsDto>? MeasurementsFixed { get; set; }
        public List<BswFixedNfsm> BswsFixed { get; set; }
        public MeasuringPointWithoutInstallationDTO MeasuringPoint { get; set; }

        public FailureNotificationFilesDto File { get; set; }
    }

    public class FailureNotificationFilesDto
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime? ImportedAt { get; set; }

    }
}
