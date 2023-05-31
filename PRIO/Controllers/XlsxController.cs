using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels;
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
        private Completion? _lastFoundCompletion;
        private Well? _lastFoundWell;

        [HttpPost("xlsx")]
        public async Task<IActionResult> PostBase64File([FromBody] RequestXslxViewModel data, [FromServices] DataContext context)
        {
            var basePath = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\";
            var pathXslx = basePath + "teste.xlsx";

            var contentBase64 = data.ContentBase64.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            System.IO.File.WriteAllBytes(pathXslx, Convert.FromBase64String(contentBase64));

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

                var clusters = new List<Cluster>();
                var installations = new List<Installation>();
                var fields = new List<Field>();
                var reservoirs = new List<Reservoir>();
                var zones = new List<Zone>();
                var completions = new List<Completion>();
                var wells = new List<Well>();

                for (int row = 2; row <= dimension.End.Row; row++)
                {
                    var rowCluster = worksheetTab.Cells[row, 1].Value?.ToString();

                    var rowInstallation = worksheetTab.Cells[row, 3].Value?.ToString();

                    var rowField = worksheetTab.Cells[row, 2].Value?.ToString();
                    var rowCodeField = worksheetTab.Cells[row, 5].Value?.ToString();

                    var rowReservoir = worksheetTab.Cells[row, 6].Value?.ToString();

                    var rowZone = worksheetTab.Cells[row, 7].Value?.ToString();

                    var rowCompletion = worksheetTab.Cells[row, 9].Value?.ToString();

                    #region Well rows

                    var rowWellNameAnp = worksheetTab.Cells[row, 10].Value?.ToString();
                    var rowWellOperatorName = worksheetTab.Cells[row, 11].Value?.ToString();
                    var rowWellCodeAnp = worksheetTab.Cells[row, 12].Value?.ToString();
                    var rowWellCategoryAnp = worksheetTab.Cells[row, 13].Value?.ToString();
                    var rowWellCategoryReclassification = worksheetTab.Cells[row, 14].Value?.ToString();
                    var rowWellCategoryOperator = worksheetTab.Cells[row, 15].Value?.ToString();
                    var rowWellStatusOperator = worksheetTab.Cells[row, 16].Value?.ToString();
                    bool? rowWellStatusOperatorBoolean = null;
                    if (rowWellStatusOperator?.ToLower() == "ativo")
                        rowWellStatusOperatorBoolean = true;
                    if (rowWellStatusOperator?.ToLower() == "inativo")
                        rowWellStatusOperatorBoolean = false;
                    var rowWellProfile = worksheetTab.Cells[row, 17].Value?.ToString();
                    var rowWellWaterDepth = worksheetTab.Cells[row, 18].Value?.ToString();

                    var rowWellPerforationTopMd = worksheetTab.Cells[row, 19].Value?.ToString();
                    var rowWellBottomPerforationMd = worksheetTab.Cells[row, 20].Value?.ToString();
                    var rowWellArtificialLift = worksheetTab.Cells[row, 21].Value?.ToString();
                    var rowWellLatitude4c = worksheetTab.Cells[row, 22].Value?.ToString();
                    var rowWellLongitude4c = worksheetTab.Cells[row, 23].Value?.ToString();
                    var rowWellLatitudeDD = worksheetTab.Cells[row, 24].Value?.ToString();
                    var rowWellLongitudeDD = worksheetTab.Cells[row, 25].Value?.ToString();
                    var rowWellDatumHorizontal = worksheetTab.Cells[row, 26].Value?.ToString();
                    var rowWellTypeCoordinate = worksheetTab.Cells[row, 27].Value?.ToString();
                    var rowWellCoodX = worksheetTab.Cells[row, 28].Value?.ToString();
                    var rowWellCoordY = worksheetTab.Cells[row, 29].Value?.ToString();

                    #endregion

                    if (!string.IsNullOrWhiteSpace(rowCluster))
                    {
                        var clusterInReadingProcess = clusters.Find(x => x.Name.ToLower() == rowCluster.ToLower());
                        if (clusterInReadingProcess is null)
                        {

                            var clusterInDatabase = await context.Clusters.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == rowCluster.ToLower().Trim());
                            if (clusterInDatabase is null)
                            {
                                var cluster = new Cluster
                                {
                                    Name = rowCluster,
                                    User = user,
                                };


                                _lastFoundCluster = cluster;
                                clusters.Add(cluster);
                            }

                        }
                        else
                        {
                            _lastFoundCluster = clusterInReadingProcess;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowInstallation))
                    {
                        var installationInReadingProcess = installations.Find(f => f.Name.ToLower() == rowInstallation.ToLower());

                        if (installationInReadingProcess is null)
                        {
                            var installationInDatabase = await context.Installations.FirstOrDefaultAsync(x => x.Name.ToLower() == rowInstallation.ToLower());

                            if (installationInDatabase is null && _lastFoundCluster is not null)
                            {

                                var installation = new Installation
                                {
                                    Name = rowInstallation,
                                    User = user,
                                    Cluster = _lastFoundCluster,
                                };


                                _lastFoundInstallation = installation;
                                installations.Add(installation);

                            };
                        }
                        else
                        {
                            _lastFoundInstallation = installationInReadingProcess;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowField))
                    {
                        var fieldInReadingProcess = fields.Find(f => f.Name.ToLower() == rowField.ToLower());
                        if (fieldInReadingProcess is null)
                        {
                            var fieldInDatabase = await context.Fields.FirstOrDefaultAsync(x => x.Name.ToLower() == rowField.ToLower());

                            if (fieldInDatabase is null && _lastFoundInstallation is not null)
                            {
                                var field = new Field
                                {
                                    Name = rowField,
                                    User = user,
                                    Installation = _lastFoundInstallation,
                                    CodField = rowCodeField is not null ? rowCodeField : string.Empty
                                };

                                _lastFoundField = field;

                                fields.Add(field);
                            }

                        }
                        else
                        {
                            _lastFoundField = fieldInReadingProcess;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowZone))
                    {
                        var zoneInReadingProcess = zones.Find(x => x.CodZone.ToLower() == rowZone.ToLower());
                        if (zoneInReadingProcess is null)
                        {
                            var zoneInDatabase = await context.Zones.FirstOrDefaultAsync(x => x.CodZone.ToLower() == rowZone.ToLower());
                            if (zoneInDatabase is null && _lastFoundField is not null)
                            {
                                var zone = new Zone
                                {
                                    CodZone = rowZone,
                                    User = user,
                                    Field = _lastFoundField
                                };


                                _lastFoundZone = zone;

                                zones.Add(zone);
                            }

                        }
                        else
                        {
                            _lastFoundZone = zoneInReadingProcess;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowReservoir))
                    {
                        var reservoirInReadingProcess = reservoirs.Find(x => x.Name.ToLower() == rowReservoir.ToLower());
                        if (reservoirInReadingProcess is null)
                        {
                            var reservoirInDatabase = await context.Reservoirs.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == rowReservoir.ToLower().Trim());
                            if (reservoirInDatabase is null && _lastFoundZone is not null)
                            {
                                var reservoir = new Reservoir
                                {
                                    Name = rowReservoir,
                                    User = user,
                                    Zone = _lastFoundZone
                                };

                                _lastFoundReservoir = reservoir;

                                reservoirs.Add(reservoir);
                            }

                        }
                        else
                        {
                            _lastFoundReservoir = reservoirInReadingProcess;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowWellCodeAnp))
                    {
                        var wellInReadingProcess = wells.Find(x => x.CodWellAnp?.ToLower().Trim() == rowWellCodeAnp?.ToLower().Trim());

                        if (wellInReadingProcess is null)
                        {

                            var wellInDatabase = await context.Wells.FirstOrDefaultAsync(x => x.CodWellAnp!.ToLower().Trim() == rowWellCodeAnp!.ToLower().Trim());
                            if (wellInDatabase is null)
                            {
                                var well = new Well
                                {
                                    Name = rowWellNameAnp,
                                    WellOperatorName = rowWellOperatorName,
                                    CodWellAnp = rowWellCodeAnp,
                                    CategoryAnp = rowWellCategoryAnp,
                                    CategoryReclassificationAnp = rowWellCategoryReclassification,
                                    CategoryOperator = rowWellCategoryOperator,
                                    StatusOperator = rowWellStatusOperatorBoolean,
                                    Type = rowWellProfile,
                                    WaterDepth = double.TryParse(rowWellWaterDepth?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var rowWellWaterDepthDouble) ? rowWellWaterDepthDouble : 0,
                                    TopOfPerforated = double.TryParse(rowWellPerforationTopMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var topOfPerforated) ? topOfPerforated : 0,
                                    BaseOfPerforated = double.TryParse(rowWellBottomPerforationMd?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var baseOfPerforated) ? baseOfPerforated : 0,
                                    ArtificialLift = rowWellArtificialLift,
                                    Latitude4C = rowWellLatitude4c,
                                    Longitude4C = rowWellLongitude4c,
                                    LatitudeDD = rowWellLatitudeDD,
                                    LongitudeDD = rowWellLongitudeDD,
                                    DatumHorizontal = rowWellDatumHorizontal,
                                    TypeBaseCoordinate = rowWellTypeCoordinate,
                                    CoordX = rowWellCoodX,
                                    CoordY = rowWellCoordY,
                                    User = user,

                                };

                                _lastFoundWell = well;
                                wells.Add(well);
                            }
                        }
                        else
                        {
                            _lastFoundWell = wellInReadingProcess;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowCompletion) && !string.IsNullOrWhiteSpace(rowWellNameAnp))
                    {
                        var completionInReadingProcess = completions.Find(x => x.Name.ToLower() == rowCompletion.ToLower());
                        if (completionInReadingProcess is null)
                        {
                            var completionInDatabase = await context.Completions.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == rowCompletion.ToLower().Trim());
                            if (completionInDatabase is null && _lastFoundWell is not null)
                            {
                                var reservoir = reservoirs.Find(x => x.Name.ToLower() == rowReservoir?.ToLower());

                                var completion = new Completion
                                {
                                    Name = rowCompletion,
                                    User = user,
                                    Reservoir = reservoir,
                                    Well = _lastFoundWell
                                };

                                _lastFoundCompletion = completion;

                                completions.Add(completion);
                            }

                        }
                        else
                        {
                            _lastFoundCompletion = completionInReadingProcess;
                        }
                    }
                }
                try
                {
                    await context.AddRangeAsync(clusters);
                    await context.AddRangeAsync(installations);
                    await context.AddRangeAsync(fields);
                    await context.AddRangeAsync(zones);
                    await context.AddRangeAsync(reservoirs);
                    await context.AddRangeAsync(completions);
                    await context.AddRangeAsync(wells);
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