using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.ViewModels;
using PRIO.src.Modules.Measuring.Comments.Dtos;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Dtos;

namespace PRIO.src.Modules.Measuring.Productions.Dtos
{
    public class ProductionDto
    {
        public string InstallationName { get; set; }
        public string UepName { get; set; }
        public DailyProduction DailyProduction { get; set; }

        public List<MeasurementHistoryWithMeasurementsDto> Files { get; set; }

        public GasConsultingDto Gas { get; set; }
        public OilConsultingDto Oil { get; set; }
    }

    public class DailyProduction
    {
        public decimal TotalGasSCF { get; set; }
        public decimal TotalGasM3 { get; set; }

        public decimal TotalOilBBL { get; set; }
        public decimal TotalOilM3 { get; set; }
        public bool StatusGas { get; set; }
        public bool StatusOil { get; set; }
        public string StatusProduction { get; set; } = "aberto";
        public bool IsActive { get; set; }
    }

    public class GetAllProductionsDto
    {
        public Guid Id { get; set; }
        public bool IsCalculated { get; set; }
        public bool CanDetailGasBurned { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
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
        public decimal TotalGasSCF { get; set; }
        public decimal TotalGasM3 { get; set; }
    }

    public class WaterTotalDto
    {
        public decimal TotalWaterM3 { get; set; }
        public decimal TotalWaterBBL { get; set; }
    }
    public class FieldFRBodyService
    {
        public GasDto? Gas { get; set; }
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
        public FRViewModel FR { get; set; }

        public GasBurntDto GasBurnt { get; set; }
        public GasFuelDto GasFuel { get; set; }
        public GasExportedDto GasExported { get; set; }
        public GasImportedDto GasImported { get; set; }

        public DetailBurn DetailedBurnedGas { get; set; }
    }

    public class GasConsultingDtoFrsNull
    {
        public decimal TotalGasProduction { get; set; }
        public FRViewModelNull FR { get; set; }

        public GasBurntDto GasBurnt { get; set; }
        public GasFuelDto GasFuel { get; set; }
        public GasExportedDto GasExported { get; set; }
        public GasImportedDto GasImported { get; set; }

        public DetailBurn DetailedBurnedGas { get; set; }
    }

    public class OilConsultingDtoFrsNull
    {
        public decimal TotalOilProduction { get; set; }
        public List<LocalOilPointDto>? MeasuringPoints { get; set; }
        public FRViewModelNull FR { get; set; }

    }

    public class ProductionDtoWithNullableDecimals
    {
        public Guid ProductionId { get; set; }
        public string InstallationName { get; set; }
        public string UepName { get; set; }
        public bool IsCalculated { get; set; }
        public bool CanDetailGasBurned { get; set; }

        public DailyProduction DailyProduction { get; set; }

        public List<MeasurementHistoryWithMeasurementsDto> Files { get; set; }

        public WaterDto Water { get; set; }
        public GasConsultingDtoFrsNull Gas { get; set; }
        public OilConsultingDtoFrsNull Oil { get; set; }

        public CreateUpdateCommentDto Comment { get; set; }

        public AppropriationDto WellAppropriation { get; set; }
        public List<ClientInfo> MeasurementsNotFound { get; set; } = new();

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
        public FRViewModel FR { get; set; }

    }
}
