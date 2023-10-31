﻿using AutoMapper;
using OfficeOpenXml;
using PRIO.src.Modules.FileExport.Templates.Infra.EF.Enums;
using PRIO.src.Modules.FileExport.Templates.Interfaces;
using PRIO.src.Modules.FileExport.XLSX.Dtos;
using PRIO.src.Modules.FileExport.XLSX.Utils;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.FileExport.Templates.Infra.Http.Services
{
    public class XLSXExportService
    {
        private readonly IWellEventRepository _wellEventRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;

        public XLSXExportService(IWellEventRepository wellEventRepository, IMapper mapper, ITemplateRepository templateRepository, IInstallationRepository installationRepository, IFieldRepository fieldRepository)
        {
            _wellEventRepository = wellEventRepository;
            _mapper = mapper;
            _templateRepository = templateRepository;
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
        }

        public async Task<ClosingOpeningFileXLSXDto> GenerateClosingOpeningRelatory(DateTime beginning, DateTime end, Guid fieldId)
        {
            var dateCurrent = DateTime.UtcNow.AddHours(-3);

            if (beginning.Date >= dateCurrent.Date)
                throw new BadRequestException("Relatórios só podem ser gerados em d-1.");

            if (end.Date >= dateCurrent.Date)
                throw new BadRequestException("Relatórios só podem ser gerados em d-1.");

            var anyField = await _fieldRepository.Any(fieldId);

            if (anyField is false)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var templateFile = await _templateRepository
                .GetByType(TypeFile.ClosingAndOpeningEventsXLS)
            ?? throw new NotFoundException("Template não encontrado");

            var wellEvents = await _wellEventRepository
                .GetByRangeDate(beginning, end, fieldId);

            if (wellEvents.Any() is false)
                throw new BadRequestException("Não foi encontrado eventos nessa data");

            using var stream = new MemoryStream(Convert.FromBase64String(templateFile.FileContent));

            using ExcelPackage package = new(stream);

            var workbook = package.Workbook;
            var worksheetTab = workbook.Worksheets[0];

            var dimension = worksheetTab.Dimension;

            var dateFormat = "HH/MMM/yy HH:mm";
            var hourFormat = "HH:mm:ss";
            var defaultValue = "N/A";

            var columnPositions = ExportUtils.GetColumnPositions(worksheetTab);

            var firstWellEvent = wellEvents.First();

            var uep = await _installationRepository
                .GetUep(firstWellEvent.Well.Field!.Installation!.CodInstallationAnp);

            for (int row = 2; row <= dimension.End.Row; ++row)
            {
                if (row - 2 < wellEvents.Count)
                {
                    var wellEvent = wellEvents[row - 2];

                    foreach (var reason in wellEvent.EventReasons)
                    {

                        worksheetTab.Cells[row, columnPositions[ExportUtils.DateEventColumnName]].Value = wellEvent.StartDate.ToString(dateFormat);
                        worksheetTab.Cells[row, columnPositions[ExportUtils.ProcessingInstallationColumnName]].Value = uep!.Name;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.InstallationCodColumnName]].Value = wellEvent.Well.Field!.Installation!.Name;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.FieldNameColumnName]].Value = wellEvent.Well.Field!.Name;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.WellANPNameColumnName]].Value = wellEvent.Well.Name;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.OperatorWellNameColumnName]].Value = wellEvent.Well.WellOperatorName;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.OperatorCategoryColumnName]].Value = wellEvent.Well.CategoryOperator;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.EventTypeColumnName]].Value = "Fechamento";
                        worksheetTab.Cells[row, columnPositions[ExportUtils.EventIDColumnName]].Value = wellEvent.IdAutoGenerated;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.RelatedEventColumnName]].Value = wellEvent.EventRelatedCode;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.ANPStatusColumnName]].Value = wellEvent.StatusANP;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.ANPStateColumnName]].Value = wellEvent.StateANP;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.EventJustificationColumnName]].Value = wellEvent.Reason;

                        worksheetTab.Cells[row, columnPositions[ExportUtils.RelatedSystemColumnName]].Value = reason.SystemRelated;
                        worksheetTab.Cells[row, columnPositions[ExportUtils.EventStartColumnName]].Value = reason.StartDate.ToString(hourFormat);
                        worksheetTab.Cells[row, columnPositions[ExportUtils.EventEndColumnName]].Value = reason.EndDate?.ToString(hourFormat);

                        if (reason.EndDate is not null)
                        {
                            worksheetTab.Cells[row, columnPositions[ExportUtils.StopPeriodColumnName]].Value = reason.Interval;
                            worksheetTab.Cells[row, columnPositions[ExportUtils.GasLossColumnName]].Value = wellEvent.StateANP;
                            worksheetTab.Cells[row, columnPositions[ExportUtils.OilLossColumnName]].Value = wellEvent.StateANP;
                            worksheetTab.Cells[row, columnPositions[ExportUtils.WaterLossColumnName]].Value = wellEvent.StateANP;
                            worksheetTab.Cells[row, columnPositions[ExportUtils.ProportionalDayColumnName]].Value = wellEvent.StateANP;

                        }

                        row++;
                    }

                    worksheetTab.Cells[row, columnPositions[ExportUtils.DateEventColumnName]].Value = wellEvent.StartDate.ToString(dateFormat);
                    worksheetTab.Cells[row, columnPositions[ExportUtils.EventStartColumnName]].Value = wellEvent.StartDate.ToString(hourFormat);
                    worksheetTab.Cells[row, columnPositions[ExportUtils.EventEndColumnName]].Value = wellEvent.EndDate?.ToString(hourFormat);
                    worksheetTab.Cells[row, columnPositions[ExportUtils.ProcessingInstallationColumnName]].Value = uep!.Name;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.InstallationCodColumnName]].Value = wellEvent.Well.Field!.Installation!.Name;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.FieldNameColumnName]].Value = wellEvent.Well.Field!.Name;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.WellANPNameColumnName]].Value = wellEvent.Well.Name;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.OperatorWellNameColumnName]].Value = wellEvent.Well.WellOperatorName;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.OperatorCategoryColumnName]].Value = wellEvent.Well.CategoryOperator;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.EventTypeColumnName]].Value = "Abertura";
                    worksheetTab.Cells[row, columnPositions[ExportUtils.RelatedSystemColumnName]].Value = defaultValue;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.GasLossColumnName]].Value = defaultValue;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.OilLossColumnName]].Value = defaultValue;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.WaterLossColumnName]].Value = defaultValue;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.ProportionalDayColumnName]].Value = defaultValue;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.StopPeriodColumnName]].Value = defaultValue;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.EventIDColumnName]].Value = wellEvent.IdAutoGenerated;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.RelatedEventColumnName]].Value = wellEvent.EventRelatedCode;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.ANPStatusColumnName]].Value = wellEvent.StatusANP;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.ANPStateColumnName]].Value = wellEvent.StateANP;
                    worksheetTab.Cells[row, columnPositions[ExportUtils.EventJustificationColumnName]].Value = wellEvent.Reason;
                }
            }

            var byteArray = await package.GetAsByteArrayAsync();


            var fileContent = Convert.ToBase64String(byteArray);

            var result = new ClosingOpeningFileXLSXDto
            (
                 Guid.NewGuid(),
                "aaaaa.xlsx",
               fileContent,
                "XLSX",
                "Relatório"
            );

            return result;
        }
    }
}
