namespace PRIO.TESTS.Files.XLS
{
    internal static class Mock
    {
        public static readonly string _clusterBravo = "BRAVO";
        public static readonly string _clusterForte = "FORTE";
        public static readonly string _clusterValente = "VALENTE";

        public static readonly string _clusterBravoUepCode = "10905";
        public static readonly string _clusterForteUepCode = "10305";
        public static readonly string _clusterValenteUepCode = "10398";

        public static readonly string _installationForte = "FPSO FORTE";
        public static readonly string _installationBravo = "FPSO BRAVO";
        public static readonly string _installationFrade = "FPSO FRADE";
        public static readonly string _installationPolvoA = "POLVO A";

        public static readonly string _fieldAlbacoraLeste = "ALBACORA LESTE";
        public static readonly string _fieldFrade = "FRADE";
        public static readonly string _fieldPolvo = "POLVO";
        public static readonly string _fieldTubaraoMartelo = "TUBARÃO MARTELO";
        public static readonly string _fieldWahoo = "WAHOO";

        public static readonly string _zoneCodAB1407890 = "7890";
        public static readonly string _zoneCod12081 = "12081";
        public static readonly string _zoneCod12434 = "12434";

        public static readonly string _zoneCodN570U9123 = "9123";
        public static readonly string _zoneCodTuroniano11775 = "11775";
        public static readonly string _zoneCod545D9121 = "9121";

        public static readonly string _reservoirAB140 = "AB140";
        public static readonly string _reservoirN570U = "N570U";
        public static readonly string _reservoirTuroniano = "TURONIANO";
        public static readonly string _reservoirN545D = "N545D";
        public static readonly string _reservoirN560D = "N560D";
        public static readonly string _reservoirQuissama_Tmbt = "QUISSAMÃ_TMBT";
        public static readonly string _reservoirQuissama_Pol = "QUISSAMÃ_POL";

        public static readonly WellMock _well74281026496 = new()
        {
            Name = "7-TBMT-4HP-RJS",
            WellOperatorName = "TBMT-04",
            CodWellAnp = "74281026496",
            CategoryAnp = "Desenvolvimento",
            CategoryReclassificationAnp = "PRODUTOR COMERCIAL DE PETRÓLEO E GÁS NATURAL",
            CategoryOperator = "Produtor",
            StatusOperator = true,
            Type = "Horizontal",
            WaterDepth = 106.4,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = "ESP",
            Latitude4C = "-23:06:30,559",
            Longitude4C = "-41:05:12,869",
            LatitudeDD = "-23,1084886111",
            LongitudeDD = "-41,0869080555",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-41,0869080555",
            CoordY = "-23,1084886111"
        };
        public static readonly WellMock _well74281026537 = new()
        {
            Name = "7-TBMT-6HP-RJS",
            WellOperatorName = "TBMT-06",
            CodWellAnp = "74281026537",
            CategoryAnp = "Desenvolvimento",
            CategoryReclassificationAnp = "PRODUTOR COMERCIAL DE PETRÓLEO E GÁS NATURAL",
            CategoryOperator = "Produtor",
            StatusOperator = true,
            Type = "Horizontal",
            WaterDepth = 106,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = "ESP",
            Latitude4C = "-23:06:47,515",
            Longitude4C = "-41:04:28,177",
            LatitudeDD = "-23,1131986111",
            LongitudeDD = "-41,0744936111",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-41,0744936111",
            CoordY = "-23,1131986111"
        };
        public static readonly WellMock _well74281026753 = new()
        {
            Name = "7-TBMT-12H-RJS",
            WellOperatorName = "7TBMT12HRJS",
            CodWellAnp = "74281026753",
            CategoryAnp = "Desenvolvimento",
            CategoryReclassificationAnp = null,
            CategoryOperator = "Produtor",
            StatusOperator = false,
            Type = "Horizontal",
            WaterDepth = 107,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = null,
            Latitude4C = "-23:06:30,461",
            Longitude4C = "-41:05:12,663",
            LatitudeDD = "-23,1084613888",
            LongitudeDD = "-41,0868508333",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-41,0868508333",
            CoordY = "-23,1084613888"
        };
        public static readonly WellMock _well74281029234 = new()
        {
            Name = "7-POL-38HP-RJS",
            WellOperatorName = "POL-038-Za",
            CodWellAnp = "74281029234",
            CategoryAnp = "Desenvolvimento",
            CategoryReclassificationAnp = "INDEFINIDO",
            CategoryOperator = "Produtor",
            StatusOperator = true,
            Type = "Horizontal",
            WaterDepth = 103,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = "ESP",
            Latitude4C = "-23:05:01,586",
            Longitude4C = "-40:59:43,430",
            LatitudeDD = "-23,0837738888",
            LongitudeDD = "-40,9953972222",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-40,9953972222",
            CoordY = "-23,0837738888"
        };
        public static readonly WellMock _well74281029209 = new()
        {
            Name = "7-POL-36HP-RJS",
            WellOperatorName = "POL-036-Pj",
            CodWellAnp = "74281029209",
            CategoryAnp = "Desenvolvimento",
            CategoryReclassificationAnp = "INDEFINIDO",
            CategoryOperator = "Produtor",
            StatusOperator = true,
            Type = "Horizontal",
            WaterDepth = 103,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = "ESP",
            Latitude4C = "-23:05:01,883",
            Longitude4C = "-40:59:43,332",
            LatitudeDD = "-23,0838563888",
            LongitudeDD = "-40,99537",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-40,99537",
            CoordY = "-23,0838563888"
        };
        public static readonly WellMock _well74281029222 = new()
        {
            Name = "9-POL-37D-RJS",
            WellOperatorName = "POL-Z",
            CodWellAnp = "74281029222",
            CategoryAnp = "Especial",
            CategoryReclassificationAnp = "INDEFINIDO",
            CategoryOperator = null,
            StatusOperator = null,
            Type = "Direcional",
            WaterDepth = 103,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = null,
            Latitude4C = "-23:05:01,586",
            Longitude4C = "-40:59:43,430",
            LatitudeDD = "-23,0837738888",
            LongitudeDD = "-40,9953972222",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-40,9953972222",
            CoordY = "-23,0837738888"
        };
        public static readonly WellMock _well74281028110 = new()
        {
            Name = "7-ABL-87HP-RJS",
            WellOperatorName = "ABL-87HP",
            CodWellAnp = "74281028110",
            CategoryAnp = "Desenvolvimento",
            CategoryReclassificationAnp = "PRODUTOR COMERCIAL DE PETRÓLEO",
            CategoryOperator = "Produtor",
            StatusOperator = true,
            Type = "Horizontal",
            WaterDepth = 1095,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = "Gas-Lift",
            Latitude4C = "-22:06:26,560",
            Longitude4C = "-39:51:11,840",
            LatitudeDD = "-22,1073777777",
            LongitudeDD = "-39,8532888888",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-39,8532888888",
            CoordY = "-22,1073777777"
        };
        public static readonly WellMock _well74281029154 = new()
        {
            Name = "8-ABL-88H-RJS",
            WellOperatorName = "ABL-88H",
            CodWellAnp = "74281029154",
            CategoryAnp = "Injeção",
            CategoryReclassificationAnp = "ABANDONADO POR PERDA CIRCULAÇÃO",
            CategoryOperator = null,
            StatusOperator = null,
            Type = "Horizontal",
            WaterDepth = 1078,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = null,
            Latitude4C = "-22:03:22,709",
            Longitude4C = "-39:51:50,819",
            LatitudeDD = "-22,0563080555",
            LongitudeDD = "-39,8641163888",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-39,8641163888",
            CoordY = "-22,0563080555"
        };
        public static readonly WellMock _well74281029266 = new()
        {
            Name = "8-ABL-88HA-RJS",
            WellOperatorName = "ABL-88HA",
            CodWellAnp = "74281029266",
            CategoryAnp = "Injeção",
            CategoryReclassificationAnp = "INJEÇÃO DE ÁGUA",
            CategoryOperator = "Injetor de água",
            StatusOperator = true,
            Type = "Horizontal",
            WaterDepth = 1077,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = null,
            Latitude4C = "-22:03:22,709",
            Longitude4C = "-39:51:50,819",
            LatitudeDD = "-22,0563080555",
            LongitudeDD = "-39,8641163888",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-39,8641163888",
            CoordY = "-22,0563080555"
        };
        public static readonly WellMock _well742810163700 = new()
        {
            Name = "1-RJS-366-RJS",
            WellOperatorName = "1RJS 0366  RJ",
            CodWellAnp = "742810163700",
            CategoryAnp = "Pioneiro",
            CategoryReclassificationAnp = "DESCOBRIDOR DE CAMPO COM PETRÓLEO",
            CategoryOperator = null,
            StatusOperator = null,
            Type = "Vertical",
            WaterDepth = 1155.00,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = "Gás-Lift",
            Latitude4C = "-21:52:35,080",
            Longitude4C = "-39:50:32,182",
            LatitudeDD = "-21,8764111111",
            LongitudeDD = "-39,8422727777",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-39,8422727777",
            CoordY = "-21,8764111111"
        };
        public static readonly WellMock _well74281026747 = new()
        {
            Name = "7-TBMT-10H-RJS",
            WellOperatorName = "TBMT-10",
            CodWellAnp = "74281026747",
            CategoryAnp = "Desenvolvimento",
            CategoryReclassificationAnp = "PRODUTOR COMERCIAL DE PETRÓLEO E GÁS NATURAL",
            CategoryOperator = "Produtor",
            StatusOperator = true,
            Type = "Horizontal",
            WaterDepth = 107,
            TopOfPerforated = 0,
            BaseOfPerforated = 0,
            ArtificialLift = "ESP",
            Latitude4C = "-23:06:30,399",
            Longitude4C = "-41:05:12,593",
            LatitudeDD = "-23,1084441666",
            LongitudeDD = "-41,0868313888",
            DatumHorizontal = "SIRGAS2000",
            TypeBaseCoordinate = "Definitiva",
            CoordX = "-41,0868313888",
            CoordY = "-23,1084441666"
        };

        public static readonly string _well74281019769 = "74281019769";
        public static readonly string _well74281022570 = "74281022570";
        public static readonly string _well74281024180 = "74281024180";

        public static readonly string _completionWellCod74281029209 = "7-POL-36HP-RJS_11776";
        public static readonly string _completionWellCod74281026537 = "7-TBMT-6HP-RJS_12081";
        public static readonly string _completionWellCod74281026753NoReservoir = "7-TBMT-12H-RJS_";
        public static readonly string _completionWellCod74281029234 = "7-POL-38HP-RJS_11775";

        public static readonly string _completionWellCod74281019769 = "3-TXCO-3DA-RJS_";
        public static readonly string _completionWellCod74281022570 = "7-POL-5H-RJS_";
        public static readonly string _completionWellCod74281024180 = "7-FR-15HP-RJS_9122";

        //adicionar mais completações para teste name/wellcode
        //7-POL-17H-RJS_
        //8-POL-18HP-RJS_
        //74281023796
        //74281023817


        //refatorar para contar linhas do xls tirando duplicados
        public static readonly int _totalClusters = 3 * 2;
        public static readonly int _totalInstallations = 4 * 2;
        public static readonly int _totalReservoirs = 19 * 2;
        public static readonly int _totalZones = 16 * 2;
        public static readonly int _totalFields = 5 * 2;
        public static readonly int _totalWells = 265 * 2;
        public static readonly int _totalCompletions = 267 * 2;
    }

    public class WellMock
    {
        public string? Name { get; set; }
        public string? WellOperatorName { get; set; }
        public string? CodWellAnp { get; set; }
        public string? CodWell { get; set; }
        public string? CategoryAnp { get; set; }
        public string? CategoryReclassificationAnp { get; set; }
        public string? CategoryOperator { get; set; }
        public bool? StatusOperator { get; set; }
        public string? Type { get; set; }
        public double? WaterDepth { get; set; }
        public double? TopOfPerforated { get; set; }
        public double? BaseOfPerforated { get; set; }
        public string? ArtificialLift { get; set; }
        public string? Latitude4C { get; set; }
        public string? Longitude4C { get; set; }
        public string? LatitudeDD { get; set; }
        public string? LongitudeDD { get; set; }
        public string? DatumHorizontal { get; set; }
        public string? TypeBaseCoordinate { get; set; }
        public string? CoordX { get; set; }
        public string? CoordY { get; set; }
    }
}
