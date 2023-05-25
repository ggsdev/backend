namespace PRIO.Models
{

    public class Field : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string CodField { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Basin { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public Cluster Cluster { get; set; }
        public User? User { get; set; }
        public List<Installation>? Installations { get; set; }

        //public string Situation { get; set; }

        //public decimal WaterDepth { get; set; }

        //public decimal CorrectedArea { get; set; }

        //public string MainFluid { get; set; }

        //public decimal APIGradeOil { get; set; }

        //public decimal CalorificPowerGas { get; set; }

        //public string ContractNum { get; set; }

        //public string ContractOperator { get; set; }

        //public string ContractType { get; set; }

        //public string ContractTypeDescription { get; set; }

        //public string Round { get; set; }

        //public string RoundDescription { get; set; }

        //public string Original { get; set; }


        //public string EnviromentDepth { get; set; }

        //public DateTime DiscoveryDate { get; set; }

        //public DateTime ProductionBeginning { get; set; }

        //public DateTime Commerciality { get; set; }

        //public DateTime ProductionFinishForecast { get; set; }

        //public DateTime ProductionFinishDate { get; set; }

        //public int QtdWells { get; set; }

        //public int PreSaltWells { get; set; }
    }
}
