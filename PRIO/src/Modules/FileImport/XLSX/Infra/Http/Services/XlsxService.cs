using AutoMapper;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.ViewModels;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;
using System.Globalization;

namespace PRIO.src.Modules.FileImport.XLSX.Infra.Http.Services
{
    public class XLSXService
    {
        private readonly string _consolidationInstance = "consolidador";
        private readonly DataContext _context;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly IMapper _mapper;

        public XLSXService(DataContext context, SystemHistoryService systemHistoryService, IMapper mapper)
        {
            _context = context;
            _systemHistoryService = systemHistoryService;
            _mapper = mapper;
        }

        public async Task ImportFiles(RequestXslxViewModel data, User user)
        {
            var envVars = DotEnv.Read();

            var contentBase64 = data.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using var stream = new MemoryStream(Convert.FromBase64String(contentBase64!));
            using ExcelPackage package = new(stream);
            envVars.TryGetValue("INSTANCE", out var getInstanceName);
            getInstanceName ??= _consolidationInstance;

            var workbook = package.Workbook;
            var worksheetTab = workbook.Worksheets
                .FirstOrDefault(x => x.Name.ToLower().Trim() == "informações gerais poços");
            worksheetTab ??= workbook.Worksheets[0];

            var dimension = worksheetTab.Dimension;

            var entityDictionary = new Dictionary<string, BaseModel>();
            var fieldDictionary = new Dictionary<string, Field>();
            var entityHistoriesDictionary = new Dictionary<string, SystemHistory>();

            var columnPositions = XlsUtils.GetColumnPositions(worksheetTab);
            var dateCurrent = DateTime.UtcNow;

            for (int row = 2; row <= dimension.End.Row; ++row)
            {
                var columnCluster = worksheetTab.Cells[row, columnPositions[XlsUtils.ClusterColumnName]].Value?.ToString();

                if (!(columnCluster?.ToLower() == getInstanceName || getInstanceName.ToLower() == _consolidationInstance))
                    continue;

                var columnInstallation = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationColumnName]].Value?.ToString()?.Trim();
                var columnInstallationCod = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationCodColumnName]].Value?.ToString()?.Trim();
                var columnInstallationCodUep = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationCodUepColumnName]].Value?.ToString()?.Trim();

                var columnField = worksheetTab.Cells[row, columnPositions[XlsUtils.FieldColumnName]].Value?.ToString()?.Trim();
                var columnCodeField = worksheetTab.Cells[row, columnPositions[XlsUtils.FieldCodeColumnName]].Value?.ToString()?.Trim();

                var columnReservoir = worksheetTab.Cells[row, columnPositions[XlsUtils.ReservoirColumnName]].Value?.ToString()?.Trim();

                var columnZone = worksheetTab.Cells[row, columnPositions[XlsUtils.ZoneCodeColumnName]].Value?.ToString()?.Trim();

                var columnCompletion = worksheetTab.Cells[row, columnPositions[XlsUtils.CompletionColumnName]].Value?.ToString()?.Trim();

                #region Well rows
                var columnWellNameAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellNameColumnName]].Value?.ToString()?.Trim();
                var columnWellOperatorName = worksheetTab.Cells[row, columnPositions[XlsUtils.WellNameOperatorColumnName]].Value?.ToString()?.Trim();
                var columnWellCodeAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCodeAnpColumnName]].Value?.ToString()?.Trim();
                var columnWellCategoryAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryAnpColumnName]].Value?.ToString()?.Trim();
                var columnWellCategoryReclassification = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryReclassificationColumnName]].Value?.ToString()?.Trim();
                var columnWellCategoryOperator = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryOperatorColumnName]].Value?.ToString()?.Trim();
                var columnWellStatusOperator = worksheetTab.Cells[row, columnPositions[XlsUtils.WellStatusOperatorColumnName]].Value?.ToString()?.ToLower()?.Trim();
                bool? columnWellStatusOperatorBoolean = null;
                if (columnWellStatusOperator is not null && columnWellStatusOperator.Contains("ativo"))
                    columnWellStatusOperatorBoolean = true;
                if (columnWellStatusOperator is not null && columnWellStatusOperator.Contains("inativo"))
                    columnWellStatusOperatorBoolean = false;
                var columnWellProfile = worksheetTab.Cells[row, columnPositions[XlsUtils.WellProfileColumnName]].Value?.ToString()?.Trim();
                var columnWellWaterDepth = worksheetTab.Cells[row, columnPositions[XlsUtils.WellWaterDepthColumnName]].Value?.ToString()?.Trim();
                var columnWellPerforationTopMd = worksheetTab.Cells[row, columnPositions[XlsUtils.WellPerforationTopMdColumnName]].Value?.ToString()?.Trim();
                var columnWellBottomPerforationMd = worksheetTab.Cells[row, columnPositions[XlsUtils.WellBottomPerforationColumnName]].Value?.ToString()?.Trim();
                var columnWellArtificialLift = worksheetTab.Cells[row, columnPositions[XlsUtils.WellArtificialLiftColumnName]].Value?.ToString()?.Trim();
                var columnWellLatitude4c = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLatitude4cColumnName]].Value?.ToString()?.Trim();
                var columnWellLongitude4c = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLongitude4cColumnName]].Value?.ToString()?.Trim();
                var columnWellLatitudeDD = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLatitudeDDColumnName]].Value?.ToString()?.Trim();
                var columnWellLongitudeDD = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLongitudeDDColumnName]].Value?.ToString()?.Trim();
                var columnWellDatumHorizontal = worksheetTab.Cells[row, columnPositions[XlsUtils.WellDatumHorizontalColumnName]].Value?.ToString()?.Trim();
                var columnWellTypeCoordinate = worksheetTab.Cells[row, columnPositions[XlsUtils.WellTypeCoordinateColumnName]].Value?.ToString()?.Trim();
                var columnWellCoordX = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCoordXColumnName]].Value?.ToString()?.Trim();
                var columnWellCoordY = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCoordYColumnName]].Value?.ToString()?.Trim();
                #endregion


                if (!string.IsNullOrWhiteSpace(columnCluster) && !entityDictionary.TryGetValue(columnCluster.ToLower(), out var cluster))
                {
                    cluster = await _context.Clusters.FirstOrDefaultAsync(x => x.Name.ToLower() == columnCluster.ToLower());
                    if (cluster is null)
                    {
                        var clusterId = Guid.NewGuid();
                        cluster = new Cluster
                        {
                            Id = clusterId,
                            Name = columnCluster,
                            User = user,
                            IsActive = true,
                            CodCluster = GenerateCode.Generate(columnCluster),
                        };

                        var history = _systemHistoryService
                            .Import<Cluster, ClusterHistoryDTO>(HistoryColumns.TableClusters, user, clusterId, cluster as Cluster);

                        entityDictionary[columnCluster.ToLower()] = cluster;
                        entityHistoriesDictionary[columnCluster.ToLower()] = history;
                    }

                }

                if (!string.IsNullOrWhiteSpace(columnInstallationCod) && !entityDictionary.TryGetValue(columnInstallationCod, out var installation))
                {
                    installation = await _context.Installations.FirstOrDefaultAsync(x => x.CodInstallationAnp == columnInstallationCod);

                    if (installation is null)
                    {
                        var installationId = Guid.NewGuid();
                        installation = new Installation
                        {
                            Id = installationId,
                            Name = columnInstallation,
                            UepCod = columnInstallationCodUep,
                            CodInstallationAnp = columnInstallationCod,
                            User = user,
                            IsActive = true,
                            Cluster = columnCluster is not null ? (Cluster)entityDictionary.GetValueOrDefault(columnCluster.ToLower())! : null,
                        };

                        var history = _systemHistoryService
                            .Import<Installation, InstallationHistoryDTO>(HistoryColumns.TableInstallations, user, installationId, installation as Installation);

                        entityHistoriesDictionary[columnInstallation.ToLower()] = history;
                        entityDictionary[columnInstallation.ToLower()] = installation;
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnField) && !entityDictionary.TryGetValue(columnField.ToLower(), out var field))
                {
                    field = await _context.Fields.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == columnField.ToLower().Trim());

                    if (field is null)
                    {
                        var fieldId = Guid.NewGuid();
                        field = new Field
                        {
                            Id = fieldId,
                            Name = columnField,
                            User = user,
                            Installation = columnInstallation is not null ? (Installation)entityDictionary.GetValueOrDefault(columnInstallation.ToLower())! : null,
                            CodField = columnCodeField,
                            IsActive = true,
                        };

                        var history = _systemHistoryService
                            .Import<Field, FieldHistoryDTO>(HistoryColumns.TableFields, user, fieldId, field as Field);

                        entityDictionary[columnField.ToLower()] = field;
                        entityHistoriesDictionary[columnField.ToLower()] = history;
                    }

                    fieldDictionary[columnField.ToLower()] = (Field)field;
                }

                if (!string.IsNullOrWhiteSpace(columnZone) && !entityDictionary.TryGetValue(columnZone.ToLower(), out var zone))
                {
                    zone = await _context.Zones.FirstOrDefaultAsync(x => x.CodZone.ToLower() == columnZone.ToLower());
                    if (zone is null)
                    {
                        var zoneId = Guid.NewGuid();
                        zone = new Zone
                        {
                            Id = zoneId,
                            CodZone = columnZone,
                            User = user,
                            IsActive = true,
                            Field = columnField is not null ? (Field)entityDictionary.GetValueOrDefault(columnField.ToLower())! : null,
                        };

                        var history = _systemHistoryService
                           .Import<Zone, ZoneHistoryDTO>(HistoryColumns.TableZones, user, zoneId, zone as Zone);

                        entityHistoriesDictionary[columnZone.ToLower()] = history;
                        entityDictionary[columnZone.ToLower()] = zone;
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnReservoir) && !entityDictionary.TryGetValue(columnReservoir.ToLower(), out var reservoir))
                {
                    reservoir = await _context.Reservoirs.FirstOrDefaultAsync(x => x.Name.ToLower() == columnReservoir.ToLower());
                    if (reservoir is null)
                    {
                        var reservoirId = Guid.NewGuid();
                        reservoir = new Reservoir
                        {
                            Id = reservoirId,
                            Name = columnReservoir,
                            User = user,
                            CodReservoir = GenerateCode.Generate(columnReservoir),
                            Zone = columnZone is not null ? (Zone)entityDictionary.GetValueOrDefault(columnZone.ToLower())! : null,
                            IsActive = true,
                        };

                        var history = _systemHistoryService
                           .Import<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, reservoirId, reservoir as Reservoir);

                        entityHistoriesDictionary[columnReservoir.ToLower()] = history;
                        entityDictionary[columnReservoir.ToLower()] = reservoir;
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnWellCodeAnp) && !entityDictionary.TryGetValue(columnWellCodeAnp.ToLower(), out var well))
                {
                    well = await _context.Wells
                        .FirstOrDefaultAsync(x => x.CodWellAnp.ToLower() == columnWellCodeAnp.ToLower());

                    if (well is null)
                    {
                        var wellId = Guid.NewGuid();
                        well = new Well
                        {
                            Id = wellId,
                            Name = columnWellNameAnp,
                            WellOperatorName = columnWellOperatorName,
                            CodWellAnp = columnWellCodeAnp,
                            CategoryAnp = columnWellCategoryAnp,
                            CategoryReclassificationAnp = columnWellCategoryReclassification,
                            CategoryOperator = columnWellCategoryOperator,
                            StatusOperator = columnWellStatusOperatorBoolean,
                            Type = columnWellProfile,
                            WaterDepth = decimal.TryParse(columnWellWaterDepth?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var columnWellWaterDepthDouble) ? columnWellWaterDepthDouble : 0,
                            TopOfPerforated = decimal.TryParse(columnWellPerforationTopMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var topOfPerforated) ? topOfPerforated : 0,
                            BaseOfPerforated = decimal.TryParse(columnWellBottomPerforationMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var baseOfPerforated) ? baseOfPerforated : 0,
                            ArtificialLift = columnWellArtificialLift,
                            Latitude4C = columnWellLatitude4c,
                            Longitude4C = columnWellLongitude4c,
                            LatitudeDD = columnWellLatitudeDD,
                            LongitudeDD = columnWellLongitudeDD,
                            DatumHorizontal = columnWellDatumHorizontal,
                            TypeBaseCoordinate = columnWellTypeCoordinate,
                            CoordX = columnWellCoordX,
                            CoordY = columnWellCoordY,
                            User = user,
                            IsActive = true,
                            Field = (Field)entityDictionary.GetValueOrDefault(columnField.ToLower()),
                        };

                        var history = _systemHistoryService
                           .Import<Well, WellHistoryDTO>(HistoryColumns.TableWells, user, wellId, well as Well);

                        entityHistoriesDictionary[columnWellCodeAnp.ToLower()] = history;
                        entityDictionary[columnWellCodeAnp.ToLower()] = well;
                    }

                    else
                    {
                        var beforeChangesWell = _mapper.Map<WellHistoryDTO>(well);

                        var propertiesToUpdate = new WellUpdateImportViewModel
                        {
                            Name = columnWellNameAnp,
                            WellOperatorName = columnWellOperatorName,
                            CategoryAnp = columnWellCategoryAnp,
                            CategoryReclassificationAnp = columnWellCategoryReclassification,
                            CategoryOperator = columnWellCategoryOperator,
                            StatusOperator = columnWellStatusOperatorBoolean,
                            Type = columnWellProfile,
                            WaterDepth = decimal.TryParse(columnWellWaterDepth?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var columnWellWaterDepthDouble) ? columnWellWaterDepthDouble : 0,
                            TopOfPerforated = decimal.TryParse(columnWellPerforationTopMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var topOfPerforated) ? topOfPerforated : 0,
                            BaseOfPerforated = decimal.TryParse(columnWellBottomPerforationMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var baseOfPerforated) ? baseOfPerforated : 0,
                            ArtificialLift = columnWellArtificialLift,
                            Latitude4C = columnWellLatitude4c,
                            Longitude4C = columnWellLongitude4c,
                            LatitudeDD = columnWellLatitudeDD,
                            LongitudeDD = columnWellLongitudeDD,
                            DatumHorizontal = columnWellDatumHorizontal,
                            TypeBaseCoordinate = columnWellTypeCoordinate,
                            CoordX = columnWellCoordX,
                            CoordY = columnWellCoordY,
                        };

                        var wellConverted = (Well)well;
                        var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(wellConverted, propertiesToUpdate);

                        var fieldToUpdate = fieldDictionary.GetValueOrDefault(columnField.ToLower());

                        if (updatedProperties.Any() is true ||
                            (fieldToUpdate?.Id is not null && wellConverted.Field?.Id != fieldToUpdate?.Id))
                        {
                            if (wellConverted.Field?.Id != fieldToUpdate?.Id && fieldToUpdate is not null)
                            {
                                wellConverted.Field = fieldToUpdate;
                                updatedProperties[nameof(WellHistoryDTO.fieldId)] = fieldToUpdate.Id;
                            }

                            _context.Wells.Update(wellConverted);

                            var history = await _systemHistoryService
                                .ImportUpdate(HistoryColumns.TableWells, user, updatedProperties, wellConverted.Id, wellConverted, beforeChangesWell);

                            entityHistoriesDictionary[columnWellCodeAnp.ToLower()] = history;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnCompletion) && !entityDictionary.TryGetValue(columnCompletion.ToLower(), out var completion))
                {
                    completion = await _context.Completions.FirstOrDefaultAsync(x => x.Name.ToLower() == columnCompletion.ToLower());
                    if (completion is null)
                    {
                        var completionId = Guid.NewGuid();
                        completion = new Completion
                        {
                            Id = completionId,
                            Name = columnCompletion,
                            User = user,
                            CodCompletion = GenerateCode.Generate(columnCompletion),
                            Reservoir = columnReservoir is not null ? (Reservoir)entityDictionary.GetValueOrDefault(columnReservoir?.ToLower())! : null,
                            Well = columnWellCodeAnp is not null ? (Well)entityDictionary.GetValueOrDefault(columnWellCodeAnp?.ToLower())! : null,
                            IsActive = true
                        };

                        var history = _systemHistoryService
                            .Import<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, completionId, completion as Completion);

                        entityHistoriesDictionary[columnCompletion.ToLower()] = history;
                        entityDictionary[columnCompletion.ToLower()] = completion;
                    }
                }
            }
            await _context.AddRangeAsync(entityDictionary.Values);
            await _context.AddRangeAsync(entityHistoriesDictionary.Values);
            await _context.SaveChangesAsync();
        }
    }
}
