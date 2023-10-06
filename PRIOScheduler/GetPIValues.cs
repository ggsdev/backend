using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.PI.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using System.Net.Http.Headers;
using System.Text;
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

            Console.WriteLine("Começou a executar");

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using HttpClient client = new(handler);
                var username = "svc-pi-frade";
                var password = "S6_5q2C?=%ff";

                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                client.Timeout = Timeout.InfiniteTimeSpan;

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

                            if (well is not null)
                            {

                                ValueJson? valueObject = null;
                                try
                                {
                                    valueObject = JsonSerializer.Deserialize<ValueJson>(jsonContent, new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });


                                    Print($"tag: {atr.Name}");
                                    Print($"atributo webId: {atr.WebId}");

                                    if (valueObject is not null)
                                    {
                                        Print($"Value: {valueObject.Value}");
                                        Print($"Date: {valueObject.Timestamp}");

                                        var value = new Value
                                        {
                                            Id = Guid.NewGuid(),
                                            Amount = valueObject.Value,
                                            Attribute = atr,
                                            Date = valueObject.Timestamp.AddHours(-3),
                                            IsCaptured = true
                                        };

                                        valuesList.Add(value);

                                        var wellValue = new WellsValues
                                        {
                                            Id = Guid.NewGuid(),
                                            Value = value,
                                            Well = well,

                                        };

                                        wellValuesList.Add(wellValue);

                                    }

                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex);

                                    valueObject = new ValueJson
                                    {
                                        Value = null,
                                        Timestamp = DateTime.UtcNow.Date.AddSeconds(-1),
                                        Annotated = false,
                                        Good = false,
                                        Questionable = false,
                                        Substituted = false,
                                        UnitsAbbreviation = "",
                                        IsCaptured = false
                                    };
                                    var value = new Value
                                    {
                                        Id = Guid.NewGuid(),
                                        Amount = valueObject.Value,
                                        Attribute = atr,
                                        Date = valueObject.Timestamp,
                                        IsCaptured = false,

                                    };

                                    valuesList.Add(value);

                                    var wellValue = new WellsValues
                                    {
                                        Id = Guid.NewGuid(),
                                        Value = value,
                                        Well = well,
                                    };

                                    wellValuesList.Add(wellValue);
                                }

                            }
                        }

                        else
                        {
                            errorsList.Add($"Tag: {atr.Name}, Failed to retrieve data, Status: {response.StatusCode}, Message: {response.Content}");
                            Print($"Failed to retrieve data. Status code: {response.StatusCode}, Message: {response.Content}");
                        }
                    }

                    if (errorsList.Any())
                        throw new BadRequestException("Alguns erros na requisição", errors: errorsList);

                    await dbContext.AddRangeAsync(wellValuesList);
                    await dbContext.AddRangeAsync(valuesList);

                    Console.WriteLine("wellValuesList count: " + wellValuesList.Count);
                    Console.WriteLine("valuesList count: " + valuesList.Count);

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Print($"HTTP request error: {e}");
                }
            }
            catch (Exception e)
            {

                Print($"Outro error: {e}");

            }

            Console.WriteLine("Terminou de executar");


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
