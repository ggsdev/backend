using PRIO.DTOS.InstallationDTOS;
using PRIO.Models.Installations;

namespace PRIO.DTOS.FieldDTOS
{
    public class FieldHistoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameOld { get; set; }
        public string? CodField { get; set; }
        public string? CodFieldOld { get; set; }
        public string? Basin { get; set; }
        public string? BasinOld { get; set; }
        public string? Location { get; set; }
        public string? LocationOld { get; set; }
        public string? State { get; set; }
        public string? StateOld { get; set; }
        public InstallationDTO? Installation { get; set; }
        public string? InstallationOld { get; set; }
        public string? Description { get; set; }
        public string? DescriptionOld { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsActiveOld { get; set; }
        public string? CreatedAt { get; set; }
        public string? Type { get; set; }
    }
}
