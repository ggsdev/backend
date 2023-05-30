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

        [HttpPost("xlsx")]
        public async Task<IActionResult> PostBase64File([FromBody] RequestXslxViewModel data, [FromServices] DataContext context)
        {
            var basePath = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO\\Files\\";
            var pathXslx = basePath + "teste.xlsx";

            var contentBase64 = data.ContentBase64.Replace("data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
            System.IO.File.WriteAllBytes(pathXslx, Convert.FromBase64String(contentBase64));

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            var fileInfo = new FileInfo(pathXslx);
            using (ExcelPackage package = new(fileInfo))
            {
                var workbook = package.Workbook;
                var worksheetTab = workbook.Worksheets[0];

                var dimension = worksheetTab.Dimension;

                var clusters = new List<Cluster>();
                var fields = new List<Field>();
                var installations = new List<Installation>();

                for (int row = 2; row <= dimension.End.Row; row++)
                {
                    var rowCluster = worksheetTab.Cells[row, 1].Value?.ToString();
                    var rowField = worksheetTab.Cells[row, 2].Value?.ToString();
                    var rowCodeField = worksheetTab.Cells[row, 5].Value?.ToString();
                    var rowInstallation = worksheetTab.Cells[row, 3].Value?.ToString();

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

                                await context.Clusters.AddAsync(cluster);

                                clusters.Add(cluster);
                            }

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowInstallation))
                    {
                        var installationInReadingProcessing = installations.Find(f => f.Name.ToLower() == rowInstallation.ToLower());

                        if (installationInReadingProcessing is null)
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


                                await context.Installations.AddAsync(installation);

                                installations.Add(installation);

                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowField) && !string.IsNullOrWhiteSpace(rowCodeField))
                    {
                        var fieldInReadingProcessing = fields.Find(f => f.Name.ToLower() == rowField.ToLower());
                        if (fieldInReadingProcessing is null)
                        {
                            var fieldInDatabase = await context.Fields.FirstOrDefaultAsync(x => x.Name.ToLower() == rowField.ToLower());

                            Console.WriteLine(_lastFoundCluster?.Name);
                            Console.WriteLine(_lastFoundInstallation?.Name);

                            if (fieldInDatabase is null && (_lastFoundCluster is not null && _lastFoundInstallation is not null))
                            {
                                var field = new Field
                                {
                                    Name = rowField,
                                    User = user,
                                    Installation = _lastFoundInstallation,
                                    Cluster = _lastFoundCluster,
                                    CodField = rowCodeField is not null ? rowCodeField : string.Empty
                                };

                                _lastFoundField = field;

                                await context.Fields.AddAsync(field);

                                fields.Add(field);
                            }

                        }
                    }
                }
                try
                {
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