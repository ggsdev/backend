
using System.Xml.Serialization;

namespace PRIO.Models
{
    [XmlRoot("Fields")]
    public class Fields
    {
        [XmlElement("Field")]
        public List<Field> FieldsList { get; set; }

    }

    public class Field
    {
        public Guid Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("CodField")]
        public string CodField { get; set; }

        [XmlElement("Acronym")]
        public string Acronym { get; set; }

        [XmlElement("Basin")]
        public string Basin { get; set; }

        [XmlElement("State")]
        public string State { get; set; }

        [XmlElement("Situation")]
        public string Situation { get; set; }

        [XmlElement("WaterDepth")]
        public decimal WaterDepth { get; set; }

        [XmlElement("CorrectedArea")]
        public decimal CorrectedArea { get; set; }

        [XmlElement("MainFluid")]
        public string MainFluid { get; set; }

        [XmlElement("APIGradeOil")]
        public decimal APIGradeOil { get; set; }

        [XmlElement("CalorificPowerGas")]
        public decimal CalorificPowerGas { get; set; }

        [XmlElement("ContractNum")]
        public string ContractNum { get; set; }

        [XmlElement("ContractOperator")]
        public string ContractOperator { get; set; }

        [XmlElement("ContractType")]
        public string ContractType { get; set; }

        [XmlElement("ContractTypeDescription")]
        public string ContractTypeDescription { get; set; }

        [XmlElement("Round")]
        public string Round { get; set; }

        [XmlElement("RoundDescription")]
        public string RoundDescription { get; set; }

        [XmlElement("Original")]
        public string Original { get; set; }

        [XmlElement("Location")]
        public string Location { get; set; }

        [XmlElement("EnviromentDepth")]
        public string EnviromentDepth { get; set; }

        [XmlElement("DiscoveryDate")]
        public DateTime DiscoveryDate { get; set; }

        [XmlElement("ProductionBeginning")]
        public DateTime ProductionBeginning { get; set; }

        [XmlElement("Commerciality")]
        public DateTime Commerciality { get; set; }

        [XmlElement("ProductionFinishForecast")]
        public DateTime ProductionFinishForecast { get; set; }

        [XmlElement("ProductionFinishDate")]
        public DateTime ProductionFinishDate { get; set; }

        [XmlElement("QtdWells")]
        public int QtdWells { get; set; }

        [XmlElement("PreSaltWells")]
        public int PreSaltWells { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
        public DateTime? UpdatedAt { get; set; } = null;
        public bool IsActive { get; set; } = true;
        public Cluster Cluster { get; set; }
        public User? User { get; set; }
        public List<Installation>? Installations { get; set; }
    }
}
