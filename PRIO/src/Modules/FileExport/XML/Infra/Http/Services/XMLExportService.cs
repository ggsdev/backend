﻿using PRIO.src.Modules.FileExport.Templates.Infra.EF.Enums;
using PRIO.src.Modules.FileExport.Templates.Interfaces;
using PRIO.src.Modules.FileExport.XML.Interfaces;
using PRIO.src.Modules.FileExport.XML.Strategy;
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
        private readonly ITemplateRepository _templateRepository;

        public XMLExportService(IDataQueryService dataQueryService, ITemplateRepository templateRepository)
        {
            _dataQueryService = dataQueryService;
            _templateRepository = templateRepository;
        }

        public async Task<string> ExportXML(Guid id, string table)
        {
            var verify = !_strategies.ContainsKey(table);
            if (verify)
                throw new ConflictException("Tabela não existente para geração de xml.");

            var model = await _dataQueryService.GetModelAsync(table, id);
            var templateXML = await _templateRepository.GetByType(TypeFile.XML042);
            var strategy = _strategies[table];
            var result = await strategy.ExportXML(model, templateXML);

            return result.FileContent;
        }
    }
}
