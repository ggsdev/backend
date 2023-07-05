﻿using AutoMapper;
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
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;
using System.Globalization;

namespace PRIO.src.Modules.FileImport.XLSX.Infra.Http.Services
{
    public class XLSXService
    {
        private readonly string _consolidationInstance = "consolidador";
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly SystemHistoryService _systemHistoryService;

        public XLSXService(IMapper mapper, DataContext context, SystemHistoryService systemHistoryService)
        {
            _mapper = mapper;
            _context = context;
            _systemHistoryService = systemHistoryService;
        }

        public async Task ImportFiles(RequestXslxViewModel data, User user)
        {
            if (data.FileName.EndsWith(".xlsx") is false)
                throw new BadRequestException("O arquivo deve ter a extensão .xlsx");

            var envVars = DotEnv.Read();

            var contentBase64 = data.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using var stream = new MemoryStream(Convert.FromBase64String(contentBase64!));
            using ExcelPackage package = new(stream);
            envVars.TryGetValue("INSTANCE", out var getInstanceName);
            //envVars.TryGetValue("INSTALLATION", out var getInstallationInstanceName);
            getInstanceName ??= _consolidationInstance;
            //getInstallationInstanceName ??= _consolidationInstance;

            var workbook = package.Workbook;
            var worksheetTab = workbook.Worksheets
                .FirstOrDefault(x => x.Name.ToLower().Trim() == "informações gerais poços");
            worksheetTab ??= workbook.Worksheets[0];

            var dimension = worksheetTab.Dimension;

            var entityDictionary = new Dictionary<string, BaseModel>();
            var updatedDictionary = new Dictionary<string, BaseModel>();

            var dateCurrent = DateTime.UtcNow;
            var columnPositions = XlsUtils.GetColumnPositions(worksheetTab);

            var errors = XlsUtils.ValidateColumns(worksheetTab);
            Console.WriteLine(errors.Count);
            if (errors.Any())
            {
                var error = new XlsErrorImportDTO
                {
                    Message = "Alguma(s) colunas(s) da planilha não possuem o valor esperado.",
                    Errors = errors
                };
                throw new BadRequestException(error.Message, error.Errors);
            }

            for (int row = 2; row <= dimension.End.Row; ++row)
            {
                var columnCluster = worksheetTab.Cells[row, columnPositions[XlsUtils.ClusterColumnName]].Value?.ToString();

                if (!(columnCluster?.ToLower() == getInstanceName || getInstanceName.ToLower() == _consolidationInstance))
                    continue;

                var columnInstallation = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationColumnName]].Value?.ToString()?.Trim();
                //if (!(columnInstallation?.ToLower() == getInstallationInstanceName || getInstallationInstanceName.ToLower() == _consolidationInstance))
                //    continue;

                var columnInstallationCod = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationCodColumnName]].Value?.ToString()?.Trim();
                var columnInstallationCodUep = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationCodUepColumnName]].Value?.ToString()?.Trim();
                var columnInstallationNameUep = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationNameUepColumnName]].Value?.ToString()?.Trim();

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
                    cluster = await _context.Clusters
                        .FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == columnCluster.ToLower().Trim());

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

                        await _systemHistoryService
                            .Import<Cluster, ClusterHistoryDTO>(HistoryColumns.TableClusters, user, data.FileName, cluster.Id, (Cluster)cluster);

                        entityDictionary[columnCluster.ToLower()] = cluster;
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnInstallationCod) && !entityDictionary.TryGetValue(columnInstallationCod.ToLower(), out var installation))
                {
                    installation = await _context.Installations
                        .FirstOrDefaultAsync(x => x.CodInstallationAnp.ToLower().Trim() == columnInstallationCod.ToLower().Trim());

                    if (installation is null && columnCluster is not null)
                    {
                        var installationId = Guid.NewGuid();

                        var clusterInDatabase = await _context.Clusters
                            .FirstOrDefaultAsync(x => x.Name == columnCluster);

                        installation = new Installation
                        {
                            Id = installationId,
                            Name = columnInstallation,
                            UepCod = columnInstallationCodUep,
                            CodInstallationAnp = columnInstallationCod,
                            UepName = columnInstallationNameUep,
                            User = user,
                            IsActive = true,
                            Cluster = entityDictionary.GetValueOrDefault(columnCluster.ToLower()) as Cluster is null ? clusterInDatabase : entityDictionary.GetValueOrDefault(columnCluster.ToLower()) as Cluster,
                        };

                        await _systemHistoryService
                            .Import<Installation, InstallationHistoryDTO>(HistoryColumns.TableInstallations, user, data.FileName, installation.Id, (Installation)installation);

                        entityDictionary[columnInstallationCod.ToLower()] = installation;
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnField) && !entityDictionary.TryGetValue(columnField.ToLower(), out var field))
                {
                    field = await _context.Fields
                        .FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == columnField.ToLower().Trim());

                    if (field is null && columnInstallationCod is not null)
                    {
                        var fieldId = Guid.NewGuid();

                        var installationInDatabase = await _context.Installations
                            .FirstOrDefaultAsync(x => x.CodInstallationAnp == columnInstallationCod);

                        field = new Field
                        {
                            Id = fieldId,
                            Name = columnField,
                            User = user,
                            Installation = entityDictionary.GetValueOrDefault(columnInstallationCod.ToLower()) as Installation is null ? installationInDatabase : entityDictionary.GetValueOrDefault(columnInstallationCod.ToLower()) as Installation,
                            CodField = columnCodeField,
                            IsActive = true,
                        };

                        await _systemHistoryService
                             .Import<Field, FieldHistoryDTO>(HistoryColumns.TableFields, user, data.FileName, field.Id, (Field)field);

                        entityDictionary[columnField.ToLower()] = field;
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnZone) && !entityDictionary.TryGetValue(columnZone.ToLower(), out var zone))
                {
                    zone = await _context.Zones
                        .FirstOrDefaultAsync(x => x.CodZone.ToLower().Trim() == columnZone.ToLower().Trim());

                    if (zone is null && columnField is not null)
                    {
                        var zoneId = Guid.NewGuid();

                        var fieldInDatabase = await _context.Fields
                            .FirstOrDefaultAsync(x => x.CodField == columnField);

                        zone = new Zone
                        {
                            Id = zoneId,
                            CodZone = columnZone,
                            User = user,
                            IsActive = true,
                            Field = entityDictionary.GetValueOrDefault(columnField.ToLower()) as Field is null ? fieldInDatabase : entityDictionary.GetValueOrDefault(columnField.ToLower()) as Field,
                        };

                        await _systemHistoryService
                            .Import<Zone, ZoneHistoryDTO>(HistoryColumns.TableZones, user, data.FileName, zone.Id, (Zone)zone);

                        entityDictionary[columnZone.ToLower()] = zone;
                    }

                }

                if (!string.IsNullOrWhiteSpace(columnReservoir) && !entityDictionary.TryGetValue(columnReservoir.ToLower(), out var reservoir))
                {
                    reservoir = await _context.Reservoirs
                        .FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == columnReservoir.ToLower().Trim());

                    if (reservoir is null && columnZone is not null)
                    {
                        var reservoirId = Guid.NewGuid();

                        var zoneInDatabase = await _context.Zones
                            .FirstOrDefaultAsync(x => x.CodZone == columnZone);

                        reservoir = new Reservoir
                        {
                            Id = reservoirId,
                            Name = columnReservoir,
                            User = user,
                            CodReservoir = GenerateCode.Generate(columnReservoir),
                            Zone = entityDictionary.GetValueOrDefault(columnZone.ToLower()) as Zone is null ? zoneInDatabase : entityDictionary.GetValueOrDefault(columnZone.ToLower()) as Zone,
                            IsActive = true,
                        };

                        await _systemHistoryService
                            .Import<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, data.FileName, reservoir.Id, (Reservoir)reservoir);

                        entityDictionary[columnReservoir.ToLower()] = reservoir;
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnWellCodeAnp) && !entityDictionary.TryGetValue(columnWellCodeAnp.ToLower(), out var well))
                {
                    well = await _context.Wells
                        .FirstOrDefaultAsync(x => x.CodWellAnp.ToLower() == columnWellCodeAnp.ToLower());

                    if (well is null && columnField is not null)
                    {
                        var wellId = Guid.NewGuid();

                        var fieldInDatabase = await _context.Fields
                            .FirstOrDefaultAsync(x => x.Name == columnField);

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
                            Field = entityDictionary.GetValueOrDefault(columnField.ToLower()) as Field is null ? fieldInDatabase : entityDictionary.GetValueOrDefault(columnField.ToLower()) as Field,
                        };

                        await _systemHistoryService
                        .Import<Well, WellHistoryDTO>(HistoryColumns.TableWells, user, data.FileName, well.Id, (Well)well);

                        entityDictionary[columnWellCodeAnp.ToLower()] = well;

                    }

                    else
                    {
                        var wellConverted = (Well)well;

                        var beforeChangesWell = _mapper.Map<WellHistoryDTO>(wellConverted);

                        var propertiesToUpdate = new WellUpdateImportViewModel
                        {
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

                        var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(wellConverted, propertiesToUpdate);

                        if (updatedProperties.Any() is true)
                        {

                            updatedDictionary[columnWellCodeAnp.ToLower()] = well;

                            await _systemHistoryService
                                .ImportUpdate(HistoryColumns.TableWells, user, data.FileName, updatedProperties, wellConverted.Id, wellConverted, beforeChangesWell);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnCompletion) && !string.IsNullOrWhiteSpace(columnWellCodeAnp) && !entityDictionary.TryGetValue(columnCompletion.ToLower(), out var completion))
                {
                    completion = await _context.Completions
                    .FirstOrDefaultAsync(x => x.Name == columnCompletion);

                    if (completion is null && columnCompletion is not null)
                    {
                        var completionId = Guid.NewGuid();

                        var wellInDatabase = await _context.Wells
                            .FirstOrDefaultAsync(x => x.CodWellAnp == columnWellCodeAnp);

                        completion = new Completion
                        {
                            Id = completionId,
                            Name = columnCompletion,
                            User = user,
                            CodCompletion = GenerateCode.Generate(columnCompletion),
                            Reservoir = columnReservoir is not null ? entityDictionary.GetValueOrDefault(columnReservoir.ToLower()) as Reservoir : null,
                            Well = entityDictionary.GetValueOrDefault(columnWellCodeAnp.ToLower()) as Well is null ? wellInDatabase : entityDictionary.GetValueOrDefault(columnWellCodeAnp.ToLower()) as Well,
                            IsActive = true
                        };

                        await _systemHistoryService
                             .Import<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, data.FileName, completion.Id, (Completion)completion);

                        entityDictionary[columnCompletion.ToLower()] = completion;
                    }
                }
            }

            if (entityDictionary.Values.Count <= 0 && updatedDictionary.Values.Count <= 0)
                throw new BadRequestException("Nenhum item foi adicionado ou atualizado.");


            await _context.AddRangeAsync(entityDictionary.Values);

            _context.UpdateRange(updatedDictionary.Values);

            await _context.SaveChangesAsync();
        }
    }
}
