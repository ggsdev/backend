﻿namespace PRIO.src.Modules.ControlAccess.Groups.Dtos
{
    public class GroupDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
