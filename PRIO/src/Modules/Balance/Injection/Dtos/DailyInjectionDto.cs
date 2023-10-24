namespace PRIO.src.Modules.Balance.Injection.Dtos
{
    //public class DailyInjectionDto
    //{
    //    public bool Status { get; set; }
    //    public string DateInjection { get; set; }
    //    public double TotalGasLift { get; set; }
    //    public double TotalWaterInjected { get; set; }

    //    public List<GasLiftInjectedDto> GasLiftFields { get; set; } = new();
    //    public List<WaterInjectedDto> WaterInjectedFields { get; set; } = new();
    //}

    public class GasLiftInjectedDto
    {
        public string Field { get; set; }

        public List<ElementGasDto> Parameters { get; set; } = new();
    }

    public class ElementGasDto
    {
        public string Parameter { get; set; }


        public List<GasValuesDto> Values { get; set; } = new();

    }

    public class WellWaterInjectedDto
    {
        public Guid WellInjectionId { get; set; }
        public string WellName { get; set; }
        public double? VolumePI { get; set; }
        public double VolumeAssigned { get; set; }
        public string Tag { get; set; }
        public string DateRead { get; set; }
    }

    public class DailyInjectionDto
    {
        public bool Status { get; set; }
        public string DateInjection { get; set; }
        public double TotalGasLift { get; set; }
        public double TotalWaterInjected { get; set; }

        public List<GasLiftInjectedDto> GasLiftFields { get; set; } = new();
        public List<WaterInjectedDto> WaterInjectedFields { get; set; } = new();
    }

    public class GasValuesDto
    {
        public Guid WellInjectionId { get; set; }
        public string WellName { get; set; } = null!;
        public string TagFlow { get; set; } = null!;
        public double? FlowVolumePI { get; set; }
        public double FlowVolumeAssigned { get; set; }
        public Guid GFLInjectionId { get; set; }
        public string TagGFL { get; set; } = null!;

        public double? VolumeGFLPI { get; set; }
        public double? VolumeGFLAssigned { get; set; }
        public string DateRead { get; set; } = null!;
    }

    public class ElementWaterDto
    {
        public string Parameter { get; set; }
        public List<WellValuesDto> Values { get; set; } = new();
    }

    public class WellValuesDto
    {
        public Guid WellInjectionId { get; set; }
        public string WellName { get; set; } = null!;
        public string TagFlow { get; set; } = null!;
        public double? FlowVolumePI { get; set; }
        public double FlowVolumeAssigned { get; set; }
        public Guid WFLInjectionId { get; set; }
        public string TagWFL { get; set; } = null!;

        public double? VolumeWFLPI { get; set; }
        public double? VolumeWFLAssigned { get; set; }
        public string DateRead { get; set; } = null!;
    }

    public class WaterInjectedDto
    {
        public string Field { get; set; }
        public double FIRS { get; set; }
        public List<ElementWaterDto> Parameters { get; set; } = new();
    }

}
