using PRIO.src.Modules.FileExport.Templates.Infra.EF.Enums;
using PRIO.src.Modules.FileExport.Templates.Interfaces;
using PRIO.src.Modules.FileExport.XML.Handlers;
using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileExport.XML.Strategy;
using PRIO.src.Modules.FileExport.XML.ViewModels;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.FileExport.XML.Infra.Http.Services
{
    public class XMLExportService
    {
        private readonly Dictionary<string, IXMLExportStrategy> _strategies = new()
        {
            [nameof(WellTests)] = new WellTestXMLExportStrategy(),
            [nameof(WellEvent)] = new EventXMLExportStrategy()
        };

        private readonly IDataQueryService _dataQueryService;
        private readonly IXMLFactory _xMLFactory;
        private readonly ITemplateRepository _templateRepository;
        private readonly IXMLExportRepository _repository;

        public XMLExportService(IDataQueryService dataQueryService, ITemplateRepository templateRepository, IXMLFactory xMLFactory, IXMLExportRepository repository)
        {
            _dataQueryService = dataQueryService;
            _templateRepository = templateRepository;
            _xMLFactory = xMLFactory;
            _repository = repository;
        }

        public async Task<string> ExportXML(Guid id, string table, ExportXMLViewModel body)
        {
            var verify = !_strategies.ContainsKey(table);
            if (verify)
                throw new ConflictException("Tabela não existente para geração de xml.");

            var model = await _dataQueryService.GetModelAsync(table, id);
            var templateXML = await _templateRepository.GetByType(TypeFile.XML042) ?? throw new ConflictException("Template não encontrado para XML.");
            var strategy = _strategies[table];
            var result = strategy.GenerateXML(model, templateXML);

            var fileName = CreateFileName(body, model);

            var createXMLBase64 = _xMLFactory.Create(model, result, fileName);
            await _repository.AddAsync(createXMLBase64);
            await _repository.SaveAsync();
            return result;
        }

        private static string CreateFileName(ExportXMLViewModel body, object model)
        {
            var internalNumber = "aaa";
            var CNPJ = "bbbbbbbb";
            var date = DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss").Replace(":", "");

            var handlerChain = new OptionalNameHandler();
            handlerChain.SetNext(new WellTestsHandler())
                       .SetNext(new WellEventHandler());

            string resultHandleChain = handlerChain.Handle(body, model);

            string buildString = $"{internalNumber}_{CNPJ}_{date}_{resultHandleChain}.xml";

            return buildString;

        }
    }
}
