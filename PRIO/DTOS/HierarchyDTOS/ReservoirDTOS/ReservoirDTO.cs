﻿using PRIO.DTOS.HierarchyDTOS.ZoneDTOS;
using PRIO.DTOS.UserDTOS;

namespace PRIO.DTOS.HierarchyDTOS.ReservoirDTOS
{
    public class ReservoirDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? CodReservoir { get; set; }
        public string? Description { get; set; }
        public UserDTO? User { get; set; }
        public ZoneDTO? Zone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
