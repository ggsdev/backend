using PRIO.Models.BaseModels;
using PRIO.Models.Reservoirs;
using PRIO.Models.Users;
using PRIO.Models.Wells;

namespace PRIO.Models.Completions
{
    public class Completion : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string? CodCompletion { get; set; }
        public Reservoir Reservoir { get; set; }
        public Well Well { get; set; }
        public User? User { get; set; }
    }
}
