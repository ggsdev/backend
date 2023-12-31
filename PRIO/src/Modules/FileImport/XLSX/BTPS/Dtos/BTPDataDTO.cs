﻿using PRIO.src.Modules.Hierarchy.Wells.Dtos;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos
{
    public class BTPDataDTO
    {
        public string? Id { get; set; }
        public Guid BTPId { get; set; }
        public string Filename { get; set; }
        public string BTPSheet { get; set; }
        public string Type { get; set; }
        public string BTPNumber { get; set; }
        public decimal PotencialLiquid { get; set; }
        public decimal? PotencialLiquidPerHour { get; set; }
        public decimal PotencialOil { get; set; }
        public decimal? PotencialOilPerHour { get; set; }
        public decimal PotencialGas { get; set; }
        public decimal? PotencialGasPerHour { get; set; }
        public decimal PotencialWater { get; set; }
        public decimal? PotencialWaterPerHour { get; set; }
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public string Duration { get; set; }
        public string MPointGas { get; set; }
        public string MPointOil { get; set; }
        public string MPointWater { get; set; }
        public decimal BSW { get; set; }
        public decimal RGO { get; set; }
        public string WellAlignmentData { get; set; }
        public string WellAlignmentHour { get; set; }
        public string WellName { get; set; }
        public string ApplicationDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsValid { get; set; }
        public WellWithoutFieldDTO? Well { get; set; }
        public BTPBase64RelationWellTestDTO? BTPBase64 { get; set; }
    }
}
