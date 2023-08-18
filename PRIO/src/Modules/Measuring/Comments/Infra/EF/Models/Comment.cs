using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Comments.Infra.EF.Models
{
    public class CommentInProduction : BaseModel
    {
        public string Text { get; set; } = string.Empty;
        public Production Production { get; set; }
        public User CommentedBy { get; set; }
    }
}
