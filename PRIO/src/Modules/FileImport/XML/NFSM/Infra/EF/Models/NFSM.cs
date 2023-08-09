using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models
{
    public class NFSM : BaseModel
    {
        public string CodeFailure { get; set; }
        public DateTime DateOfOcurrence { get; set; }
        public string? DescriptionFailure { get; set; }
        public string Action { get; set; }
        public string Methodology { get; set; }
        public Installation Installation { get; set; }
        public List<Measurement> Measurements { get; set; }
        public List<NFSMsProductions> Productions { get; set; }
        public MeasuringPoint MeasuringPoint { get; set; }
    }

    public class NFSMsProductions : BaseModel
    {
        public NFSM NFSM { get; set; }
        public Production Production { get; set; }
        public DateTime MeasuredAt { get; set; }
        public decimal VolumeAfter { get; set; }
        public decimal VolumeBefore { get; set; }
    }
}
