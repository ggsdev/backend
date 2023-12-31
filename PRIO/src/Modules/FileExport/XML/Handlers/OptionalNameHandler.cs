﻿using PRIO.src.Modules.FileExport.XML.ViewModels;

namespace PRIO.src.Modules.FileExport.XML.Handlers
{
    public class OptionalNameHandler : FileNameHandler
    {
        public override string Handle(ExportXMLViewModel body, object model)
        {
            if (!string.IsNullOrEmpty(body.OptionalName))
            {
                return body.OptionalName;
            }
            return base.Handle(body, model);
        }
    }
}
