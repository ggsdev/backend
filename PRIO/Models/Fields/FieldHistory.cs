using System.Text.Json.Serialization;
using PRIO.Models.BaseModels;
using PRIO.Models.Users;

namespace PRIO.Models.Clusters
{
    public class FieldHistory : BaseHistoryModel
    {
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodField { get; set; }
        public string? CodFieldOld { get; set; }
        public string? State { get; set; }
        public string? StateOld { get; set; }
        public string? Basin { get; set; }
        public string? BasinOld { get; set; }
        public string? Location { get; set; }
        public string? LocationOld { get; set; }
        public Cluster Cluster { get; set; }
        public Cluster ClusterOld { get; set; }
        public User? User { get; set; }
    }
}
