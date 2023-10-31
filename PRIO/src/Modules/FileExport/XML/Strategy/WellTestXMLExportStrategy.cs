using PRIO.src.Modules.FileExport.Templates.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using System.Xml;

namespace PRIO.src.Modules.FileExport.XML.Strategy
{
    public class WellTestXMLExportStrategy : IXMLExportStrategy
    {
        public async Task<XMLBase64> ExportXML(object model, Template templateXML)
        {
            XMLBase64? XML = null;
            if (model is WellTests wellTests)
            {
                XML = new XMLBase64
                {
                    WellTest = wellTests,
                };
            }
            else
            {
                throw new ArgumentException("Tipo de modelo não suportado");
            }

            var base64String = GenerateXML(wellTests, templateXML);

            XML.FileContent = base64String;

            return XML;
        }
        private string GenerateXML(WellTests wellTest, Template templateXML)
        {
            using MemoryStream memoryStream = new();
            using (XmlWriter writer = XmlWriter.Create(memoryStream))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("a042");

                writer.WriteStartElement("LISTA_TESTE_POCO");

                writer.WriteStartElement("TESTE_POCO");

                // Adiciona os elementos do XML com os valores do objeto WellTests
                writer.WriteElementString("COD_CADASTRO_POCO", wellTest.Well.CodWellAnp);
                writer.WriteElementString("IND_TIPO_TESTE", wellTest.Type);
                writer.WriteElementString("DHA_TESTE", wellTest.InitialDate);
                writer.WriteElementString("DHA_APLICACAO", wellTest.ApplicationDate);
                writer.WriteElementString("IND_VALIDO", wellTest.IsValid ? "S" : "N");
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

