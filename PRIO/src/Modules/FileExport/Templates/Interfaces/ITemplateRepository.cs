using PRIO.src.Modules.FileExport.Templates.Infra.EF.Enums;
using PRIO.src.Modules.FileExport.Templates.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.Templates.Interfaces
{
    public interface ITemplateRepository
    {
        Task<List<Template>> GetAll();
        Task<Template?> GetById(Guid? id);
        Task<Template?> GetByType(TypeFile type);
    }
}
