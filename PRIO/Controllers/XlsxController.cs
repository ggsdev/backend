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
        private string? _lastFoundCodField;

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
                        var existingCluster = clusters.Find(c => c.Name.ToLower() == rowCluster.ToLower());
                        //var existingCluster = await context.Clusters.FirstOrDefaultAsync(x => x.Name.ToLower() == rowCluster.ToLower());

                        if (existingCluster is null)
                        {
                            var cluster = new Cluster
                            {
                                Name = rowCluster,
                                User = user,
                            };

                            clusters.Add(cluster);
                        }
                        else
                        {
                            _lastFoundCluster = existingCluster;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowField) && user is not null)
                    {
                        var existingField = fields.Find(f => f.Name.ToLower() == rowField.ToLower());

                        if (!string.IsNullOrWhiteSpace(rowCodeField))
                        {
                            _lastFoundCodField = rowCodeField;
                        }

                        if (existingField is null && _lastFoundCluster is not null)
                        {
                            var field = new Field
                            {
                                Name = rowField,
                                User = user,
                                Cluster = _lastFoundCluster,
                                CodField = _lastFoundCodField is not null ? _lastFoundCodField : string.Empty
                            };
                            fields.Add(field);
                        }
                        else
                        {
                            _lastFoundField = existingField;

                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowInstallation))
                    {
                        var existingInstallation = installations.Find(f => f.Name == rowInstallation);
                        _lastFoundInstallation = existingInstallation;

                        if (existingInstallation is null)
                        {
                            var existingField = fields.Find(c => c.Name == rowField);

                            if (existingField is not null)
                            {

                                var installation = new Installation
                                {
                                    Name = rowInstallation,
                                    User = user,
                                };

                                installations.Add(installation);

                            }
                        }
                    }
                }
                try
                {
                    await context.Installations.AddRangeAsync(installations);
                    await context.Fields.AddRangeAsync(fields);
                    await context.Clusters.AddRangeAsync(clusters);
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