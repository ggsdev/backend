using OfficeOpenXml;

namespace PRIO.src.Modules.FileExport.XLSX.Utils
{
    public class ExportUtils
    {
        internal static readonly string DateEventColumnName = "DATA DO EVENTO";
        internal static readonly string EventStartColumnName = "HORA INÍCIO DO EVENTO";
        internal static readonly string EventEndColumnName = "HORA FIM DO EVENTO";

        internal static readonly string ProcessingInstallationColumnName = "INSTALAÇÃO DE PROCESSAMENTO";
        internal static readonly string InstallationCodColumnName = "INSTALAÇÃO";
        internal static readonly string FieldNameColumnName = "CAMPO";

        internal static readonly string WellANPNameColumnName = "POÇO - NOME ANP";
        internal static readonly string OperatorWellNameColumnName = "POÇO - NOME OPERADORA";
        internal static readonly string OperatorCategoryColumnName = "CATEGORIA OPERADOR";
        internal static readonly string EventTypeColumnName = "TIPO DE EVENTO";
        internal static readonly string EventIDColumnName = "ID DO EVENTO";
        internal static readonly string RelatedEventColumnName = "EVENTO RELACIONADO";
        internal static readonly string RelatedSystemColumnName = "SISTEMA RELACIONADO";

        internal static readonly string ANPStatusColumnName = "STATUS ANP";
        internal static readonly string ANPStateColumnName = "ESTADO ANP";
        internal static readonly string StopPeriodColumnName = "PERÍODO PARADO (H)";

        internal static readonly string ProportionalDayColumnName = "DIA PROPORCIONAL";
        internal static readonly string EventJustificationColumnName = "JUSTIFICATIVA DO EVENTO";
        internal static readonly string OilLossColumnName = "PERDA ÓLEO (SBBL)";
        internal static readonly string WaterLossColumnName = "PERDA ÁGUA (SBBL)";
        internal static readonly string GasLossColumnName = "PERDA GÁS (SCF)";

        public static Dictionary<string, int> GetColumnPositions(ExcelWorksheet worksheet)
        {
            var columnPositions = new Dictionary<string, int>();

            var columnCount = worksheet.Dimension.Columns;

            for (int column = 1; column <= columnCount; column++)
            {
                var cellValue = worksheet.Cells[1, column].Value?.ToString();
                if (cellValue is not null)
                {
                    cellValue = cellValue.ToUpper().Trim();

                    if (cellValue == DateEventColumnName ||
                        cellValue == EventStartColumnName ||
                        cellValue == EventEndColumnName ||
                        cellValue == ProcessingInstallationColumnName ||
                        cellValue == InstallationCodColumnName ||
                        cellValue == FieldNameColumnName ||
                        cellValue == InstallationCodColumnName ||
                        cellValue == FieldNameColumnName ||
                        cellValue == WellANPNameColumnName ||
                        cellValue == OperatorWellNameColumnName ||
                        cellValue == OperatorCategoryColumnName ||
                        cellValue == EventTypeColumnName ||
                        cellValue == EventIDColumnName ||
                        cellValue == RelatedEventColumnName ||
                        cellValue == RelatedSystemColumnName ||
                        cellValue == ANPStatusColumnName ||
                        cellValue == ANPStateColumnName ||
                        cellValue == StopPeriodColumnName ||
                        cellValue == ProportionalDayColumnName ||
                        cellValue == EventJustificationColumnName ||
                        cellValue == OilLossColumnName ||
                        cellValue == WaterLossColumnName ||
                        cellValue == GasLossColumnName)
                    {
                        columnPositions.Add(cellValue, column);
                    }
                }
            }

            return columnPositions;
        }
    }
}
