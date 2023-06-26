﻿using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models
{
    public class FileType : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public List<Measurement> Measurements { get; set; }
    }
}
