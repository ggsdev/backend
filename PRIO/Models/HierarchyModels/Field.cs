using PRIO.Models.BaseModels;
using PRIO.Models.UserControlAccessModels;
namespace PRIO.Models.HierarchyModels
{
    public class Field : BaseModel
    {
        public string? Name { get; set; }
        public string? CodField { get; set; }
        public string? State { get; set; }
        public string? Basin { get; set; }
        public string? Location { get; set; }
        public User? User { get; set; }
        public Installation? Installation { get; set; }
        public List<Zone>? Zones { get; set; }
        public List<Well>? Wells { get; set; }
    }
}
