namespace PRIO.src.Modules.Measuring.WellProductions.Infra.Utils
{
    public static class WellProductionUtils
    {
        public static readonly string fluidOil = "oil";
        public static readonly string fluidGas = "gas";
        public static readonly string fluidWater = "water";

        public static decimal CalculateWellProductionAsPercentageOfField(decimal potencial, decimal totalPotencialField)
        {
            return potencial / totalPotencialField;
        }
        public static decimal CalculateWellProductionAsPercentageOfUEP(decimal potencial, decimal totalPotencialUEP)
        {
            return potencial / totalPotencialUEP;
        }
        public static decimal CalculateWellProductionAsPercentageOfInstallation(decimal potencial, decimal frFactor)
        {
            return potencial * frFactor;
        }

        public static decimal CalculateWellProduction(decimal productionInField, decimal potencial)
        {
            return productionInField * potencial;
        }
    }
}
