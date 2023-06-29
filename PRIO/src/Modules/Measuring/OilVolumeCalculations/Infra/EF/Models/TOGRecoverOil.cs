﻿using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models
{
    public class TOGRecoveredOil
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public OilVolumeCalculation? OilVolumeCalculation { get; set; }
        public MeasuringPoint? MeasuringPoint { get; set; }
    }
}
