using OfficeOpenXml;

namespace PRIO.src.Shared.Utils
{
    internal static class XlsUtils
    {
        internal static readonly string ClusterColumnName = "CLUSTER";
        internal static readonly string FieldColumnName = "FIELD";
        internal static readonly string InstallationCodColumnName = "INSTALAÇÃO [CÓDIGO]";
        internal static readonly string InstallationColumnName = "INSTALAÇÃO [NOME]";
        internal static readonly string InstallationCodUepColumnName = "INSTALAÇÃO DE PROCESSAMENTO [CÓDIGO]";
        internal static readonly string InstallationNameUepColumnName = "INSTALAÇÃO DE PROCESSAMENTO [NOME]";

        internal static readonly string FieldCodeColumnName = "FIELD_CODE";
        internal static readonly string ReservoirColumnName = "RESERVOIR";
        internal static readonly string ZoneCodeColumnName = "ZONE_CODE";
        internal static readonly string AllocationByReservoirColumnName = "ALLOCATION_BY_RESERVOIR (FRACTION)";

        internal static readonly string CompletionColumnName = "COMPLETION";
        internal static readonly string WellNameColumnName = "WELL_NAME_ANP";
        internal static readonly string WellNameOperatorColumnName = "WELL_NAME_OPERATOR";

        internal static readonly string WellCodeAnpColumnName = "WELL_CODE_ANP";
        internal static readonly string WellCategoryAnpColumnName = "CATEGORY_ANP";
        internal static readonly string WellCategoryReclassificationColumnName = "CATEGORY_RECLASSIFICATION_ANP";

        internal static readonly string WellCategoryOperatorColumnName = "CATEGORY_OPERATOR";
        internal static readonly string WellStatusOperatorColumnName = "STATUS_OPERATOR";
        internal static readonly string WellProfileColumnName = "WELL_PROFILE";
        internal static readonly string WellWaterDepthColumnName = "WATER_DEPTH (M)";
        internal static readonly string WellPerforationTopMdColumnName = "PERFORATION_TOP_MD (M)";
        internal static readonly string WellBottomPerforationColumnName = "BOTTOM_PERFORATION_MD (M)";

        internal static readonly string WellArtificialLiftColumnName = "ARTIFICIAL_LIFT";
        internal static readonly string WellLatitude4cColumnName = "LATITUDE_BASE_4C";
        internal static readonly string WellLongitude4cColumnName = "LONGITUDE_BASE_4C";
        internal static readonly string WellLatitudeDDColumnName = "LATITUDE_BASE_DD";
        internal static readonly string WellLongitudeDDColumnName = "LONGITUDE_BASE_DD";
        internal static readonly string WellDatumHorizontalColumnName = "DATUM_HORIZONTAL";
        internal static readonly string WellTypeCoordinateColumnName = "TIPO_DE_COORDENADA_DE_BASE";
        internal static readonly string WellCoordXColumnName = "COORD_X";
        internal static readonly string WellCoordYColumnName = "COORD_Y";

        public static Dictionary<string, int> GetColumnPositions(ExcelWorksheet worksheet)
        {
            var columnPositions = new Dictionary<string, int>();

            var columnCount = worksheet.Dimension.Columns;

            for (int column = 1; column <= columnCount; column++)
            {
                var cellValue = worksheet.Cells[1, column].Value?.ToString();
                if (cellValue is not null)
                {
                    cellValue = cellValue.ToUpper();
                    if (cellValue == ClusterColumnName ||
                        cellValue == FieldColumnName ||
                        cellValue == InstallationCodColumnName ||
                        cellValue == InstallationColumnName ||
                        cellValue == InstallationCodUepColumnName ||
                        cellValue == InstallationNameUepColumnName ||
                        cellValue == InstallationColumnName ||
                        cellValue == FieldCodeColumnName ||
                        cellValue == ReservoirColumnName ||
                        cellValue == ZoneCodeColumnName ||
                        cellValue == AllocationByReservoirColumnName ||
                        cellValue == CompletionColumnName ||
                        cellValue == WellNameColumnName ||
                        cellValue == WellNameOperatorColumnName ||
                        cellValue == WellCodeAnpColumnName ||
                        cellValue == WellCategoryAnpColumnName ||
                        cellValue == WellCategoryReclassificationColumnName ||
                        cellValue == WellCategoryOperatorColumnName ||
                        cellValue == WellStatusOperatorColumnName ||
                        cellValue == WellProfileColumnName ||
                        cellValue == WellWaterDepthColumnName ||
                        cellValue == WellPerforationTopMdColumnName ||
                        cellValue == WellBottomPerforationColumnName ||
                        cellValue == WellArtificialLiftColumnName ||
                        cellValue == WellLatitude4cColumnName ||
                        cellValue == WellLongitude4cColumnName ||
                        cellValue == WellLatitudeDDColumnName ||
                        cellValue == WellLongitudeDDColumnName ||
                        cellValue == WellDatumHorizontalColumnName ||
                        cellValue == WellTypeCoordinateColumnName ||
                        cellValue == WellCoordXColumnName ||
                        cellValue == WellCoordYColumnName)

                    {
                        columnPositions.Add(cellValue, column);
                    }
                }
            }

            return columnPositions;
        }


        public static List<string> ValidateColumns(ExcelWorksheet worksheet)
        {
            var errorMessages = new List<string>();

            var expectedColumns = new List<string>
                {
                    ClusterColumnName,
                    FieldColumnName,
                    InstallationCodColumnName,
                    InstallationColumnName,
                    InstallationCodUepColumnName,
                    InstallationNameUepColumnName,
                    FieldCodeColumnName,
                    ReservoirColumnName,
                    ZoneCodeColumnName,
                    AllocationByReservoirColumnName,
                    CompletionColumnName,
                    WellNameColumnName,
                    WellNameOperatorColumnName,
                    WellCodeAnpColumnName,
                    WellCategoryAnpColumnName,
                    WellCategoryReclassificationColumnName,
                    WellCategoryOperatorColumnName,
                    WellStatusOperatorColumnName,
                    WellProfileColumnName,
                    WellWaterDepthColumnName,
                    WellPerforationTopMdColumnName,
                    WellBottomPerforationColumnName,
                    WellArtificialLiftColumnName,
                    WellLatitude4cColumnName,
                    WellLongitude4cColumnName,
                    WellLatitudeDDColumnName,
                    WellLongitudeDDColumnName,
                    WellDatumHorizontalColumnName,
                    WellTypeCoordinateColumnName,
                    WellCoordXColumnName,
                WellCoordYColumnName
            };

            for (int column = 1; column <= worksheet.Dimension.Columns; column++)
            {
                var currentValue = worksheet.Cells[1, column].Value?.ToString()?.ToUpper();
                bool isValidColumn = false;
                foreach (var expectedValue in expectedColumns)
                {
                    if (currentValue?.ToUpper() == expectedValue.ToUpper())
                    {
                        isValidColumn = true;
                        break;
                    }
                }

                if (!isValidColumn)
                {
                    var expectedValue = expectedColumns[column - 1];
                    errorMessages.Add($"Valor inválido para coluna: na {column}ª posição, deveria ser: '{expectedValue}'");
                }
            }

            return errorMessages;
        }
    }
}