﻿namespace PRIO.src.Modules.ControlAccess.Groups.Dtos
{
    public class GroupWithGroupPermissionDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public List<GroupPermissionParentDTO>? GroupPermissions { get; set; }
    }
}
