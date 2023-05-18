using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace PRIO.Files._039
{

    public class CustomDateTimeFormatProvider : IFormatProvider
    {
        private readonly CultureInfo _culture;

        public CustomDateTimeFormatProvider()
        {
            _culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            _culture.DateTimeFormat.ShortTimePattern = "HH:mm";
            _culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
        }

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(DateTimeFormatInfo) ? _culture.DateTimeFormat : null;
        }
    }
}
public class Functions
{
    private static List<string>? _result = new();

    public static List<string>? CheckFormat(string xmlFilePath, string xsdFilePath)
    {
        _result = new();
        var schema = new XmlSchemaSet();

        schema.Add("", xsdFilePath);

        XmlReader reader = XmlReader.Create(xmlFilePath);
        XDocument doc = XDocument.Load(reader);
        doc.Validate(schema, ValidationEventHandler);

        reader.Close();

        return _result;
    }

    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {

        if (e.Severity == XmlSeverityType.Error)
        {
            _result.Add(e.Message);
            Console.WriteLine(e.Message);
        }

    }

}

