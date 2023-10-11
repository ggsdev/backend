namespace PRIO.src.Modules.Balance.Balance.Dtos
{
    public record BalanceDto
    (
        Guid UepBalanceId,
        string Uep,
        bool Status,
        string DateBalance,
        decimal TotalWaterProduced,
        decimal TotalWaterInjected,
        decimal TotalWaterInjectedRS,
        decimal TotalWaterDisposal,
        decimal TotalWaterReceived,
        decimal TotalWaterCaptured,
        decimal DischargedSurface,
        decimal TotalWaterTransferred
    );
}
