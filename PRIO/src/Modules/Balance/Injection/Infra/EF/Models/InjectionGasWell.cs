﻿using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Models
{
    public class InjectionGasWell
    {
        public Guid Id { get; set; }
        public User CreatedBy { get; set; }
        public DateTime MeasurementAt { get; set; }
        public WellsValues WellValues { get; set; }
        public InjectionWaterGasField? InjectionWaterGasField { get; set; }
    }
}
