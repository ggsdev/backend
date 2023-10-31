using AutoMapper;
using OfficeOpenXml;
using PRIO.src.Modules.FileExport.Templates.Infra.EF.Enums;
using PRIO.src.Modules.FileExport.Templates.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;

namespace PRIO.src.Modules.FileExport.Templates.Infra.Http.Services
{
    public class XLSXExportService
    {
        private readonly IWellEventRepository _wellEventRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;

        public XLSXExportService(IWellEventRepository wellEventRepository, IMapper mapper, ITemplateRepository templateRepository)
        {
            _wellEventRepository = wellEventRepository;
            _mapper = mapper;
            _templateRepository = templateRepository;
        }

        public async Task/*<ClosingOpeningFileXLSXDto>*/ GenerateClosingOpeningRelatory(DateTime beginning, DateTime end, Guid fieldId)
        {
            var wellEvents = await _wellEventRepository
            .GetByRangeDate(beginning, end, fieldId);

            var templateFile = await _templateRepository
            .GetByType(TypeFile.ClosingAndOpeningEventsXLS);

            using var stream = new MemoryStream(Convert.FromBase64String(templateFile!.FileContent));

            using ExcelPackage package = new(stream);

            var workbook = package.Workbook;
            var worksheetTab = workbook.Worksheets[0];

            var dimension = worksheetTab.Dimension;

            var dateCurrent = DateTime.UtcNow.AddHours(-3);

            for (int row = 2; row <= dimension.End.Row; ++row)
            {




            }
        }
    }
}
