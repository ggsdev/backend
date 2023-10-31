namespace PRIO.src.Modules.FileExport.XML.Interfaces
{
    public interface IDataQueryService
    {
        Task<object> GetModelAsync(string tableName, Guid id);
    }
}
