﻿using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Dtos
{
    public class NFSMGetAllDto
    {
        public string CodeFailure { get; set; }
        public DateTime DateOfOcurrence { get; set; }
        public string? DescriptionFailure { get; set; }
        public short? TypeOfFailure { get; set; }
        public string Action { get; set; }
        public string Methodology { get; set; }
        public CreateUpdateInstallationDTO Installation { get; set; }
        public List<NFSMsProductionsDto>? MeasurementsFixed { get; set; }
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