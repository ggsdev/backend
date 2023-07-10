using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PRIO.src.Modules.FileImport.XML.FileContent
{
    public class Functions
    {
        private static List<string>? _result = new();

        public static List<string>? CheckFormat(string xmlFilePath, string xsdFilePath)
        {
            _result = new();
            var schema = new XmlSchemaSet();

            schema.Add("", xsdFilePath);

            var reader = XmlReader.Create(xmlFilePath);
            var doc = XDocument.Load(reader);
            doc.Validate(schema, ValidationEventHandler);

            reader.Close();
            return _result;
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {

            if (e.Severity == XmlSeverityType.Error)
            {
                _result?.Add(e.Message);
            }

        }

        public static T DeserializeXml<T>(XElement element)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(element.CreateReader());
        }
    }
}

