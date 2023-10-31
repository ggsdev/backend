using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.FileExport.XML.Infra.Http.Services
{
    public class DataQueryService : IDataQueryService
    {
        private readonly IBTPRepository _bTPRepository;
        private readonly IWellEventRepository _wellEventRepository;
        public DataQueryService(IBTPRepository btpRepository, IWellEventRepository wellEventRepository)
        {
            _bTPRepository = btpRepository;
            _wellEventRepository = wellEventRepository;
        }
        public async Task<object> GetModelAsync(string tableName, Guid id)
        {
            if (tableName == nameof(WellTests))
            {
                var wellTest = await _bTPRepository.GetByDataIdAsync(id);
                return wellTest is null ? throw new ConflictException("Teste de poço não encontrado") : (object)wellTest;
            }
            else if (tableName == nameof(WellEvent))
            {
                var wellEvent = await _wellEventRepository.GetEventWithWellTestById(id);
                return wellEvent is null ? throw new ConflictException("Evento do poço não encontrado.") : (object)wellEvent;
            }
            else
            {
                throw new ArgumentException("Tabela não existente para consulta de dados.");
            }
        }
    }
}
