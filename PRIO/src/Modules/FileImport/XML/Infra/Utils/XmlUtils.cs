using System.Globalization;

namespace PRIO.src.Modules.FileImport.XML.Infra.Utils
{
    public class XmlUtils
    {
        internal static readonly string File039 = "039";
        internal static readonly string File001 = "001";
        internal static readonly string File002 = "002";
        internal static readonly string File003 = "003";


        internal static readonly string FileAcronym039 = "EFM";
        internal static readonly string FileAcronym001 = "PMO";
        internal static readonly string FileAcronym002 = "PMGL";
        internal static readonly string FileAcronym003 = "PMGD";

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
