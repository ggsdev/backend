using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models
{
    public class FieldFR : BaseModel
    {
        public decimal? FRGas { get; set; }
        public decimal? FROil { get; set; }
        public Field Field { get; set; }
        public Production DailyProduction { get; set; }
        public decimal TotalProductionInField { get; set; }
        public decimal GasProductionInField { get; set; }
        public decimal OilProductionInField { get; set; }

        public decimal ProductionInFieldAsPercentage { get; set; }
        public bool IsManually { get; set; }
    }

    public class FRFieldsViewModelNull
    {
        public Guid FieldId { get; set; }
        public decimal? FluidFr { get; set; }
        public string FieldName { get; set; }
        public decimal ProductionInField { get; set; }
    }

    public class FRViewModelNull
    {
        public List<FRFieldsViewModelNull> Fields { get; set; }

    }
}
