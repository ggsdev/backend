using PRIO.Data;
using PRIO.Models;
using System.Xml.Serialization;
namespace PRIO
{
    public class ProcessingData
    {

        public static Fields ReadXml(string xmlFilePath)
        {
            XmlSerializer serializer = new(typeof(Fields));

            using StreamReader reader = new(xmlFilePath);
            return (Fields)serializer.Deserialize(reader);
        }

        public static void SaveFieldsToDatabase(List<Field> fields)
        {
            using var dbContext = new DataContext();
            foreach (var field in fields)
            {
                dbContext.Fields.Add(field);
            }

            dbContext.SaveChanges();
        }

    }
}
