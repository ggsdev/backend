using PRIO.src.Modules.FileExport.Templates.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using System.Xml;

namespace PRIO.src.Modules.FileExport.XML.Strategy
{
    public class EventXMLExportStrategy : IXMLExportStrategy
    {
        public async Task<XMLBase64> ExportXML(object model, Template templateXML)
        {
            XMLBase64? XML = null;
            if (model is WellEvent wellEvent)
            {
                XML = new XMLBase64
                {
                    WellEvent = wellEvent,
                };
            }
            else
            {
                throw new ConflictException("Tipo de modelo não suportado");
            }
            var base64String = GenerateXML(wellEvent, templateXML);

            XML.FileContent = base64String;

            return XML;

        }
        private string GenerateXML(WellEvent wellEvent, Template templateXML)
        {
            var date = wellEvent.StartDate;
            WellTests? wellTest = null;
            if (wellEvent.Well.WellTests is not null && wellEvent.Well.WellTests.Count != 0)
            {
                wellTest = wellEvent.Well.WellTests.Where(x => (x.FinalApplicationDate == null && x.ApplicationDate != null && DateTime.Parse(x.ApplicationDate) <= date) && x.Well.CategoryOperator is not null ||
                        x.Well is not null && x.Well.CategoryOperator is not null && (x.FinalApplicationDate != null && x.ApplicationDate != null && DateTime.Parse(x.FinalApplicationDate) >= date && DateTime.Parse(x.ApplicationDate) <= date)).FirstOrDefault();
                if (wellTest == null)
                    throw new ConflictException("Não existe teste de poço para esse evento.");
            }
            else
            {
                throw new ConflictException("Não existe teste de poço para esse evento.");

            }

            using MemoryStream memoryStream = new();
            using (XmlWriter writer = XmlWriter.Create(memoryStream))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("a042");
                writer.WriteStartElement("LISTA_TESTE_POCO");
                writer.WriteStartElement("TESTE_POCO");

                writer.WriteElementString("COD_CADASTRO_POCO", wellEvent.Well.CodWellAnp);
                writer.WriteElementString("IND_TIPO_TESTE", wellEvent.EventStatus);
                writer.WriteElementString("DHA_TESTE", wellEvent.StartDate.ToString());
                writer.WriteElementString("DHA_APLICACAO", wellTest.ApplicationDate);
                writer.WriteElementString("IND_VALIDO", "S");
                writer.WriteElementString("MED_POTENCIAL_OLEO", wellTest.PotencialOil.ToString());
                writer.WriteElementString("MED_POTENCIAL_GAS", wellTest.PotencialGas.ToString());
                writer.WriteElementString("MED_POTENCIAL_AGUA", wellTest.PotencialWater.ToString());
                writer.WriteElementString("NOM_RELATORIO", wellTest.BTPNumber);
                writer.WriteElementString("LISTA_SEPARADOR", "");

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            string base64String = Convert.ToBase64String(memoryStream.ToArray());
            return base64String;
        }

    }
}
