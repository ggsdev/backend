namespace PRIO.src.Modules.Measuring.WellEvents.Dtos
{
    public class ClosingEventDto
    {
        public List<UepDto> Ueps { get; set; }
    }
    public class UepDto
    {
        public Guid UepId { get; set; }
        public string? UepCod { get; set; }
        public string? UepName { get; set; }
        public List<InstallationWithFieldsOnlyDto> Installations { get; set; }

    }

    public class InstallationWithFieldsOnlyDto
    {
        public Guid InstallationId { get; set; }
        public string? Name { get; set; }
        public string? CodInstallationAnp { get; set; }
        public decimal? GasSafetyBurnVolume { get; set; }
        public List<FieldWithWellAndWellEventsDto> Fields { get; set; }
    }

    public class FieldWithWellAndWellEventsDto
    {
        public Guid? FieldId { get; set; }
        public string? Name { get; set; }
        public List<WellWithEventDto>? Wells { get; set; }
    }
}
