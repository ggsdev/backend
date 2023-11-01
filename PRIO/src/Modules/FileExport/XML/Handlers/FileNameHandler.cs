using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileExport.XML.ViewModels;

namespace PRIO.src.Modules.FileExport.XML.Handlers
{
    public class FileNameHandler : IFileNameHandler
    {
        private IFileNameHandler? _nextHandler;

        public IFileNameHandler SetNext(IFileNameHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual string Handle(ExportXMLViewModel body, object model)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(body, model);
            }
            return "";
        }
    }
}
