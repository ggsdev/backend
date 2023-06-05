using PRIO.Models.BaseModels;
using PRIO.Models.Reservoirs;
using PRIO.Models.Users;
using PRIO.Models.Wells;

namespace PRIO.Models.Completions
{
    public class CompletionHistory : BaseHistoryModel
    {
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodCompletion { get; set; }
        public string? CodCompletionOld { get; set; }
        public Reservoir? Reservoir { get; set; }
        public Guid? ReservoirOld { get; set; }
        public Well? Well { get; set; }
        public Guid? WellOld { get; set; }
        public User? User { get; set; }
    }
}
