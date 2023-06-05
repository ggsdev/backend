using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models.BaseModels;
using PRIO.Models.Clusters;
using PRIO.Models.Completions;
using PRIO.Models.Fields;
using PRIO.Models.Installations;
using PRIO.Models.Reservoirs;
using PRIO.Models.Wells;
using PRIO.Models.Zones;
using PRIO.Utils;
using PRIO.ViewModels.Files;
using System.Globalization;

namespace PRIO.Controllers
{
    [ApiController]
    public class XlsxController : ControllerBase
    {
        private Cluster? _lastFoundCluster;
        private Field? _lastFoundField;
        private Installation? _lastFoundInstallation;
        private Reservoir? _lastFoundReservoir;
        private Zone? _lastFoundZone;
        private Well? _lastFoundWell;

        [HttpPost("xlsx")]
        public async Task<IActionResult> PostBase64File([FromBody] RequestXslxViewModel data, [FromServices] DataContext context)
        {
            var basePath = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\";
            var pathXslx = basePath + "teste.xlsx";

            var contentBase64 = data.ContentBase64?.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            await System.IO.File.WriteAllBytesAsync(pathXslx, Convert.FromBase64String(contentBase64!));

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var fileInfo = new FileInfo(pathXslx);
            using (ExcelPackage package = new(fileInfo))
            {
                var workbook = package.Workbook;
                var worksheetTab = workbook.Worksheets[0];

                var dimension = worksheetTab.Dimension;

                var entityDictionary = new Dictionary<string, BaseModel>();
                var columnPositions = XlsUtils.GetColumnPositions(worksheetTab);

                for (int row = 2; row <= dimension.End.Row; row++)
                {
                    var columnCluster = worksheetTab.Cells[row, columnPositions[XlsUtils.ClusterColumnName]].Value?.ToString();

                    var columnInstallation = worksheetTab.Cells[row, columnPositions[XlsUtils.InstallationColumnName]].Value?.ToString();

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
                    var columnWellCategoryOperator = worksheetTab.Cells[row, columnPositions[XlsUtils.WellNameOperatorColumnName]].Value?.ToString();
                    var columnWellStatusOperator = worksheetTab.Cells[row, columnPositions[XlsUtils.WellStatusOperatorColumnName]].Value?.ToString();
                    bool? columnWellStatusOperatorBoolean = null;
                    if (columnWellStatusOperator?.ToLower() == "ativo")
                        columnWellStatusOperatorBoolean = true;
                    if (columnWellStatusOperator?.ToLower() == "inativo")
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
                            };

                            entityDictionary[columnCluster.ToLower()] = cluster;

                        }

                        _lastFoundCluster = (Cluster)cluster;
                    }

                    if (!string.IsNullOrWhiteSpace(columnInstallation) && !entityDictionary.TryGetValue(columnInstallation.ToLower(), out var installation))
                    {
                        installation = await context.Installations.FirstOrDefaultAsync(x => x.Name.ToLower() == columnInstallation.ToLower());

                        if (installation is null && _lastFoundCluster is not null)
                        {

                            installation = new Installation
                            {
                                Name = columnInstallation,
                                User = user,
                                Cluster = _lastFoundCluster,
                            };

                            entityDictionary[columnInstallation.ToLower()] = installation;

                        }

                        _lastFoundInstallation = installation is not null ? (Installation)installation : null;
                    }

                    if (!string.IsNullOrWhiteSpace(columnField) && !entityDictionary.TryGetValue(columnField.ToLower(), out var field))
                    {
                        field = await context.Fields.FirstOrDefaultAsync(x => x.Name.ToLower() == columnField.ToLower());

                        if (field is null && _lastFoundInstallation is not null)
                        {
                            field = new Field
                            {
                                Name = columnField,
                                User = user,
                                Installation = _lastFoundInstallation,
                                CodField = columnCodeField is not null ? columnCodeField : string.Empty
                            };

                            entityDictionary[columnField.ToLower()] = field;
                        }

                        _lastFoundField = field is not null ? (Field)field : null;
                    }

                    if (!string.IsNullOrWhiteSpace(columnZone) && !entityDictionary.TryGetValue(columnZone.ToLower(), out var zone))
                    {
                        zone = await context.Zones.FirstOrDefaultAsync(x => x.CodZone.ToLower() == columnZone.ToLower());
                        if (zone is null && _lastFoundField is not null)
                        {
                            zone = new Zone
                            {
                                CodZone = columnZone,
                                User = user,
                                Field = _lastFoundField
                            };

                            entityDictionary[columnZone.ToLower()] = zone;
                        }

                        _lastFoundZone = zone is not null ? (Zone)zone : null;
                    }

                    if (!string.IsNullOrWhiteSpace(columnReservoir) && !entityDictionary.TryGetValue(columnReservoir.ToLower(), out var reservoir))
                    {
                        reservoir = await context.Reservoirs.FirstOrDefaultAsync(x => x.Name.ToLower() == columnReservoir.ToLower());
                        if (reservoir is null && _lastFoundZone is not null)
                        {
                            reservoir = new Reservoir
                            {
                                Name = columnReservoir,
                                User = user,
                                Zone = _lastFoundZone
                            };

                            entityDictionary[columnReservoir.ToLower()] = reservoir;
                        }

                        _lastFoundReservoir = reservoir is not null ? (Reservoir)reservoir : null;
                    }

                    if (!string.IsNullOrWhiteSpace(columnWellCodeAnp) && !entityDictionary.TryGetValue(columnWellCodeAnp.ToLower(), out var well))
                    {
                        well = await context.Wells.FirstOrDefaultAsync(x => x.CodWellAnp.ToLower() == columnWellCodeAnp.ToLower());
                        if (well is null && _lastFoundField is not null)
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
                                WaterDepth = double.TryParse(columnWellWaterDepth?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var columnWellWaterDepthDouble) ? columnWellWaterDepthDouble : 0,
                                TopOfPerforated = double.TryParse(columnWellPerforationTopMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var topOfPerforated) ? topOfPerforated : 0,
                                BaseOfPerforated = double.TryParse(columnWellBottomPerforationMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var baseOfPerforated) ? baseOfPerforated : 0,
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
                                Field = _lastFoundField
                            };

                            entityDictionary[columnWellCodeAnp.ToLower()] = well;
                        }

                        _lastFoundWell = well is not null ? (Well)well : null;
                    }

                    if (!string.IsNullOrWhiteSpace(columnCompletion) && !string.IsNullOrWhiteSpace(columnWellNameAnp) && !entityDictionary.TryGetValue(columnCompletion.ToLower(), out var completion))
                    {
                        completion = await context.Completions.FirstOrDefaultAsync(x => x.Name.ToLower() == columnCompletion.ToLower());
                        if (completion is null && _lastFoundWell is not null)
                        {

                            completion = new Completion
                            {
                                Name = columnCompletion,
                                User = user,
                                Reservoir = columnReservoir is not null ? _lastFoundReservoir : null,
                                Well = _lastFoundWell
                            };
                            entityDictionary[columnCompletion.ToLower()] = completion;
                        }
                    }
                }
                try
                {
                    await context.AddRangeAsync(entityDictionary.Values);
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

            return Created("xlsx", new
            {
                Message = "File imported successfully"
            });
        }
    }
}