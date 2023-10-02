﻿using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.PI.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using System.Text.Json;

namespace PRIO.src.Modules.PI.Infra.Http.Services
{
    public class PIService
    {
        DataContext _context;

        public PIService(DataContext context)
        {
            _context = context;
        }

        public async Task TestPI()
        {
            var attributes = await _context.Attributes
                .ToListAsync();

            var valuesList = new List<Value>();
            var wellValuesList = new List<WellsValues>();
            var errorsList = new List<string>();

            using HttpClient client = new();
            try
            {
                foreach (var atr in attributes)
                {
                    var valueRoute = atr.ValueRoute;
                    HttpResponseMessage response = await client.GetAsync(valueRoute);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        var well = await _context.Wells
                            .FirstOrDefaultAsync(x => x.Name.ToUpper().Trim() == atr.Name.ToUpper().Trim());

                        Value? valueObject = JsonSerializer.Deserialize<Value>(jsonContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (valueObject is not null && well is not null)
                        {
                            var value = new Value
                            {
                                Id = Guid.NewGuid(),
                                Amount = valueObject.Amount,
                                Attribute = valueObject.Attribute,
                                Date = valueObject.Date,

                            };

                            valuesList.Add(value);

                            var wellValue = new WellsValues
                            {
                                Id = Guid.NewGuid(),
                                Value = value,
                                Well = well,

                            };

                            wellValuesList.Add(wellValue);

                            Print($"Value: {valueObject.Amount}");
                            Print($"Date: {valueObject.Date}");
                        }

                    }

                    else
                    {
                        errorsList.Add($"Tag: {atr.Name}, Failed to retrieve data. Status code: {response.StatusCode}");
                        Print($"Failed to retrieve data. Status code: {response.StatusCode}, Message: {response.Content}");
                    }
                }

                if (errorsList.Any())
                    throw new BadRequestException("Alguns erros na requisição", errors: errorsList);


                await _context.AddRangeAsync(wellValuesList);
                await _context.AddRangeAsync(valuesList);

                await _context.SaveChangesAsync();
            }
            catch (HttpRequestException e)
            {
                Print($"HTTP request error: {e.Message}");
            }

        }

        private static void Print<T>(T text)
        {
            Console.WriteLine(text);
        }
    }
}
