using PRIO.src.Modules.FileImport.XML.Dtos;

namespace PRIO.src.Modules.Measuring.Productions.Dtos
{
    public class ProductionDto
    {
        public DailyProduction DailyProduction { get; set; }

        public List<MeasurementHistoryDto> Files { get; set; }

        public GasConsultingDto Gas { get; set; }
        public OilConsultingDto Oil { get; set; }
    }

    public class DailyProduction
    {
        public decimal TotalGasBBL { get; set; }
        public decimal TotalGasM3 { get; set; }

        public decimal TotalOilBBL { get; set; }
        public decimal TotalOilM3 { get; set; }

        public bool Status { get; set; }
    }

    public class LocalGasPointDto
    {
        public string? LocalPoint { get; set; }
        public string? TagMeasuringPoint { get; set; }
        public DateTime? DateMeasuring { get; set; }
        public decimal? IndividualProduction { get; set; }

    }

    public class LocalOilPointDto
    {
        public string? LocalPoint { get; set; }
        public string? TagMeasuringPoint { get; set; }
        public DateTime? DateMeasuring { get; set; }
        public decimal? VolumeAfterBsw { get; set; }
        public decimal? VolumeBeforeBsw { get; set; }
        public decimal? Bsw { get; set; }
    }

    public class GasConsultingDto
    {
        public decimal TotalGasProduction { get; set; }
        public GasBurntDto GasBurnt { get; set; }
        public GasFuelDto GasFuel { get; set; }
        public GasExportedDto GasExported { get; set; }
        public GasImportedDto GasImported { get; set; }
    }

    public class GasBurntDto
    {
        public decimal TotalGasBurnt { get; set; }
        public List<LocalGasPointDto>? MeasuringPoints { get; set; }
    }
    public class GasFuelDto
    {
        public decimal TotalGasFuel { get; set; }
        public List<LocalGasPointDto>? MeasuringPoints { get; set; }
    }

    public class GasExportedDto
    {
        public decimal TotalGasExported { get; set; }
        public List<LocalGasPointDto>? MeasuringPoints { get; set; }
    }

    public class GasImportedDto
    {
        public decimal TotalGasImported { get; set; }
        public List<LocalGasPointDto>? MeasuringPoints { get; set; }
    }

    public class OilConsultingDto
    {
        public decimal TotalOilProduction { get; set; }
        public List<LocalOilPointDto>? MeasuringPoints { get; set; }

    }


}
