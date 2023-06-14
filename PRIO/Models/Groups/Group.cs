﻿using PRIO.Models.BaseModels;

namespace PRIO.Models.Groups
{
    public class Group : BaseModel
    {
        public string? Name { get; set; }
        public List<GroupPermissions>? GroupPermissions { get; set; }
    }
}
