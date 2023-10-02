using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.PI.Infra.EF.Models;
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

            Console.WriteLine(attributes.Count);
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

                        var well = await _context.Wells.FirstOrDefaultAsync(x => x.Name == atr.Name);

                        // Deserialize the JSON content into a 'Value' object
                        Value valueObject = JsonSerializer.Deserialize<Value>(jsonContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        var value = new Value
                        {
                            Id = Guid.NewGuid(),
                            Amount = valueObject.Amount,
                            Attribute = valueObject.Attribute,
                            Date = valueObject.Date,

                        };

                        var wellValue = new WellsValues
                        {
                            Id = Guid.NewGuid(),
                            Value = value,
                            Well = well,

                        };

                        Console.WriteLine($"Amount: {valueObject.Amount}");
                        Console.WriteLine($"Date: {valueObject.Date}");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }

                    Print(atr.WebId);
                }


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP request error: {e.Message}");
            }

        }

        private static void Print<T>(T text)
        {
            Console.WriteLine(text);
        }
    }
}
