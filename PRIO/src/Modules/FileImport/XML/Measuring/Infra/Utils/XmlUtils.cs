using System.Globalization;

namespace PRIO.src.Modules.FileImport.XML.Measuring.Infra.Utils
{
    public class XmlUtils
    {
        public static readonly string File039 = "039";
        public static readonly string File001 = "001";
        public static readonly string File002 = "002";
        public static readonly string File003 = "003";


        public static readonly string FileAcronym039 = "EFM";
        public static readonly string FileAcronym001 = "PMO";
        public static readonly string FileAcronym002 = "PMGL";
        public static readonly string FileAcronym003 = "PMGD";

        public static decimal? DecimalParser(string? valueToBeParsed, List<string> errors, string? nameElement)
        {
            if (string.IsNullOrEmpty(valueToBeParsed))
                return null;

            if (valueToBeParsed is not null && decimal.TryParse(valueToBeParsed.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                return result;
            else
                errors.Add($"Formato dados inválidos, valor: {valueToBeParsed}, tag: {nameElement} formato númerico aceitável: '00000,00000'");

            return 0;
        }

        public static DateTime? DateTimeParser(string? valueToBeParsed, List<string> errors, string? nameElement)
        {
            if (string.IsNullOrEmpty(valueToBeParsed))
                return null;

            if (DateTime.TryParseExact(valueToBeParsed, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;
            else
                errors.Add($"Formato dados inválidos, valor: {valueToBeParsed}, tag: {nameElement}, formato de data aceitável: 'dd/MM/yyyy HH:mm:ss'");

            return null;
        }

        public static DateTime? DateTimeWithoutTimeParser(string? valueToBeParsed, List<string> errors, string? nameElement)
        {
            if (string.IsNullOrEmpty(valueToBeParsed))
                return null;

            if (DateTime.TryParseExact(valueToBeParsed, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;
            else
                errors.Add($"Formato dados inválidos, valor: {valueToBeParsed}, tag: {nameElement}, formato de data aceitável: 'dd/MM/yyyy'");

            return null;
        }

        public static short? ShortParser(string? valueToBeParsed, List<string> errors, string? nameElement)
        {
            if (string.IsNullOrEmpty(valueToBeParsed))
                return null;

            if (short.TryParse(valueToBeParsed, out var result))
                return result;
            else
                errors.Add($"Formato dados inválidos, valor: {valueToBeParsed}, tag: {nameElement}, formato númerico aceitável: '0'");

            return null;
        }
    }
}
