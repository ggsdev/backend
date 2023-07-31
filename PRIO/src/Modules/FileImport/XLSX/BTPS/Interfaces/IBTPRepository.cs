using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces
{
    public interface IBTPRepository
    {
        Task<List<BTP>> GetAllBTPsAsync();
    }
}
