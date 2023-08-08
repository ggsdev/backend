using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Dtos
{
    public class ProductionDto
    {
        public DailyProduction DailyProduction { get; set; }

        public List<MeasurementHistoryWithMeasurementsDto> Files { get; set; }

        public GasConsultingDto Gas { get; set; }
        public OilConsultingDto Oil { get; set; }
    }

    public class DailyProduction
    {
        public decimal TotalGasBBL { get; set; }
        public decimal TotalGasM3 { get; set; }

        public decimal TotalOilBBL { get; set; }
        public decimal TotalOilM3 { get; set; }
        public bool StatusGas { get; set; }
        public bool StatusOil { get; set; }
        public bool StatusProduction { get; set; }
    }

    public class GetAllProductionsDto
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string UepName { get; set; }
        public string DateProduction { get; set; }
        public List<ProductionFilesDto> Files { get; set; }
        public OilTotalDto? Oil { get; set; }
        public GasTotalDto? Gas { get; set; }
        public WaterTotalDto? Water { get; set; }
    }

    public class ProductionFilesDto
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime? ImportedAt { get; set; }

    }

    public class ProductionFilesDtoWithBase64
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime? ImportedAt { get; set; }
        public string Base64 { get; set; }

    }

    public class OilTotalDto
    {
        public decimal TotalOilM3 { get; set; }
        public decimal TotalOilBBL { get; set; }
    }

    public class GasTotalDto
    {
        public decimal TotalGasBBL { get; set; }
        public decimal TotalGasM3 { get; set; }
    }

    public class WaterTotalDto
    {
        public decimal TotalWaterM3 { get; set; }
        public decimal TotalWaterSFC { get; set; }
    }
    public class FieldFRBodyService
    {
        public GasDto? Gas { get; set; }
        public bool BothGas { get; set; }
        public OilDto? Oil { get; set; }
        public Production Production { get; set; }
        public Guid InstallationId { get; set; }
    }


    public class LocalGasPointDto
    {
        public string? LocalPoint { get; set; }
        public string? TagMeasuringPoint { get; set; }
        public string? DateMeasuring { get; set; }
        public decimal? IndividualProduction { get; set; }
        //public Guid ImportId { get; set; }
        //public string FileName { get; set; }
        //public string FileType { get; set; }

    }

    public class LocalOilPointDto
    {
        public string? LocalPoint { get; set; }
        public string? TagMeasuringPoint { get; set; }
        public string? DateMeasuring { get; set; }
        public decimal? VolumeAfterBsw { get; set; }
        public decimal? VolumeBeforeBsw { get; set; }
        public decimal? Bsw { get; set; }
        //public Guid ImportId { get; set; }
        //public string FileName { get; set; }
        //public string FileType { get; set; }
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
