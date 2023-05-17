using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace PRIO.Files._039
{
    public class Functions
    {

        public static void IsRightFormat(string xmlFilePath, string xsdFilePath)
        {

            var schema = new XmlSchemaSet();

            schema.Add("", xsdFilePath);

            XmlReader reader = XmlReader.Create(xmlFilePath);
            XDocument doc = XDocument.Load(reader);
            doc.Validate(schema, ValidationEventHandler);
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {

            if (e.Severity == XmlSeverityType.Error)
            {

                Console.WriteLine(e.Message);
            }

        }

    }
}
