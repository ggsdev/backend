using PRIO.src.Modules.FileExport.XML.ViewModels;

namespace PRIO.src.Modules.FileExport.XML.Interfaces
{
    public interface IFileNameHandler
    {
        string Handle(ExportXMLViewModel body, object model);
        IFileNameHandler SetNext(IFileNameHandler handler);
    }
}
