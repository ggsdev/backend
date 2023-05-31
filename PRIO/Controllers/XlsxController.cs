using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels;

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
                var fields = new List<Field>();
                var installations = new List<Installation>();
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

                    #region Well

                    var rowWellNameAnp = worksheetTab.Cells[row, 10].Value?.ToString();
                    var rowWellOperatorName = worksheetTab.Cells[row, 11].Value?.ToString();
                    var rowWellCodeAnp = worksheetTab.Cells[row, 12].Value?.ToString();
                    var rowWellCategoryAnp = worksheetTab.Cells[row, 13].Value?.ToString();
                    var rowWellCategoryReclassification = worksheetTab.Cells[row, 14].Value?.ToString();
                    var rowWellCategoryOperator = worksheetTab.Cells[row, 15].Value?.ToString();
                    var rowWellStatusOperator = worksheetTab.Cells[row, 16].Value?.ToString();
                    var rowWellProfile = worksheetTab.Cells[row, 17].Value?.ToString();
                    var rowWellWaterDepth = worksheetTab.Cells[row, 18].Value?.ToString();
                    var rowWellPerforationTopMd = worksheetTab.Cells[row, 19].Value?.ToString();
                    var rowWellBottomPerforationMd = worksheetTab.Cells[row, 20].Value?.ToString();
                    var rowWellArtificialLift = worksheetTab.Cells[row, 21].Value?.ToString();
                    var rowWellLatitudeBase4c = worksheetTab.Cells[row, 22].Value?.ToString();
                    var rowWellLongitudeBase4c = worksheetTab.Cells[row, 23].Value?.ToString();
                    var rowWell = worksheetTab.Cells[row, 24].Value?.ToString();
                    var rowWell = worksheetTab.Cells[row, 25].Value?.ToString();

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
                            else
                            {
                                _lastFoundCluster = clusterInReadingProcess;

                            }
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

                    if (!string.IsNullOrWhiteSpace(rowField) && !string.IsNullOrWhiteSpace(rowCodeField))
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
                            var zoneInDatabase = await context.Zones.FirstOrDefaultAsync(x => x.CodZone.ToLower().Trim() == rowZone.ToLower().Trim());
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
                            else
                            {
                                _lastFoundZone = zoneInReadingProcess;

                            }
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
                            else
                            {
                                _lastFoundReservoir = reservoirInReadingProcess;

                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowCompletion))
                    {
                        var completionInReadingProcess = completions.Find(x => x.Name.ToLower() == rowCompletion.ToLower());
                        if (completionInReadingProcess is null)
                        {
                            var completionInDatabase = await context.Completions.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == rowCompletion.ToLower().Trim());
                            if (completionInDatabase is null && _lastFoundReservoir is not null)
                            {
                                var completion = new Completion
                                {
                                    Name = rowCompletion,
                                    User = user,
                                    Reservoir = _lastFoundReservoir
                                };

                                _lastFoundCompletion = completion;
                                completions.Add(completion);
                            }
                            else
                            {
                                _lastFoundCompletion = completionInReadingProcess;

                            }
                        }
                    }



                    if (!string.IsNullOrWhiteSpace(rowCompletion))
                    {
                        var completionInReadingProcess = completions.Find(x => x.Name.ToLower() == rowCompletion.ToLower());
                        if (completionInReadingProcess is null)
                        {
                            var completionInDatabase = await context.Completions.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == rowCompletion.ToLower().Trim());
                            if (completionInDatabase is null && _lastFoundReservoir is not null)
                            {
                                var completion = new Completion
                                {
                                    Name = rowCompletion,
                                    User = user,
                                    Reservoir = _lastFoundReservoir
                                };

                                _lastFoundCompletion = completion;
                                completions.Add(completion);
                            }
                            else
                            {
                                _lastFoundCompletion = completionInReadingProcess;

                            }
                        }
                    }


                }
                try
                {
                    //await context.AddRangeAsync(installations);
                    //await context.AddRangeAsync(clusters);
                    //await context.AddRangeAsync(fields);
                    await context.AddRangeAsync(reservoirs);
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