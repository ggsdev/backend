namespace PRIO.Models
{
    public class Installation : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string CodInstallation { get; set; } = string.Empty;
        public Field Field { get; set; }
        public User? User { get; set; }
        public List<Reservoir>? Reservoirs { get; set; }
        public List<MeasuringEquipment>? MeasuringEquipments { get; set; }

        //public string Acronym { get; set; }
        //public string Operator { get; set; }
        //public string Owner { get; set; }
        //public string Type { get; set; }
        //public string Environment { get; set; }
        //public decimal WaterDepth { get; set; }
        //public string State { get; set; }
        //public string City { get; set; }
        //public string FieldService { get; set; }
        //public string Latitude { get; set; }
        //public string Longitude { get; set; }
        //public decimal GasProcessing { get; set; }
        //public decimal OilProcessing { get; set; }
        //public DateTime BeginningValidity { get; set; }
        //public DateTime InclusionDate { get; set; }
        //public int PsmQty { get; set; }
        //public bool Situation { get; set; }

    }
}
