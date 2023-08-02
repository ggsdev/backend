﻿using PRIO.src.Modules.Measuring.MeasuringPoints.Dtos;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Dtos
{
    public class AssistanceGasDto
    {
        public Guid Id { get; set; }
        public string StaticLocalMeasuringPoint { get; set; } = string.Empty;
        public MeasuringPointWithoutInstallationDTO? MeasuringPoint { get; set; }
        public bool IsApplicable { get; set; }
    }
}