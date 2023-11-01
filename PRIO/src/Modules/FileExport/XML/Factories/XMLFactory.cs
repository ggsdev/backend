using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.FileExport.XML.Factories
{
    public class XMLFactory : IXMLFactory
    {
        public object Create(object model, string base64, string fileName)
        {
            var XMLId = Guid.NewGuid();
            if (model is WellTests)
            {
                return new WellTestXML042Base64
                {
                    Id = XMLId,
                    WellTest = model is WellTests ? model as WellTests : null,
                    Filename = fileName,
                    FileContent = base64
                };
            }
            else if (model is WellEvent)
            {
                return new WellEventXML042Base64
                {
                    Id = XMLId,
                    WellEvent = model is WellEvent ? model as WellEvent : null,
                    Filename = fileName,
                    FileContent = base64
                };
            }
            else
            {
                throw new ConflictException("Modelo não corresponde a WellTest ou WellEvent");
            }
        }
    }
}
