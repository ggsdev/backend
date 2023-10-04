using AutoMapper;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.Hierarchy.Dtos;
using PRIO.src.Modules.FileImport.XLSX.Hierarchy.Utils;
using PRIO.src.Modules.FileImport.XLSX.Hierarchy.ViewModels;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly IWellEventRepository _wellEventRepository;
        private readonly IManualConfigRepository _manualConfigRepository;
        private readonly IUserRepository _userRepository;

        public XLSXService(IMapper mapper, DataContext context, SystemHistoryService systemHistoryService, IWellEventRepository wellEventRepository, IUserRepository userRepository, IManualConfigRepository manualConfigRepository)
        {
            _mapper = mapper;
            _context = context;
            _systemHistoryService = systemHistoryService;
            _wellEventRepository = wellEventRepository;
            _userRepository = userRepository;
            _manualConfigRepository = manualConfigRepository;
        }

        public async Task<ImportResponseDTO> ImportFiles(RequestXslxViewModel data, User user)
        {
            var envVars = DotEnv.Read();

            if (data.FileName.EndsWith(".xlsx") is false)
                throw new BadRequestException("O arquivo deve ter a extensão .xlsx", status: "Error");

            envVars.TryGetValue("INSTANCE", out var getInstanceName);

            if (getInstanceName is null)
                throw new BadRequestException("Cluster não encontrado na planilha.", status: "Error");

            var contentBase64 = data.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using var stream = new MemoryStream(Convert.FromBase64String(contentBase64!));
            using ExcelPackage package = new(stream);

            var errorCount = 0;
            var workbook = package.Workbook;
            var worksheetTab = workbook.Worksheets
                .FirstOrDefault(x => x.Name.ToLower().Trim() == "informações gerais poços");
            worksheetTab ??= workbook.Worksheets[0];

            var dimension = worksheetTab.Dimension;

            var entityDictionary = new Dictionary<string, BaseModel>();
            var updatedDictionary = new Dictionary<string, BaseModel>();

            var dateCurrent = DateTime.UtcNow.AddHours(-3);
            var columnPositions = XlsUtils.GetColumnPositions(worksheetTab);

            var errors = XlsUtils.ValidateColumns(worksheetTab);
            if (errors.Any())
                throw new BadRequestException("Alguma(s) coluna(s) de título da planilha não possuem o valor esperado.", status: "Error");

            for (int row = 2; row <= dimension.End.Row; ++row)
            {
                var cellCluster = worksheetTab.Cells[row, columnPositions[XlsUtils.ClusterColumnName]].Value?.ToString();

                if (cellCluster is not null && cellCluster.ToUpper().Trim().Contains(getInstanceName.ToUpper().Trim()) is false)
                    continue;

                var cellInstallationCod = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationCodColumnName]].Value?.ToString()?.Trim();

                var cellInstallation = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationColumnName]].Value?.ToString()?.Trim();

                var cellInstallationCodUep = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationCodUepColumnName]].Value?.ToString()?.Trim();

                var cellInstallationNameUep = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationNameUepColumnName]].Value?.ToString()?.Trim();

                var cellField = worksheetTab.Cells[row, columnPositions[XlsUtils.FieldColumnName]].Value?.ToString()?.Trim();

                var cellCodeField = worksheetTab.Cells[row, columnPositions[XlsUtils.FieldCodeColumnName]].Value?.ToString()?.Trim();


                #region Well rows
                var cellWellCodeAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCodeAnpColumnName]].Value?.ToString()?.Trim();

                var cellWellNameAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellNameColumnName]].Value?.ToString()?.Trim();
                var cellWellOperatorName = worksheetTab.Cells[row, columnPositions[XlsUtils.WellNameOperatorColumnName]].Value?.ToString()?.Trim();
                var cellWellCategoryAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryAnpColumnName]].Value?.ToString()?.Trim();
                var cellWellCategoryReclassification = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryReclassificationColumnName]].Value?.ToString()?.Trim();
                var cellWellCategoryOperator = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryOperatorColumnName]].Value?.ToString()?.Trim();
                var cellWellStatusOperator = worksheetTab.Cells[row, columnPositions[XlsUtils.WellStatusOperatorColumnName]].Value?.ToString()?.ToLower()?.Trim();
                bool cellWellStatusOperatorBoolean = true;
                if (cellWellStatusOperator is not null && cellWellStatusOperator.Contains("ativo"))
                    cellWellStatusOperatorBoolean = true;
                if (cellWellStatusOperator is not null && cellWellStatusOperator.Contains("inativo"))
                    cellWellStatusOperatorBoolean = false;
                var cellWellProfile = worksheetTab.Cells[row, columnPositions[XlsUtils.WellProfileColumnName]].Value?.ToString()?.Trim();
                var cellWellWaterDepth = worksheetTab.Cells[row, columnPositions[XlsUtils.WellWaterDepthColumnName]].Value?.ToString()?.Trim();
                var cellWellPerforationTopMd = worksheetTab.Cells[row, columnPositions[XlsUtils.WellPerforationTopMdColumnName]].Value?.ToString()?.Trim();
                var cellWellBottomPerforationMd = worksheetTab.Cells[row, columnPositions[XlsUtils.WellBottomPerforationColumnName]].Value?.ToString()?.Trim();
                var cellWellArtificialLift = worksheetTab.Cells[row, columnPositions[XlsUtils.WellArtificialLiftColumnName]].Value?.ToString()?.Trim();
                var cellWellLatitude4c = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLatitude4cColumnName]].Value?.ToString()?.Trim();
                var cellWellLongitude4c = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLongitude4cColumnName]].Value?.ToString()?.Trim();
                var cellcolumnWellLatitudeDD = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLatitudeDDColumnName]].Value?.ToString()?.Trim();
                var cellWellLongitudeDD = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLongitudeDDColumnName]].Value?.ToString()?.Trim();
                var cellWellDatumHorizontal = worksheetTab.Cells[row, columnPositions[XlsUtils.WellDatumHorizontalColumnName]].Value?.ToString()?.Trim();
                var cellWellTypeCoordinate = worksheetTab.Cells[row, columnPositions[XlsUtils.WellTypeCoordinateColumnName]].Value?.ToString()?.Trim();
                var cellWellCoordX = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCoordXColumnName]].Value?.ToString()?.Trim();
                var cellWellCoordY = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCoordYColumnName]].Value?.ToString()?.Trim();
                #endregion

                var columnZone = worksheetTab.Cells[row, columnPositions[XlsUtils.ZoneCodeColumnName]].Value?.ToString()?.Trim();

                var columnReservoir = worksheetTab.Cells[row, columnPositions[XlsUtils.ReservoirColumnName]].Value?.ToString()?.Trim();

                var columnAllocationByReservoir = worksheetTab.Cells[row, columnPositions[XlsUtils.AllocationByReservoirColumnName]].Value?.ToString()?.Trim();

                var columnCompletion = worksheetTab.Cells[row, columnPositions[XlsUtils.CompletionColumnName]].Value?.ToString()?.Trim();

                if (cellCluster is not null && cellCluster.ToUpper().Trim().Contains(getInstanceName.ToUpper().Trim()) is true)
                {
                    if (string.IsNullOrWhiteSpace(cellCluster) || string.IsNullOrWhiteSpace(cellInstallationCod) || string.IsNullOrWhiteSpace(cellInstallationCodUep) || string.IsNullOrWhiteSpace(cellInstallationNameUep) || string.IsNullOrWhiteSpace(cellCodeField) || string.IsNullOrWhiteSpace(columnZone) || string.IsNullOrWhiteSpace(columnReservoir) || string.IsNullOrWhiteSpace(cellWellCodeAnp))
                    {
                        errorCount++;
                        continue;
                    }
                }

                if (!string.IsNullOrWhiteSpace(cellCluster) && !entityDictionary.TryGetValue(cellCluster.ToLower(), out var cluster))
                {
                    cluster = await _context.Clusters
                        .FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == cellCluster.ToLower().Trim());

                    if (cluster is null)
                    {
                        var clusterId = Guid.NewGuid();
                        cluster = new Cluster
                        {
                            Id = clusterId,
                            Name = cellCluster,
                            User = user,
                            IsActive = true,
                        };

                        await _systemHistoryService
                            .Import<Cluster, ClusterHistoryDTO>(HistoryColumns.TableClusters, user, data.FileName, cluster.Id, (Cluster)cluster);

                        entityDictionary[cellCluster.ToLower()] = cluster;
                    }
                }

                if (!string.IsNullOrWhiteSpace(cellInstallationCod) && !string.IsNullOrWhiteSpace(cellCluster) && !entityDictionary.TryGetValue(cellInstallationCod.ToLower(), out var installation))
                {
                    installation = await _context.Installations
                        .FirstOrDefaultAsync(x => x.CodInstallationAnp.ToLower().Trim() == cellInstallationCod.ToLower().Trim());

                    if (installation is null)
                    {
                        var clusterInDatabase = await _context.Clusters
                            .FirstOrDefaultAsync(x => x.Name == cellCluster);

                        var installationId = Guid.NewGuid();
                        if (clusterInDatabase is not null && clusterInDatabase.IsActive)
                        {
                            installation = new Installation
                            {
                                Id = installationId,
                                Name = cellInstallation,
                                UepCod = cellInstallationCodUep,
                                CodInstallationAnp = cellInstallationCod,
                                UepName = cellInstallationNameUep,
                                User = user,
                                IsActive = true,
                                IsProcessingUnit = cellInstallationCodUep == cellInstallationCod,
                                Cluster = clusterInDatabase
                            };

                            if (cellInstallationCodUep == cellInstallationCod)
                            {

                                var createOilVolumeCalculation = new OilVolumeCalculation
                                {
                                    Id = Guid.NewGuid(),
                                    Installation = installation as Installation
                                };
                                await _context.OilVolumeCalculations.AddAsync(createOilVolumeCalculation);

                                var gasCalculation = new GasVolumeCalculation
                                {
                                    Id = Guid.NewGuid(),
                                    Installation = installation as Installation,
                                };
                                await _context.GasVolumeCalculations.AddAsync(gasCalculation);

                            }

                            var usersMaster = await _userRepository.GetAdminUsers();
                            foreach (var userMaster in usersMaster)
                            {
                                var InstallataionAccess = new InstallationsAccess
                                {
                                    Id = Guid.NewGuid(),
                                    Installation = installation as Installation,
                                    User = userMaster
                                };
                                await _context.InstallationsAccess.AddAsync(InstallataionAccess);
                            }
                        }
                        else if (clusterInDatabase is null && entityDictionary.GetValueOrDefault(cellCluster.ToLower()) is not null)
                        {
                            installation = new Installation
                            {
                                Id = installationId,
                                Name = cellInstallation,
                                UepCod = cellInstallationCodUep,
                                CodInstallationAnp = cellInstallationCod,
                                UepName = cellInstallationNameUep,
                                User = user,
                                IsActive = true,
                                IsProcessingUnit = cellInstallationCodUep == cellInstallationCod,
                                Cluster = entityDictionary.GetValueOrDefault(cellCluster.ToLower()) as Cluster
                            };

                            if (cellInstallationCodUep == cellInstallationCod)
                            {

                                var createOilVolumeCalculation = new OilVolumeCalculation
                                {
                                    Id = Guid.NewGuid(),
                                    Installation = installation as Installation
                                };
                                await _context.OilVolumeCalculations.AddAsync(createOilVolumeCalculation);

                                var gasCalculation = new GasVolumeCalculation
                                {
                                    Id = Guid.NewGuid(),
                                    Installation = installation as Installation,
                                };
                                await _context.GasVolumeCalculations.AddAsync(gasCalculation);

                            }

                            var usersMaster = await _userRepository.GetAdminUsers();
                            foreach (var userMaster in usersMaster)
                            {
                                var InstallataionAccess = new InstallationsAccess
                                {
                                    Id = Guid.NewGuid(),
                                    Installation = installation as Installation,
                                    User = userMaster
                                };
                                await _context.InstallationsAccess.AddAsync(InstallataionAccess);
                            }
                        }
                        if (installation is not null)
                        {
                            await _systemHistoryService
                                .Import<Installation, InstallationHistoryDTO>(HistoryColumns.TableInstallations, user, data.FileName, installation.Id, (Installation)installation);

                            entityDictionary[cellInstallationCod.ToLower()] = installation;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(cellCodeField) && !string.IsNullOrWhiteSpace(cellInstallationCod) && !entityDictionary.TryGetValue(cellCodeField.ToLower(), out var field))
                {
                    field = await _context.Fields
                        .FirstOrDefaultAsync(x => x.CodField.ToLower().Trim() == cellCodeField.ToLower().Trim());

                    if (field is null)
                    {
                        var installationInDatabase = await _context.Installations
                            .FirstOrDefaultAsync(x => x.CodInstallationAnp == cellInstallationCod);

                        var fieldId = Guid.NewGuid();
                        if (installationInDatabase is not null && installationInDatabase.IsActive)
                        {
                            field = new Field
                            {
                                Id = fieldId,
                                Name = cellField,
                                User = user,
                                Installation = installationInDatabase,
                                CodField = cellCodeField,
                                IsActive = true,
                            };

                        }
                        else if (installationInDatabase is null && entityDictionary.GetValueOrDefault(cellInstallationCod.ToLower()) is not null)
                        {
                            field = new Field
                            {
                                Id = fieldId,
                                Name = cellField,
                                User = user,
                                Installation = entityDictionary.GetValueOrDefault(cellInstallationCod.ToLower()) as Installation,
                                CodField = cellCodeField,
                                IsActive = true,
                            };
                        }
                        if (field is not null)
                        {
                            await _systemHistoryService
                             .Import<Field, FieldHistoryDTO>(HistoryColumns.TableFields, user, data.FileName, field.Id, (Field)field);

                            entityDictionary[cellCodeField.ToLower()] = field;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnZone) && !string.IsNullOrWhiteSpace(cellCodeField) && !entityDictionary.TryGetValue(columnZone.ToLower(), out var zone))
                {
                    zone = await _context.Zones
                        .FirstOrDefaultAsync(x => x.CodZone.ToLower().Trim() == columnZone.ToLower().Trim());

                    if (zone is null)
                    {
                        var fieldInDatabase = await _context.Fields
                        .FirstOrDefaultAsync(x => x.CodField == cellCodeField);

                        var zoneId = Guid.NewGuid();
                        if (fieldInDatabase is not null && fieldInDatabase.IsActive)
                        {
                            zone = new Zone
                            {
                                Id = zoneId,
                                CodZone = columnZone,
                                User = user,
                                IsActive = true,
                                Field = fieldInDatabase
                            };
                        }

                        else if (fieldInDatabase is null && entityDictionary.GetValueOrDefault(cellCodeField.ToLower()) is not null)
                        {
                            zone = new Zone
                            {
                                Id = zoneId,
                                CodZone = columnZone,
                                User = user,
                                IsActive = true,
                                Field = entityDictionary.GetValueOrDefault(cellCodeField.ToLower()) as Field
                            };
                        }
                        if (zone is not null)
                        {
                            await _systemHistoryService
                           .Import<Zone, ZoneHistoryDTO>(HistoryColumns.TableZones, user, data.FileName, zone.Id, (Zone)zone);

                            entityDictionary[columnZone.ToLower()] = zone;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(columnReservoir) && !string.IsNullOrWhiteSpace(columnZone) && !entityDictionary.TryGetValue(columnReservoir.ToLower(), out var reservoir))
                {
                    reservoir = await _context.Reservoirs
                        .FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == columnReservoir.ToLower().Trim());

                    if (reservoir is null)
                    {
                        var zoneInDatabase = await _context.Zones
                            .FirstOrDefaultAsync(x => x.CodZone == columnZone);
                        var reservoirId = Guid.NewGuid();
                        if (zoneInDatabase is not null && zoneInDatabase.IsActive)
                        {
                            reservoir = new Reservoir
                            {
                                Id = reservoirId,
                                Name = columnReservoir,
                                User = user,
                                Zone = zoneInDatabase,
                                IsActive = true,
                            };
                        }
                        else if (zoneInDatabase is null && entityDictionary.GetValueOrDefault(columnZone.ToLower()) is not null)
                        {
                            reservoir = new Reservoir
                            {
                                Id = reservoirId,
                                Name = columnReservoir,
                                User = user,
                                Zone = entityDictionary.GetValueOrDefault(columnZone.ToLower()) as Zone,
                                IsActive = true,
                            };
                        }
                        if (reservoir is not null)
                        {
                            await _systemHistoryService
                                .Import<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, data.FileName, reservoir.Id, (Reservoir)reservoir);

                            entityDictionary[columnReservoir.ToLower()] = reservoir;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(cellWellCodeAnp) && !string.IsNullOrWhiteSpace(cellCodeField) && !entityDictionary.TryGetValue(cellWellCodeAnp.ToLower(), out var well))
                {
                    well = await _context.Wells
                        .FirstOrDefaultAsync(x => x.CodWellAnp.ToLower() == cellWellCodeAnp.ToLower());

                    if (well is null)
                    {
                        var fieldInDatabase = await _context.Fields
                            .FirstOrDefaultAsync(x => x.CodField == cellCodeField);
                        var wellId = Guid.NewGuid();

                        if (fieldInDatabase is not null && fieldInDatabase.IsActive)
                        {
                            well = new Well
                            {
                                Id = wellId,
                                Name = cellWellNameAnp,
                                WellOperatorName = cellWellOperatorName,
                                CodWellAnp = cellWellCodeAnp,
                                CategoryAnp = cellWellCategoryAnp,
                                CategoryReclassificationAnp = cellWellCategoryReclassification,
                                CategoryOperator = cellWellCategoryOperator,
                                StatusOperator = cellWellStatusOperatorBoolean,
                                Type = cellWellProfile,
                                WaterDepth = decimal.TryParse(cellWellWaterDepth?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var cellWellWaterDepthDouble) ? cellWellWaterDepthDouble : null,
                                ArtificialLift = cellWellArtificialLift,
                                Latitude4C = cellWellLatitude4c,
                                Longitude4C = cellWellLongitude4c,
                                LatitudeDD = cellcolumnWellLatitudeDD,
                                LongitudeDD = cellWellLongitudeDD,
                                DatumHorizontal = cellWellDatumHorizontal,
                                TypeBaseCoordinate = cellWellTypeCoordinate,
                                CoordX = cellWellCoordX,
                                CoordY = cellWellCoordY,
                                User = user,
                                IsActive = cellWellStatusOperatorBoolean,
                                Field = fieldInDatabase,
                            };

                            var dotEnv = DotEnv.Read();
                            if (cellWellStatusOperatorBoolean)
                            {
                                var wellConv = (Well)well;

                                var containsStartDate = dotEnv.ContainsKey("APPLICATIONSTARTDATE");
                                if (containsStartDate)
                                {
                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?.Substring(0, 3)}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user

                                    };

                                    await _wellEventRepository.Add(wellEvent);
                                }
                                else
                                {
                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse("01/01/2023"),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?.Substring(0, 3)}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user
                                    };

                                    await _wellEventRepository.Add(wellEvent);
                                }
                            }

                            else
                            {
                                var wellConv = (Well)well;

                                var containsStartDate = dotEnv.ContainsKey("APPLICATIONSTARTDATE");
                                if (containsStartDate)
                                {
                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse("01/01/2023"),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?[..3]}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user,
                                        EventStatus = "A",
                                        EndDate = DateTime.Parse("01/01/2023"),
                                        Interval = 0d

                                    };

                                    var closingWellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse("01/01/2023"),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?[..3]}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user,
                                        EventStatus = "F",
                                        EventRelated = wellEvent,
                                    };

                                    var eventReason = new EventReason
                                    {
                                        SystemRelated = "Inativo",
                                        Id = Guid.NewGuid(),
                                        WellEvent = closingWellEvent,
                                        StartDate = DateTime.Parse("01/01/2023"),
                                        WellEventId = closingWellEvent.Id,
                                        CreatedBy = user,
                                    };


                                    await _wellEventRepository.Add(wellEvent);
                                    await _wellEventRepository.Add(closingWellEvent);
                                    await _wellEventRepository.AddReasonClosedEvent(eventReason);

                                }
                                else
                                {
                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?[..3]}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user,
                                        EventStatus = "A",
                                        EndDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        Interval = 0d

                                    };

                                    var closingWellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?.Substring(0, 3)}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user,
                                        EventStatus = "F",
                                        EventRelated = wellEvent,
                                    };

                                    var eventReason = new EventReason
                                    {
                                        SystemRelated = "Inativo",
                                        Id = Guid.NewGuid(),
                                        WellEvent = closingWellEvent,
                                        StartDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        WellEventId = closingWellEvent.Id,
                                        CreatedBy = user,
                                    };

                                    await _wellEventRepository.Add(wellEvent);
                                    await _wellEventRepository.Add(closingWellEvent);
                                    await _wellEventRepository.AddReasonClosedEvent(eventReason);
                                }
                            }

                            var manualConfig = new ManualWellConfiguration
                            {
                                Id = Guid.NewGuid(),
                                Well = (Well)well,
                            };
                            var injectivityIndex = new InjectivityIndex
                            {
                                Id = Guid.NewGuid(),
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                IsActive = false,
                                IsOperating = false,
                                Value = 0,
                                ManualWellConfiguration = manualConfig
                            };
                            var productivityIndex = new ProductivityIndex
                            {
                                Id = Guid.NewGuid(),
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                IsActive = false,
                                IsOperating = false,
                                Value = 0,
                                ManualWellConfiguration = manualConfig
                            };
                            var buildUp = new BuildUp
                            {
                                Id = Guid.NewGuid(),
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                IsActive = false,
                                IsOperating = false,
                                Value = 0,
                                ManualWellConfiguration = manualConfig
                            };

                            await _manualConfigRepository.AddConfigAsync(manualConfig);
                            await _manualConfigRepository.AddProductivityAsync(productivityIndex);
                            await _manualConfigRepository.AddInjectivityAsync(injectivityIndex);
                            await _manualConfigRepository.AddBuildUpAsync(buildUp);
                        }

                        else if (fieldInDatabase is null && entityDictionary.GetValueOrDefault(cellCodeField.ToLower()) is not null)
                        {
                            var fieldDict = entityDictionary.GetValueOrDefault(cellCodeField.ToLower()) as Field;

                            well = new Well
                            {
                                Id = wellId,
                                Name = cellWellNameAnp,
                                WellOperatorName = cellWellOperatorName,
                                CodWellAnp = cellWellCodeAnp,
                                CategoryAnp = cellWellCategoryAnp,
                                CategoryReclassificationAnp = cellWellCategoryReclassification,
                                CategoryOperator = cellWellCategoryOperator,
                                StatusOperator = cellWellStatusOperatorBoolean,
                                Type = cellWellProfile,
                                WaterDepth = decimal.TryParse(cellWellWaterDepth?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var cellWellWaterDepthDouble) ? cellWellWaterDepthDouble : null,
                                ArtificialLift = cellWellArtificialLift,
                                Latitude4C = cellWellLatitude4c,
                                Longitude4C = cellWellLongitude4c,
                                LatitudeDD = cellcolumnWellLatitudeDD,
                                LongitudeDD = cellWellLongitudeDD,
                                DatumHorizontal = cellWellDatumHorizontal,
                                TypeBaseCoordinate = cellWellTypeCoordinate,
                                CoordX = cellWellCoordX,
                                CoordY = cellWellCoordY,
                                User = user,
                                IsActive = cellWellStatusOperatorBoolean,
                                Field = fieldDict,
                            };

                            var dotEnv = DotEnv.Read();
                            if (cellWellStatusOperatorBoolean)
                            {
                                var containsStartDate = dotEnv.ContainsKey("APPLICATIONSTARTDATE");
                                var wellConv = (Well)well;

                                if (containsStartDate)
                                {
                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        IdAutoGenerated = $"{fieldDict?.Name?[..3]}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user

                                    };

                                    await _wellEventRepository.Add(wellEvent);
                                }
                                else
                                {
                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse("01/01/2023"),
                                        IdAutoGenerated = $"{fieldDict?.Name?[..3]}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user

                                    };

                                    await _wellEventRepository.Add(wellEvent);
                                }
                            }
                            else
                            {
                                var wellConv = (Well)well;

                                var containsStartDate = dotEnv.ContainsKey("APPLICATIONSTARTDATE");
                                if (containsStartDate)
                                {
                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse("01/01/2023"),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?[..3]}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user,
                                        EventStatus = "A",
                                        EndDate = DateTime.Parse("01/01/2023"),
                                        Interval = 0d

                                    };

                                    var closingWellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse("01/01/2023"),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?[..3]}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user,
                                        EventStatus = "F",
                                        EventRelated = wellEvent,
                                    };

                                    var eventReason = new EventReason
                                    {
                                        SystemRelated = "Inativo",
                                        Id = Guid.NewGuid(),
                                        WellEvent = closingWellEvent,
                                        StartDate = DateTime.Parse("01/01/2023"),
                                        WellEventId = closingWellEvent.Id,
                                        CreatedBy = user,
                                    };


                                    await _wellEventRepository.Add(wellEvent);
                                    await _wellEventRepository.Add(closingWellEvent);
                                    await _wellEventRepository.AddReasonClosedEvent(eventReason);

                                }
                                else
                                {
                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?[..3]}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user,
                                        EventStatus = "A",
                                        EndDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        Interval = 0d

                                    };

                                    var closingWellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        Well = (Well)well,
                                        StartDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        IdAutoGenerated = $"{fieldInDatabase?.Name?.Substring(0, 3)}0001",
                                        StatusANP = "Produzindo",
                                        StateANP = "1",
                                        CreatedBy = user,
                                        EventStatus = "F",
                                        EventRelated = wellEvent,
                                    };

                                    var eventReason = new EventReason
                                    {
                                        SystemRelated = "Inativo",
                                        Id = Guid.NewGuid(),
                                        WellEvent = closingWellEvent,
                                        StartDate = DateTime.Parse(dotEnv["APPLICATIONSTARTDATE"]),
                                        WellEventId = closingWellEvent.Id,
                                        CreatedBy = user,
                                    };

                                    await _wellEventRepository.Add(wellEvent);
                                    await _wellEventRepository.Add(closingWellEvent);
                                    await _wellEventRepository.AddReasonClosedEvent(eventReason);
                                }
                            }

                            var manualConfig = new ManualWellConfiguration
                            {
                                Id = Guid.NewGuid(),
                                Well = (Well)well,
                            };
                            var injectivityIndex = new InjectivityIndex
                            {
                                Id = Guid.NewGuid(),
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                IsActive = false,
                                IsOperating = false,
                                Value = 0,
                                ManualWellConfiguration = manualConfig
                            };
                            var productivityIndex = new ProductivityIndex
                            {
                                Id = Guid.NewGuid(),
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                IsActive = false,
                                IsOperating = false,
                                Value = 0,
                                ManualWellConfiguration = manualConfig
                            };
                            var buildUp = new BuildUp
                            {
                                Id = Guid.NewGuid(),
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                IsActive = false,
                                IsOperating = false,
                                Value = 0,
                                ManualWellConfiguration = manualConfig
                            };

                            await _manualConfigRepository.AddConfigAsync(manualConfig);
                            await _manualConfigRepository.AddProductivityAsync(productivityIndex);
                            await _manualConfigRepository.AddInjectivityAsync(injectivityIndex);
                            await _manualConfigRepository.AddBuildUpAsync(buildUp);

                        }
                        if (well is not null)
                        {
                            await _systemHistoryService
                            .Import<Well, WellHistoryDTO>(HistoryColumns.TableWells, user, data.FileName, well.Id, (Well)well);

                            entityDictionary[cellWellCodeAnp.ToLower()] = well;

                        }
                    }

                    //else
                    //{
                    //    var wellConverted = (Well)well;

                    //    var beforeChangesWell = _mapper.Map<WellHistoryDTO>(wellConverted);

                    //    var propertiesToUpdate = new WellUpdateImportViewModel
                    //    {
                    //        WellOperatorName = cellWellOperatorName,
                    //        CategoryAnp = cellWellCategoryAnp,
                    //        CategoryReclassificationAnp = cellWellCategoryReclassification,
                    //        CategoryOperator = cellWellCategoryOperator,
                    //        StatusOperator = cellWellStatusOperatorBoolean,
                    //        Type = cellWellProfile,
                    //        WaterDepth = decimal.TryParse(cellWellWaterDepth?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var cellWellWaterDepthDouble) ? cellWellWaterDepthDouble : 0,
                    //        ArtificialLift = cellWellArtificialLift,
                    //        Latitude4C = cellWellLatitude4c,
                    //        Longitude4C = cellWellLongitude4c,
                    //        LatitudeDD = cellcolumnWellLatitudeDD,
                    //        LongitudeDD = cellWellLongitudeDD,
                    //        DatumHorizontal = cellWellDatumHorizontal,
                    //        TypeBaseCoordinate = cellWellTypeCoordinate,
                    //        CoordX = cellWellCoordX,
                    //        CoordY = cellWellCoordY,
                    //        IsActive = cellWellStatusOperatorBoolean
                    //    };

                    //    var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(wellConverted, propertiesToUpdate);

                    //    if (updatedProperties.Any() is true)
                    //    {
                    //        updatedDictionary[cellWellCodeAnp.ToLower()] = well;

                    //        await _systemHistoryService
                    //            .ImportUpdate(HistoryColumns.TableWells, user, data.FileName, updatedProperties, wellConverted.Id, wellConverted, beforeChangesWell);
                    //    }
                    //}
                }

                if (!string.IsNullOrWhiteSpace(columnCompletion) && !string.IsNullOrWhiteSpace(columnReservoir) && !string.IsNullOrWhiteSpace(cellWellCodeAnp) && !entityDictionary.TryGetValue(columnCompletion.ToLower(), out var completion))
                {
                    completion = await _context.Completions
                    .FirstOrDefaultAsync(x => x.Name == columnCompletion);

                    if (completion is null)
                    {
                        if (columnCompletion is not null)
                        {
                            var wellInDatabase = await _context.Wells
                                .FirstOrDefaultAsync(x => x.CodWellAnp == cellWellCodeAnp);

                            var reservoirInDatabase = await _context.Reservoirs
                                .FirstOrDefaultAsync(x => x.Name.ToUpper() == columnReservoir.ToUpper());

                            var completionId = Guid.NewGuid();

                            if ((wellInDatabase is not null && wellInDatabase.IsActive) || (reservoirInDatabase is not null && reservoirInDatabase.IsActive))
                            {
                                completion = new Completion
                                {
                                    Id = completionId,
                                    Name = columnCompletion,
                                    User = user,
                                    AllocationReservoir = decimal.TryParse(columnAllocationByReservoir?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var allocation) ? allocation : 1,
                                    TopOfPerforated = decimal.TryParse(cellWellPerforationTopMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var topOfPerforated) ? topOfPerforated : null,
                                    BaseOfPerforated = decimal.TryParse(cellWellBottomPerforationMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var baseOfPerforated) ? baseOfPerforated : null,
                                    Reservoir = reservoirInDatabase is null ? entityDictionary.GetValueOrDefault(columnReservoir.ToLower()) as Reservoir : reservoirInDatabase,
                                    Well = wellInDatabase is null ? entityDictionary.GetValueOrDefault(cellWellCodeAnp.ToLower()) as Well : wellInDatabase,
                                    IsActive = true
                                };
                            }

                            else if ((wellInDatabase is null && entityDictionary.GetValueOrDefault(cellWellCodeAnp.ToLower()) is not null) && (entityDictionary.GetValueOrDefault(columnReservoir.ToLower()) is not null && reservoirInDatabase is null))
                            {

                                completion = new Completion
                                {
                                    Id = completionId,
                                    Name = columnCompletion,
                                    User = user,
                                    AllocationReservoir = decimal.TryParse(columnAllocationByReservoir?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var allocation) ? allocation : 1,
                                    TopOfPerforated = decimal.TryParse(cellWellPerforationTopMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var topOfPerforated) ? topOfPerforated : null,
                                    BaseOfPerforated = decimal.TryParse(cellWellBottomPerforationMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var baseOfPerforated) ? baseOfPerforated : null,
                                    Reservoir = entityDictionary.GetValueOrDefault(columnReservoir.ToLower()) as Reservoir,
                                    Well = entityDictionary.GetValueOrDefault(cellWellCodeAnp.ToLower()) as Well,
                                    IsActive = true
                                };
                            }

                            if (completion is not null)
                            {
                                await _systemHistoryService
                                     .Import<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, data.FileName, completion.Id, (Completion)completion);

                                entityDictionary[columnCompletion.ToLower()] = completion;
                            }
                        }
                    }
                    //else
                    //{
                    //    var completionConverted = (Completion)completion;

                    //    var beforeChangesCompletion = _mapper.Map<CompletionHistoryDTO>(completionConverted);

                    //    var propertiesToUpdate = new UpdateCompletionViewModel
                    //    {
                    //        AllocationReservoir = decimal.TryParse(columnAllocationByReservoir?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var allocation) ? allocation : 1,
                    //    };

                    //    var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(completionConverted, propertiesToUpdate);

                    //    if (updatedProperties.Any() is true)
                    //    {
                    //        updatedDictionary[cellWellCodeAnp.ToLower()] = completion;

                    //        await _systemHistoryService
                    //            .ImportUpdate(HistoryColumns.TableWells, user, data.FileName, updatedProperties, completionConverted.Id, completionConverted, beforeChangesCompletion);
                    //    }
                    //}
                }
            }

            if (entityDictionary.Values.Count <= 0 && updatedDictionary.Values.Count <= 0 && errorCount >= 1)
                throw new BadRequestException($"Nenhum item foi adicionado ou atualizado, tiveram: {errorCount} poços com informações obrigatórias em branco.", status: "Error");

            if (entityDictionary.Values.Count <= 0 && updatedDictionary.Values.Count <= 0)
                throw new BadRequestException("Nenhum item foi adicionado ou atualizado.", status: "Error");

            await _context.AddRangeAsync(entityDictionary.Values);

            _context.UpdateRange(updatedDictionary.Values);

            await _context.SaveChangesAsync();

            if ((entityDictionary.Values.Count >= 0 || updatedDictionary.Values.Count >= 0) && errorCount >= 1)
                return new ImportResponseDTO { Status = "Warning", Message = $"Arquivo importado com sucesso, porém tiveram: {errorCount} poços não importados com informações obrigatórias em branco." };

            return new ImportResponseDTO { Status = "Success", Message = "Arquivo importado com sucesso." };
        }
    }
}
