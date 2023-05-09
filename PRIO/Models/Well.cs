using Microsoft.VisualBasic;

namespace Prio_BackEnd.Models
{
    public class Well
    {
        public Guid Id { get; set; }
        public string NameAnp { get; set; }
        public string RegisterNum { get; set; }
        public string WellOperatorName { get; set; }
        public string FieldName { get; set; }
        public string CompanyCodOperator { get; set; }
        public string Basin { get; set; }
        public string State { get; set; }
        public string Category { get; set; }
        public DateTime DrillingStartDate { get; set; }
        public DateTime DrillingFinishDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Location { get; set; }
        public decimal WaterDepth { get; set; }
        public decimal SounderDepth { get; set; }
        public string InstallationName { get; set; }
        public string EnviromentProduction { get; set; }
        public string Block { get; set; }
        public string ClusterName { get; set; }
        public string CodInstallation { get; set; }
        public string ReservoirName { get; set; }
        public string ProductionByReservoir { get; set; }  //?????
        public string CompletionName { get; set; }
        public string TopOfPerforated { get; set; }
        public string BaseOfPerforated { get; set; }
        public string Type { get; set; }
        public string MD { get; set; }
        public string TVD { get; set; }
        public string ArtificialElevation { get; set; }
        public string CoordX { get; set; }
        public string CoordY { get; set; }
        public string FieldCod { get; set; }
        public string CurrentSituation { get; set; }
        public string Latitude { get; set; }    
        public string Longitude { get; set; }
        public bool IsActive  { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User? User { get; set; }
        public Completion Completion { get; set; }
    }
}
