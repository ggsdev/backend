using PRIO.src.Modules.FileExport.XML.ViewModels;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XML.Handlers
{
    public class WellEventHandler : FileNameHandler
    {
        public override string Handle(ExportXMLViewModel body, object model)
        {
            if (model is WellEvent wellEvent)
            {
                return wellEvent.Well.WellOperatorName;
            }
            return base.Handle(body, model);
        }
    }
}
