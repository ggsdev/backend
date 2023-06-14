using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.XLSDTOS;
using PRIO.Filters;
using PRIO.Models.BaseModels;
using PRIO.Models.Clusters;
using PRIO.Models.Completions;
using PRIO.Models.Fields;
using PRIO.Models.Installations;
using PRIO.Models.Reservoirs;
using PRIO.Models.Users;
using PRIO.Models.Wells;
using PRIO.Models.Zones;
using PRIO.Utils;
using PRIO.ViewModels.Files;
using System.Globalization;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("xlsx")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class XlsxController : ControllerBase
    {
        private readonly string _consolidationInstance = "consolidador";
        [HttpPost]
        public async Task<IActionResult> PostBase64File([FromBody] RequestXslxViewModel data, [FromServices] DataContext context)
        {
            var user = HttpContext.Items["User"] as User;

            var envVars = DotEnv.Read();

            var contentBase64 = data.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            using (var stream = new MemoryStream(Convert.FromBase64String(contentBase64!)))
            using (ExcelPackage package = new(stream))
            {
                envVars.TryGetValue("INSTANCE", out var getInstanceName);
                getInstanceName ??= _consolidationInstance;

                var workbook = package.Workbook;
                var worksheetTab = workbook.Worksheets.FirstOrDefault(x => x.Name.ToLower().Trim() == "informações gerais poços");
                worksheetTab ??= workbook.Worksheets[0];

                var dimension = worksheetTab.Dimension;

                var entityDictionary = new Dictionary<string, BaseModel>();
                var entityHistoriesDictionary = new Dictionary<string, BaseHistoryModel>();

                var columnPositions = XlsUtils.GetColumnPositions(worksheetTab);

                for (int row = 2; row <= dimension.End.Row; ++row)
                {
                    var columnCluster = worksheetTab.Cells[row, columnPositions[XlsUtils.ClusterColumnName]].Value?.ToString();

                    if (!(columnCluster?.ToLower() == getInstanceName || getInstanceName.ToLower() == _consolidationInstance))
                        continue;

                    var columnInstallation = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationColumnName]].Value?.ToString();
                    var columnInstallationCodUep = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationCodUepColumnName]].Value?.ToString();

                    var columnField = worksheetTab.Cells[row, columnPositions[XlsUtils.FieldColumnName]].Value?.ToString();
                    var columnCodeField = worksheetTab.Cells[row, columnPositions[XlsUtils.FieldCodeColumnName]].Value?.ToString();

                    var columnReservoir = worksheetTab.Cells[row, columnPositions[XlsUtils.ReservoirColumnName]].Value?.ToString();

                    var columnZone = worksheetTab.Cells[row, columnPositions[XlsUtils.ZoneCodeColumnName]].Value?.ToString();

                    var columnCompletion = worksheetTab.Cells[row, columnPositions[XlsUtils.CompletionColumnName]].Value?.ToString();

                    #region Well rows
                    var columnWellNameAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellNameColumnName]].Value?.ToString();
                    var columnWellOperatorName = worksheetTab.Cells[row, columnPositions[XlsUtils.WellNameOperatorColumnName]].Value?.ToString();
                    var columnWellCodeAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCodeAnpColumnName]].Value?.ToString();
                    var columnWellCategoryAnp = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryAnpColumnName]].Value?.ToString();
                    var columnWellCategoryReclassification = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryReclassificationColumnName]].Value?.ToString();
                    var columnWellCategoryOperator = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCategoryOperatorColumnName]].Value?.ToString();
                    var columnWellStatusOperator = worksheetTab.Cells[row, columnPositions[XlsUtils.WellStatusOperatorColumnName]].Value?.ToString()?.ToLower();
                    bool? columnWellStatusOperatorBoolean = null;
                    if (columnWellStatusOperator is not null && columnWellStatusOperator.Contains("ativo"))
                        columnWellStatusOperatorBoolean = true;
                    if (columnWellStatusOperator is not null && columnWellStatusOperator.Contains("inativo"))
                        columnWellStatusOperatorBoolean = false;
                    var columnWellProfile = worksheetTab.Cells[row, columnPositions[XlsUtils.WellProfileColumnName]].Value?.ToString();
                    var columnWellWaterDepth = worksheetTab.Cells[row, columnPositions[XlsUtils.WellWaterDepthColumnName]].Value?.ToString();
                    var columnWellPerforationTopMd = worksheetTab.Cells[row, columnPositions[XlsUtils.WellPerforationTopMdColumnName]].Value?.ToString();
                    var columnWellBottomPerforationMd = worksheetTab.Cells[row, columnPositions[XlsUtils.WellBottomPerforationColumnName]].Value?.ToString();
                    var columnWellArtificialLift = worksheetTab.Cells[row, columnPositions[XlsUtils.WellArtificialLiftColumnName]].Value?.ToString();
                    var columnWellLatitude4c = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLatitude4cColumnName]].Value?.ToString();
                    var columnWellLongitude4c = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLongitude4cColumnName]].Value?.ToString();
                    var columnWellLatitudeDD = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLatitudeDDColumnName]].Value?.ToString();
                    var columnWellLongitudeDD = worksheetTab.Cells[row, columnPositions[XlsUtils.WellLongitudeDDColumnName]].Value?.ToString();
                    var columnWellDatumHorizontal = worksheetTab.Cells[row, columnPositions[XlsUtils.WellDatumHorizontalColumnName]].Value?.ToString();
                    var columnWellTypeCoordinate = worksheetTab.Cells[row, columnPositions[XlsUtils.WellTypeCoordinateColumnName]].Value?.ToString();
                    var columnWellCoordX = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCoordXColumnName]].Value?.ToString();
                    var columnWellCoordY = worksheetTab.Cells[row, columnPositions[XlsUtils.WellCoordYColumnName]].Value?.ToString();
                    #endregion


                    if (!string.IsNullOrWhiteSpace(columnCluster) && !entityDictionary.TryGetValue(columnCluster.ToLower(), out var cluster))
                    {
                        cluster = await context.Clusters.FirstOrDefaultAsync(x => x.Name.ToLower() == columnCluster.ToLower());
                        if (cluster is null)
                        {
                            cluster = new Cluster
                            {
                                Name = columnCluster,
                                User = user,
                                IsActive = true,
                                CodCluster = GenerateCode.Generate(columnCluster),
                            };

                            var clusterHistory = new ClusterHistory
                            {
                                Name = columnCluster,
                                User = user,
                                TypeOperation = TypeOperation.Import,
                                Cluster = (Cluster)cluster,
                            };

                            entityDictionary[columnCluster.ToLower()] = cluster;
                            entityHistoriesDictionary[columnCluster.ToLower()] = clusterHistory;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(columnInstallation) && !entityDictionary.TryGetValue(columnInstallation.ToLower(), out var installation))
                    {
                        installation = await context.Installations.FirstOrDefaultAsync(x => x.Name.ToLower() == columnInstallation.ToLower());

                        if (installation is null)
                        {
                            installation = new Installation
                            {
                                Name = columnInstallation,
                                CodInstallationUep = columnInstallationCodUep,
                                User = user,
                                IsActive = true,
                                Cluster = columnCluster is not null ? (Cluster)entityDictionary.GetValueOrDefault(columnCluster.ToLower())! : null,
                            };

                            var installationHistory = new InstallationHistory
                            {
                                Cluster = columnCluster is not null ? (Cluster)entityDictionary.GetValueOrDefault(columnCluster.ToLower())! : null,
                                CodInstallationUep = columnInstallationCodUep,
                                Name = columnInstallation,
                                User = user,
                                Installation = (Installation)installation,
                                TypeOperation = TypeOperation.Import
                            };

                            entityDictionary[columnInstallation.ToLower()] = installation;
                            entityHistoriesDictionary[columnInstallation.ToLower()] = installationHistory;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(columnField) && !entityDictionary.TryGetValue(columnField.ToLower(), out var field))
                    {
                        field = await context.Fields.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == columnField.ToLower().Trim());

                        if (field is null)
                        {
                            field = new Field
                            {
                                Name = columnField,
                                User = user,
                                Installation = columnInstallation is not null ? (Installation)entityDictionary.GetValueOrDefault(columnInstallation.ToLower())! : null,
                                CodField = columnCodeField,
                                IsActive = true,
                            };
                            var fieldHistory = new FieldHistory
                            {
                                Name = columnField,
                                CodField = columnCodeField,
                                User = user,
                                Field = (Field)field,
                                Installation = columnInstallation is not null ? (Installation)entityDictionary.GetValueOrDefault(columnInstallation.ToLower())! : null,
                                TypeOperation = TypeOperation.Import,
                            };

                            entityDictionary[columnField.ToLower()] = field;
                            entityHistoriesDictionary[columnField.ToLower()] = fieldHistory;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(columnZone) && !entityDictionary.TryGetValue(columnZone.ToLower(), out var zone))
                    {
                        zone = await context.Zones.FirstOrDefaultAsync(x => x.CodZone.ToLower() == columnZone.ToLower());
                        if (zone is null)
                        {
                            zone = new Zone
                            {
                                CodZone = columnZone,
                                User = user,
                                IsActive = true,
                                Field = columnField is not null ? (Field)entityDictionary.GetValueOrDefault(columnField.ToLower())! : null,
                            };

                            var zoneHistory = new ZoneHistory
                            {
                                CodZone = columnZone,
                                Field = columnField is not null ? (Field)entityDictionary.GetValueOrDefault(columnField.ToLower())! : null,
                                User = user,
                                Zone = (Zone)zone,

                                TypeOperation = TypeOperation.Import
                            };

                            entityDictionary[columnZone.ToLower()] = zone;
                            entityHistoriesDictionary[columnZone.ToLower()] = zoneHistory;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(columnReservoir) && !entityDictionary.TryGetValue(columnReservoir.ToLower(), out var reservoir))
                    {
                        reservoir = await context.Reservoirs.FirstOrDefaultAsync(x => x.Name.ToLower() == columnReservoir.ToLower());
                        if (reservoir is null)
                        {
                            reservoir = new Reservoir
                            {
                                Name = columnReservoir,
                                User = user,
                                CodReservoir = GenerateCode.Generate(columnReservoir),
                                Zone = columnZone is not null ? (Zone)entityDictionary.GetValueOrDefault(columnZone.ToLower())! : null,
                                IsActive = true,
                            };

                            var reservoirHistory = new ReservoirHistory
                            {
                                Name = columnReservoir,
                                User = user,
                                Reservoir = (Reservoir)reservoir,

                                Zone = columnZone is not null ? (Zone)entityDictionary.GetValueOrDefault(columnZone.ToLower())! : null,

                                TypeOperation = TypeOperation.Import,
                            };

                            entityDictionary[columnReservoir.ToLower()] = reservoir;
                            entityHistoriesDictionary[columnReservoir.ToLower()] = reservoirHistory;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(columnWellCodeAnp) && !entityDictionary.TryGetValue(columnWellCodeAnp.ToLower(), out var well))
                    {
                        well = await context.Wells.FirstOrDefaultAsync(x => x.CodWellAnp.ToLower() == columnWellCodeAnp.ToLower());
                        if (well is null)
                        {
                            well = new Well
                            {
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
                                Field = columnField is not null ? (Field)entityDictionary.GetValueOrDefault(columnField.ToLower())! : null,
                            };

                            var wellHistory = new WellHistory
                            {
                                Name = columnWellNameAnp,
                                WellOperatorName = columnWellOperatorName,
                                CodWellAnp = columnWellCodeAnp,
                                CategoryAnp = columnWellCategoryAnp,
                                CategoryReclassificationAnp = columnWellCategoryReclassification,
                                CategoryOperator = columnWellCategoryOperator,
                                StatusOperator = columnWellStatusOperatorBoolean,
                                Type = columnWellProfile,
                                WaterDepth = columnWellWaterDepthDouble,
                                TopOfPerforated = topOfPerforated,
                                BaseOfPerforated = baseOfPerforated,
                                ArtificialLift = columnWellArtificialLift,
                                Latitude4C = columnWellLatitude4c,
                                Longitude4C = columnWellLongitude4c,
                                LongitudeDD = columnWellLongitudeDD,
                                LatitudeDD = columnWellLatitudeDD,
                                DatumHorizontal = columnWellDatumHorizontal,
                                TypeBaseCoordinate = columnWellTypeCoordinate,
                                CoordX = columnWellCoordX,
                                CoordY = columnWellCoordY,
                                Field = columnField is not null ? (Field)entityDictionary.GetValueOrDefault(columnField.ToLower())! : null,
                                User = user,
                                TypeOperation = TypeOperation.Import,
                                Well = (Well)well,
                            };

                            entityDictionary[columnWellCodeAnp.ToLower()] = well;
                            entityHistoriesDictionary[columnWellCodeAnp.ToLower()] = wellHistory;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(columnCompletion) && !entityDictionary.TryGetValue(columnCompletion.ToLower(), out var completion))
                    {
                        completion = await context.Completions.FirstOrDefaultAsync(x => x.Name.ToLower() == columnCompletion.ToLower());
                        if (completion is null)
                        {
                            completion = new Completion
                            {
                                Name = columnCompletion,
                                User = user,
                                CodCompletion = GenerateCode.Generate(columnCompletion),
                                Reservoir = columnReservoir is not null ? (Reservoir)entityDictionary.GetValueOrDefault(columnReservoir?.ToLower())! : null,
                                Well = columnWellCodeAnp is not null ? (Well)entityDictionary.GetValueOrDefault(columnWellCodeAnp?.ToLower())! : null,
                                IsActive = true
                            };

                            var completionHistory = new CompletionHistory
                            {
                                Name = columnCompletion,
                                Reservoir = columnReservoir is not null ? (Reservoir)entityDictionary.GetValueOrDefault(columnReservoir?.ToLower())! : null,
                                Well = columnWellCodeAnp is not null ? (Well)entityDictionary.GetValueOrDefault(columnWellCodeAnp?.ToLower())! : null,
                                User = user,
                                Completion = (Completion)completion,
                                TypeOperation = TypeOperation.Import,
                            };
                            entityDictionary[columnCompletion.ToLower()] = completion;
                            entityHistoriesDictionary[columnCompletion.ToLower()] = completionHistory;
                        }
                    }
                }
                try
                {
                    await context.AddRangeAsync(entityDictionary.Values);
                    await context.AddRangeAsync(entityHistoriesDictionary.Values);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return StatusCode(500, new ErrorResponseDTO
                    {
                        Message = "Something went wrong when saving to the database"
                    });
                }
            }

            return Created("xlsx", new ImportResponseDTO
            {
                Message = "File imported successfully"
            });
        }
    }
}