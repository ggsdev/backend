using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Interfaces
{
    public interface INFSMRepository
    {
        Task<List<NFSM>> GetAll();
        Task<NFSM?> GetOneById(Guid id);
        Task<NFSMHistory?> DownloadFile(Guid id);
        Task AddAsync(NFSM nfsm);
        Task AddHistoryAsync(NFSMHistory history);
        Task AddRangeNFSMsProductionsAsync(List<NFSMsProductions> nFSMsProductions);
        Task SaveChangesAsync();
    }
}
