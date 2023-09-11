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
        public static decimal CalculateDowntimeInHours(string downtime)
        {
            // Analisar a string no formato "hh:mm:ss" em horas, minutos e segundos
            var partes = downtime.Split(':');
            if (partes.Length != 3)
            {
                throw new InvalidOperationException("Formato de Downtime inválido.");
            }

            int horas = int.Parse(partes[0]);
            int minutos = int.Parse(partes[1]);
            int segundos = int.Parse(partes[2]);

            // Calcular a fração de horas como decimal
            decimal fracaoDeHoras = horas + (minutos / 60.0m) + (segundos / 3600.0m);

            return fracaoDeHoras;
        }
    }
}
