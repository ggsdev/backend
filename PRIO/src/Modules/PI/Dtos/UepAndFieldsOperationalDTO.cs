namespace PRIO.src.Modules.PI.Dtos
{
    public record UepAndFieldsOperationalDTO
    (
        Guid UepId,
        string UepName,
        List<InstallationWithFieldsOperationalDTO> Installations
    );

    public record InstallationWithFieldsOperationalDTO
    (
        Guid Id,
        string Name,
        string UepCod,
        string UepName,
        string CodInstallationAnp,
        decimal? GasSafetyBurnVolume,
        string? Description,
        string CreatedAt,
        DateTime UpdatedAt,
        bool IsActive,
        List<FieldWithOperationalData> Fields
    );

    public record FieldWithOperationalData(
        Guid Id,
        bool IsActive,
        string FieldName,
        string DateOperational
    );
}
