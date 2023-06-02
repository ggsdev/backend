using PRIO.Models.BaseModels;
using PRIO.Models.Installations;
using PRIO.Models.Users;

namespace PRIO.Models.Fields
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
        public Installation? Installation { get; set; }
        public Guid? InstallationOld { get; set; }
        public Field Field { get; set; }
        public User? User { get; set; }
    }
}
