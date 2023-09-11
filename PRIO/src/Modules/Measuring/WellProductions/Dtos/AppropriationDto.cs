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

        public decimal ProductionOilInWellM3 { get; set; }
        public decimal ProductionOilInWellBBL { get; set; }

        public decimal ProductionGasInWellM3 { get; set; }
        public decimal ProductionGasInWellSCF { get; set; }

        public decimal ProductionWaterInWellM3 { get; set; }
        public decimal ProductionWaterInWellBBL { get; set; }

        public decimal ProductionLostOilM3 { get; set; }
        public decimal ProductionLostOilBBL { get; set; }

        public decimal ProductionLostGasM3 { get; set; }
        public decimal ProductionLostGasSCF { get; set; }

        public decimal ProductionLostWaterM3 { get; set; }
        public decimal ProductionLostWaterBBL { get; set; }

        public decimal EfficienceLoss { get; set; }
        public decimal ProportionalDay { get; set; }
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

        public decimal GasLossInFieldM3 { get; set; }
        public decimal WaterLossInFieldM3 { get; set; }
        public decimal OilLossInFieldM3 { get; set; }

        public decimal GasLossInFieldSCF { get; set; }
        public decimal WaterLossInFieldBBL { get; set; }
        public decimal OilLossInFieldBBL { get; set; }

        public List<WellProductionDto> WellAppropriations { get; set; }
    }

}
