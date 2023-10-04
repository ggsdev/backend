namespace PRIO.src.Modules.Hierarchy.Wells.Dtos
{
    public class WellWithManualConfigDTO
    {
        public Guid Id { get; set; }
        public string CodWell { get; set; }
        public string Name { get; set; }
        public string WellOperatorName { get; set; }
        public string CodWellAnp { get; set; }
        public bool IsActive { get; set; }
        public ManualConfigDTO ManualConfig { get; set; }
    }
}
