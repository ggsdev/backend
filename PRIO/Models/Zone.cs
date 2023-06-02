using PRIO.Models.Fields;

namespace PRIO.Models
{
    public class Zone : BaseModel
    {
        public string CodZone { get; set; } = string.Empty;
        public User? User { get; set; }
        public Field Field { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
    }
}
