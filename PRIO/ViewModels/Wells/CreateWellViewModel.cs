﻿using System.ComponentModel.DataAnnotations;

namespace PRIO.ViewModels.Zones
{
    public class CreateWellViewModel
    {
        [Required(ErrorMessage = "Well code is required")]
        public string CodWell { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string WellOperatorName { get; set; } = string.Empty;
        public string CodWellAnp { get; set; } = string.Empty;
        public string CategoryAnp { get; set; } = string.Empty;
        public string CategoryReclassificationAnp { get; set; } = string.Empty;
        public string CategoryOperator { get; set; } = string.Empty;
        public bool StatusOperator { get; set; } 
        public string Type { get; set; } = string.Empty;
        public double WaterDepth { get; set; } 
        public double TopOfPerforated { get; set; }
        public double BaseOfPerforated { get; set; } 
        public string ArtificialLift { get; set; } = string.Empty;
        public string Latitude4C { get; set; } = string.Empty;
        public string Longitude4C { get; set; } = string.Empty;
        public string LatitudeDD { get; set; } = string.Empty;
        public string LongitudeDD { get; set; } = string.Empty;
        public string DatumHorizontal { get; set; } = string.Empty;
        public string TypeBaseCoordinate { get; set; } = string.Empty;
        public string CoordX { get; set; } = string.Empty;
        public string CoordY { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid FieldId { get; set; } 
    }
}