namespace PRIO.Models
{
    public class Well : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string WellOperatorName { get; set; } = string.Empty;
        public string CodWellAnp { get; set; } = string.Empty;
        public string CategoryAnp { get; set; } = string.Empty;
        public string CategoryReclassificationAnp { get; set; } = string.Empty;
        public string CategoryOperator { get; set; } = string.Empty;
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
        public string CoordX { get; set; } = string.Empty;
        public string CoordY { get; set; } = string.Empty;
        public User? User { get; set; }
        public List<Completion>? Completions { get; set; }
        //public string RegisterNum { get; set; }
        //public string FieldName { get; set; }
        //public string CompanyCodOperator { get; set; }
        //public string Basin { get; set; }
        //public string State { get; set; }
        //public DateTime DrillingStartDate { get; set; }
        //public DateTime DrillingFinishDate { get; set; }
        //public DateTime CompletionDate { get; set; }
        //public string Location { get; set; }
        //public decimal SounderDepth { get; set; }
        //public string InstallationName { get; set; }
        //public string EnviromentProduction { get; set; }
        //public string Block { get; set; }
        //public string ClusterName { get; set; }
        //public string CodInstallation { get; set; }
        //public string ReservoirName { get; set; }
        //public string ProductionByReservoir { get; set; }  //?????
        //public string CompletionName { get; set; }

        //public string MD { get; set; }
        //public string TVD { get; set; }

        //public string FieldCod { get; set; }
        //public string CurrentSituation { get; set; }
    }
}
