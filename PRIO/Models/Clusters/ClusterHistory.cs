﻿using System.Text.Json.Serialization;
using PRIO.Models.BaseModels;
using PRIO.Models.Users;

namespace PRIO.Models.Clusters
{
    public class ClusterHistory : BaseHistoryModel
    {
        public string? Type { get; set; }
        public string? NameOld { get; set; }
        public string? Name { get; set; }
        public string? CodClusterOld { get; set; }
        public string? CodCluster { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public Cluster Cluster { get; set; }
    }
}
