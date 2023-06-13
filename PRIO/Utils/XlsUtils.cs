using OfficeOpenXml;

namespace PRIO.Utils
{
    internal static class XlsUtils
    {
        internal static readonly string ClusterColumnName = "CLUSTER";
        internal static readonly string InstallationCodUepColumnName = "UEP_CODE";
        internal static readonly string FieldColumnName = "FIELD";
        internal static readonly string InstallationColumnName = "PLATFORM";
        internal static readonly string FieldCodeColumnName = "FIELD_CODE";
        internal static readonly string ReservoirColumnName = "RESERVOIR";
        internal static readonly string ZoneCodeColumnName = "ZONE_CODE";
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
                        cellValue == InstallationCodUepColumnName ||
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
