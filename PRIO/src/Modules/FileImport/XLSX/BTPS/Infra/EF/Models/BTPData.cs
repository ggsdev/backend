﻿using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models
{
    public class BTPData : BaseModel
    {
        public string Filename { get; set; }
        public string Type { get; set; }
        public string BTPSheet { get; set; }
        public string WellName { get; set; }
        public string WellAlignmentData { get; set; }
        public string WellAlignmentHour { get; set; }
        public string PotencialLiquid { get; set; }
        public string MPointOil { get; set; }
        public string PotencialOil { get; set; }
        public string MPointGas { get; set; }
        public string PotencialGas { get; set; }
        public string MPointWater { get; set; }
        public string PotencialWater { get; set; }
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public string Duration { get; set; }
        public string BTPNumber { get; set; }
        public string RGO { get; set; }
        public string BSW { get; set; }
        public BTPBase64 BTPBase64 { get; set; }
        public Well? Well { get; set; }
    }
}