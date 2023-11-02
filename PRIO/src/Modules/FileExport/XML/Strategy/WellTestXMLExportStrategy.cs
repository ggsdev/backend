using PRIO.src.Modules.FileExport.Templates.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using System.Text;
using System.Xml;

namespace PRIO.src.Modules.FileExport.XML.Strategy
{
    public class WellTestXMLExportStrategy : IXMLExportStrategy
    {
        public string GenerateXML(object model, Template templateXML)
        {
            if (model is WellTests wellTest)
            {
                using MemoryStream memoryStream = new();

                string base64Template = templateXML.FileContent;
                byte[] templateBytes = Convert.FromBase64String(base64Template);
                string templateXml = Encoding.UTF8.GetString(templateBytes);

                using (XmlWriter writer = XmlWriter.Create(memoryStream))
                {
                    using XmlReader reader = XmlReader.Create(new StringReader(templateXml));
                    var shouldCloseElements = false;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.Name)
                            {
                                case "a042":
                                    writer.WriteStartElement(reader.Name);
                                    break;
                                case "LISTA_TESTE_POCO":
                                    writer.WriteStartElement(reader.Name);
                                    break;
                                case "TESTE_POCO":
                                    writer.WriteStartElement(reader.Name);
                                    break;
                                case "COD_CADASTRO_POCO":
                                    writer.WriteElementString("COD_CADASTRO_POCO", wellTest.Well.CodWellAnp);
                                    break;
                                case "IND_TIPO_TESTE":
                                    writer.WriteElementString("IND_TIPO_TESTE", wellTest.Type);
                                    break;
                                case "DHA_TESTE":
                                    writer.WriteElementString("DHA_TESTE", wellTest.InitialDate.ToString());
                                    break;
                                case "DHA_APLICACAO":
                                    writer.WriteElementString("DHA_APLICACAO", wellTest.ApplicationDate);
                                    break;
                                case "IND_VALIDO":
                                    writer.WriteElementString("IND_VALIDO", wellTest.IsValid ? "S" : "N");
                                    break;
                                case "MED_POTENCIAL_OLEO":
                                    writer.WriteElementString("MED_POTENCIAL_OLEO", wellTest.PotencialOil.ToString());
                                    break;
                                case "MED_POTENCIAL_GAS":
                                    writer.WriteElementString("MED_POTENCIAL_GAS", wellTest.PotencialGas.ToString());
                                    break;
                                case "MED_POTENCIAL_AGUA":
                                    writer.WriteElementString("MED_POTENCIAL_AGUA", wellTest.PotencialWater.ToString());
                                    break;
                                case "NOM_RELATORIO":
                                    writer.WriteElementString("NOM_RELATORIO", wellTest.BTPNumber);
                                    break;
                                case "LISTA_SEPARADOR":
                                    writer.WriteElementString("LISTA_SEPARADOR", " ");
                                    shouldCloseElements = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                string base64String = Convert.ToBase64String(memoryStream.ToArray());
                return base64String;
            }
            else
            {
                throw new ArgumentException("Tipo de modelo não suportado");
            }
        }

        //using MemoryStream memoryStream = new();
        //using (XmlWriter writer = XmlWriter.Create(memoryStream))
        //{
        //    writer.WriteStartDocument();
        //    writer.WriteStartElement("a042");

        //    writer.WriteStartElement("LISTA_TESTE_POCO");

        //    writer.WriteStartElement("TESTE_POCO");

        //    // Adiciona os elementos do XML com os valores do objeto WellTests
        //    writer.WriteElementString("COD_CADASTRO_POCO", wellTest.Well.CodWellAnp);
        //    writer.WriteElementString("IND_TIPO_TESTE", wellTest.Type);
        //    writer.WriteElementString("DHA_TESTE", wellTest.InitialDate);
        //    writer.WriteElementString("DHA_APLICACAO", wellTest.ApplicationDate);
        //    writer.WriteElementString("IND_VALIDO", wellTest.IsValid ? "S" : "N");
        //    writer.WriteElementString("MED_POTENCIAL_OLEO", wellTest.PotencialOil.ToString());
        //    writer.WriteElementString("MED_POTENCIAL_GAS", wellTest.PotencialGas.ToString());
        //    writer.WriteElementString("MED_POTENCIAL_AGUA", wellTest.PotencialWater.ToString());
        //    writer.WriteElementString("NOM_RELATORIO", wellTest.BTPNumber);
        //    writer.WriteElementString("LISTA_SEPARADOR", "");

        //    writer.WriteEndElement();

        //    writer.WriteEndElement();

        //    writer.WriteEndElement();

        //    writer.WriteEndDocument();
        //}
    }
}


