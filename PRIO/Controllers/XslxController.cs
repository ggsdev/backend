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
    public class XslxController : ControllerBase
    {
        private string _lastFoundCluster = string.Empty;
        private string _lastFoundField = string.Empty;

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
                var worksheetHierarchy = workbook.Worksheets[0];

                var dimension = worksheetHierarchy.Dimension;

                var clusters = new List<Cluster>();
                var fields = new List<Field>();

                for (int row = 4; row <= dimension.End.Row; row++)
                {
                    var rowCluster = worksheetHierarchy.Cells[row, 1].Value?.ToString();
                    var rowField = worksheetHierarchy.Cells[row, 2].Value?.ToString();
                    var rowCodeField = worksheetHierarchy.Cells[row, 8].Value?.ToString();

                    if (!string.IsNullOrWhiteSpace(rowCluster) && rowCluster.ToLower() != "(vazio)")
                    {
                        _lastFoundCluster = rowCluster;
                        var existingCluster = await context.Clusters.FirstOrDefaultAsync(c => c.Name == rowCluster);

                        if (existingCluster is null)
                        {
                            var cluster = new Cluster
                            {
                                Name = rowCluster,
                                User = user,
                            };
                            clusters.Add(cluster);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(rowField) && rowField.ToLower() != "(vazio)")
                    {
                        var existingField = await context.Fields.FirstOrDefaultAsync(c => c.Name == rowField);
                        if (existingField is null)
                        {
                            var existingCluster = clusters.Find(c => c.Name == (rowCluster ?? _lastFoundCluster));
                            _lastFoundField = rowField;

                            if (existingCluster is not null)
                            {
                                var field = new Field
                                {
                                    Name = rowField,
                                    User = user,
                                    Cluster = existingCluster,
                                    CodField = rowCodeField is not null && rowCodeField.ToLower() != "(vazio)" ? rowCodeField : string.Empty
                                };
                                fields.Add(field);
                            }
                        }
                    }
                }
                try
                {
                    await context.Fields.AddRangeAsync(fields);
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