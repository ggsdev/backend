﻿using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models
{
    public class BTP : BaseModel
    {
        public string Name { get; set; }
        public bool Mutable { get; set; }
        public string FileContent { get; set; }
        public string Type { get; set; }
        public string BTPSheet { get; set; }
        public string CellWellName { get; set; }
        public string CellWellAlignmentData { get; set; }
        public string CellWellAlignmentHour { get; set; }
        public string CellPotencialLiquid { get; set; }
        public string CellMPointOil { get; set; }
        public string CellPotencialOil { get; set; }
        public string CellMPointGas { get; set; }
        public string CellPotencialGas { get; set; }
        public string CellMPointWater { get; set; }
        public string CellPotencialWater { get; set; }
        public string CellInitialDate { get; set; }
        public string CellFinalDate { get; set; }
        public string CellDuration { get; set; }
        public string CellBTPNumber { get; set; }
        public string CellRGO { get; set; }
        public string CellBSW { get; set; }
        public List<InstallationBTP>? BTPs { get; set; }
    }
}
