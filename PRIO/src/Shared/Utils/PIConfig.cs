namespace PRIO.src.Shared.Utils
{
    public static class PIConfig
    {
        public readonly static string _user = "svc-pi-frade";
        public readonly static string _password = "S6_5q2C?=%ff";

        public readonly static string _flow = "Vazão";
        public readonly static string _wfl1 = "Vazão da WFL1";
        public readonly static string _gasLiftFlow = "Vazão de Gas Lift";
        public readonly static string _waterFlow = "Vazão de injeção de água";
        public readonly static string _gfl1 = "Vazão da GFL1";
        public readonly static string _gfl4 = "Vazão da GFL4";
        public readonly static string _gfl6 = "Vazão da GFL6";

        public readonly static string _pressure = "Pressão";
        public readonly static string _intakeEsp = "Pressão Intake ESP";
        public readonly static string _pdg2 = "Pressão PDG 2";
        public readonly static string _pdg1 = "Pressão PDG 1";
        public readonly static string _whPressure = "Pressão WH";

        public readonly static List<string> _pressureValues = new()
        {
            _intakeEsp, _pdg2, _pdg1,_whPressure
        };

        public readonly static List<string> _flowValues = new()
        {
            _wfl1, _gasLiftFlow, _waterFlow, _gfl1, _gfl4, _gfl6,
        };
    };
}
