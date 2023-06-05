using PRIO.DTOS.UserDTOS;
using PRIO.Models;

namespace PRIO.DTOS.WellDTOS
{
    public class WellDTO
    {
        public Guid Id { get; set; }
        public string CodWell { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string WellOperatorName { get; set; } = string.Empty;
        public string CodWellAnp { get; set; } = string.Empty;
        public string CategoryAnp { get; set; } = string.Empty;
        public string CategoryReclassificationAnp { get; set; } = string.Empty;
        public string CategoryOperator { get; set; } = string.Empty;
        public bool StatusOperator { get; set; }
        public string Type { get; set; } = string.Empty;
        public double WaterDepth { get; set; }
        public double TopOfPerforated { get; set; }
        public double BaseOfPerforated { get; set; }
        public string ArtificialLift { get; set; } = string.Empty;
        public string Latitude4C { get; set; } = string.Empty;
        public string Longitude4C { get; set; } = string.Empty;
        public string LatitudeDD { get; set; } = string.Empty;
        public string LongitudeDD { get; set; } = string.Empty;
        public string DatumHorizontal { get; set; } = string.Empty;
        public string TypeBaseCoordinate { get; set; } = string.Empty;
        public string TypeOperation { get; set; } = string.Empty;
        public string CoordX { get; set; } = string.Empty;
        public string CoordY { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDTO? User { get; set; }
    }
}
