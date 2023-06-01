using OfficeOpenXml;

namespace PRIO.Utils
{
    internal static class XlsUtils
    {
        public const string ClusterColumnName = "CLUSTER";
        public const string FieldColumnName = "FIELD";
        public const string InstallationColumnName = "PLATFORM";
        public const string FieldCodeColumnName = "FIELD_CODE";
        public const string ReservoirColumnName = "RESERVOIR";
        public const string ZoneCodeColumnName = "ZONE_CODE";
        public const string CompletionColumnName = "COMPLETION";
        public const string WellNameColumnName = "WELL_NAME_ANP";
        public const string WellNameOperatorColumnName = "WELL_NAME_OPERATOR";
        public const string WellCodeAnpColumnName = "WELL_CODE_ANP";
        public const string WellCategoryAnpColumnName = "CATEGORY_ANP";
        public const string WellCategoryReclassificationColumnName = "CATEGORY_RECLASSIFICATION_ANP";
        public const string WellCategoryOperatorColumnName = "CATEGORY_OPERATOR";
        public const string WellStatusOperatorColumnName = "STATUS_OPERATOR";
        public const string WellProfileColumnName = "WELL_PROFILE";
        public const string WellWaterDepthColumnName = "WATER_DEPTH (M)";
        public const string WellPerforationTopMdColumnName = "PERFORATION_TOP_MD (M)";
        public const string WellBottomPerforationColumnName = "BOTTOM_PERFORATION_MD (M)";
        public const string WellArtificialLiftColumnName = "ARTIFICIAL_LIFT";
        public const string WellLatitude4cColumnName = "LATITUDE_BASE_4C";
        public const string WellLongitude4cColumnName = "LONGITUDE_BASE_4C";
        public const string WellLatitudeDDColumnName = "LATITUDE_BASE_DD";
        public const string WellLongitudeDDColumnName = "LONGITUDE_BASE_DD";
        public const string WellDatumHorizontalColumnName = "DATUM_HORIZONTAL";
        public const string WellTypeCoordinateColumnName = "TIPO_DE_COORDENADA_DE_BASE";
        public const string WellCoordXColumnName = "COORD_X";
        public const string WellCoordYColumnName = "COORD_Y";

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
                        cellValue == InstallationColumnName ||
                        cellValue == FieldCodeColumnName ||
                        cellValue == ReservoirColumnName ||
                        cellValue == ZoneCodeColumnName ||
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
    }
}
