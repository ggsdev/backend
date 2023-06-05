using PRIO.DTOS.ReservoirDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.DTOS.WellDTOS;

namespace PRIO.DTOS.CompletionDTOS
{
    public class CompletionHistoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? Description { get; set; }
        public string? DescriptionOld { get; set; }
        public string? CodCompletion { get; set; }
        public string? CodCompletionOld { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
        public ReservoirDTO? Reservoir { get; set; }
        public Guid? ReservoirOld { get; set; }
        public WellDTO? Well { get; set; }
        public Guid? WellOld { get; set; }
    }
}
