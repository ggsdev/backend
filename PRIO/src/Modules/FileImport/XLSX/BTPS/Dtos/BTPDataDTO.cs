using PRIO.src.Modules.Hierarchy.Wells.Dtos;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos
{
    public class BTPDataDTO
    {
        public string Id { get; set; }
        public string Filename { get; set; }
        public string BTPSheet { get; set; }
        public string Type { get; set; }
        public string BTPNumber { get; set; }
        public string PotencialLiquid { get; set; }
        public string PotencialLiquidPerHour { get; set; }
        public string PotencialOil { get; set; }
        public string PotencialOilPerHour { get; set; }
        public string PotencialGas { get; set; }
        public string PotencialGasPerHour { get; set; }
        public string PotencialWater { get; set; }
        public string PotencialWaterPerHour { get; set; }
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public string Duration { get; set; }
        public string MPointGas { get; set; }
        public string MPointOil { get; set; }
        public string MPointWater { get; set; }
        public string BSW { get; set; }
        public string RGO { get; set; }
        public string WellAlignmentData { get; set; }
        public string WellAlignmentHour { get; set; }
        public string WellName { get; set; }
        public string CreatedAt { get; set; }
        public string ApplicationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid { get; set; }
        public BTPBase64DTO BTPBase64 { get; set; }
        public WellWithoutFieldDTO Well { get; set; }
    }
}
