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
        public static decimal CalculateWellProductionAsPercentageOfInstallation(decimal potencial, decimal frFactor, decimal bsw, string fluidType)
        {
            switch (fluidType.ToLower())
            {
                case "oil":
                    return potencial * frFactor * ((100 - bsw) / 100);
                case "water":
                    return potencial * frFactor * (bsw / 100);
                case "gas":
                    return potencial * frFactor;

                default:
                    return 0;
            }
        }

        public static decimal CalculateWellProduction(decimal productionInField, decimal frFactor, decimal bsw, decimal potencial, string fluidType)
        {
            switch (fluidType.ToLower())
            {
                case "oil":
                    return productionInField * ((100 - bsw) / 100) * potencial;

                case "gas":
                    return productionInField * potencial;

                case "water":
                    return productionInField * (bsw / 100) * potencial;

                default:
                    return 0;

            }
        }
    }
}
