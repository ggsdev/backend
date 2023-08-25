using PRIO.src.Modules.FileImport.XML.Dtos;

namespace PRIO.src.Modules.Measuring.WellProductions.Dtos
{
    public class AppropriationDto
    {
        public Guid ProductionId { get; set; }
        public List<FieldProductionDto> FieldProductions { get; set; }
        public WaterDto WaterProduction { get; set; }
    }
    public class WellProductionDto
    {
        public Guid WellProductionId { get; set; }
        public string WellName { get; set; }
        public string Downtime { get; set; }

        public decimal ProductionGasInWellM3 { get; set; }
        public decimal ProductionWaterInWellM3 { get; set; }
        public decimal ProductionOilInWellM3 { get; set; }

        public decimal ProductionGasInWellSCF { get; set; }
        public decimal ProductionWaterInWellBBL { get; set; }
        public decimal ProductionOilInWellBBL { get; set; }
    }

    public class FieldProductionDto
    {
        public Guid FieldProductionId { get; set; }
        public string FieldName { get; set; }
        public decimal GasProductionInFieldM3 { get; set; }
        public decimal WaterProductionInFieldM3 { get; set; }
        public decimal OilProductionInFieldM3 { get; set; }

        public decimal GasProductionInFieldSCF { get; set; }
        public decimal WaterProductionInFieldBBL { get; set; }
        public decimal OilProductionInFieldBBL { get; set; }
        public List<WellProductionDto> WellAppropriations { get; set; }
    }

}
