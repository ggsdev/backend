
namespace Prio_BackEnd.Models
{

    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CodField { get; set; }
        public string Acronym { get; set; }
        public string Basin { get; set; }
        public string State { get; set; }
        public string Situation { get; set; }
        public decimal WaterDepth { get; set; }
        public decimal CorrectedArea { get; set; }
        public string MainFluid { get; set; }
        public decimal APIGradeOil { get; set; }
        public decimal CalorificPowerGas { get; set; }
        public string ContractNum { get; set; }
        public string ContractOperator { get; set; }
        public string ContractType { get; set; }
        public string ContractTypeDescription { get; set; }
        public string Round { get; set; }
        public string RoundDescription { get; set; }
        public string Original { get; set; }
        public string Location { get; set; }
        public string EnviromentDepth { get; set; }
        public DateTime DiscoveryDate { get; set; }
        public DateTime ProductionBeginning { get; set; }
        public DateTime Commerciality { get; set; }
        public DateTime ProductionFinishForecast { get; set; }
        public DateTime ProductionFinishDate { get; set; }
        public int QtdWells { get; set; }
        public int PreSaltWells { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public Cluster Cluster { get; set; }
        public User? User { get; set; }
        public List<Installation> Installations { get; set; }
    }
}
