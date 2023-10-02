using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.PI.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using System.Text.Json;

namespace PRIOScheduler
{
    public class GetPIValues
    {
        private static readonly IDictionary<string, string> _envVars = DotEnv.Read();

        private static readonly string _connectionString = $"Server={_envVars["SERVER"]},{_envVars["PORT"]}\\{_envVars["SERVER_INSTANCE"]};Database={_envVars["DATABASE"]};User ID={_envVars["USER_ID"]};Password={_envVars["PASSWORD"]};Encrypt={_envVars["ENCRYPT"]}";

        public static async Task ExecuteAsync()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
               .UseSqlServer(_connectionString)
               .Options;
            using var dbContext = new DataContext(dbContextOptions);
            var dateToday = DateTime.UtcNow.AddHours(-3).Date;

            var attributes = await dbContext.Attributes
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

                        var well = await dbContext.Wells
                            .FirstOrDefaultAsync(x => x.Name.ToUpper().Trim() == atr.WellName.ToUpper().Trim() || x.WellOperatorName.ToUpper().Trim() == atr.WellName.ToUpper().Trim());

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


                await dbContext.AddRangeAsync(wellValuesList);
                await dbContext.AddRangeAsync(valuesList);

                await dbContext.SaveChangesAsync();
            }
            catch (HttpRequestException e)
            {
                Print($"HTTP request error: {e.Message}");
            }

        }

        public static void Execute()
        {
            ExecuteAsync().Wait();
        }
        private static void Print<T>(T text)
        {
            Console.WriteLine(text);
        }
    }
}
