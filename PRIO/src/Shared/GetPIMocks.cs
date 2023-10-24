using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.PI.Infra.EF.Models;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Utils;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PRIO.src.Shared
{
    public class GetPIMocks
    {
        private static readonly IDictionary<string, string> _envVars = DotEnv.Read();

        private static readonly string _connectionString = $"Server={_envVars["SERVER"]},{_envVars["PORT"]}\\{_envVars["SERVER_INSTANCE"]};Database={_envVars["DATABASE"]};User ID={_envVars["USER_ID"]};Password={_envVars["PASSWORD"]};Encrypt={_envVars["ENCRYPT"]}";

        public static async Task ExecuteAsync(string queryDate)
        {
            if (queryDate == null)
                throw new ConflictException("Data não informada.");

            var checkDate = DateTime.TryParse(queryDate, out DateTime day);
            if (checkDate is false)
                throw new ConflictException("Data não é válida.");

            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
               .UseSqlServer(_connectionString)
               .Options;

            using var dbContext = new DataContext(dbContextOptions);

            var dateToday = DateTime.UtcNow.Date.AddHours(3).AddSeconds(-1);
            var requestDate = day.Date.AddHours(3).AddSeconds(-1);
            Console.WriteLine("october" + requestDate);
            var attributes = await dbContext.Attributes.Include(x => x.Element)
               .ToListAsync();

            var errorsList = new List<string>();

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using HttpClient client = new(handler);
                var username = PIConfig._user;
                var password = PIConfig._password;

                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                client.Timeout = Timeout.InfiniteTimeSpan;
                for (var date = requestDate; date <= dateToday; date = date.AddDays(1))
                {
                    Console.WriteLine(date);

                    var valuesList = new List<Value>();
                    var wellValuesList = new List<WellsValues>();

                    var formattedDate = date.ToString("yyyy-MM-ddTHH:mm:ssZ");

                    try
                    {
                        foreach (var atr in attributes)
                        {
                            Print($"tag: {atr.Name}");

                            var valueRoute = $"{atr.ValueRoute}?time={formattedDate}";
                            HttpResponseMessage response = await client.GetAsync(valueRoute);

                            if (response.IsSuccessStatusCode)
                            {
                                string jsonContent = await response.Content.ReadAsStringAsync();

                                var wellNames = atr.WellName
                                    .Split(',');

                                var wellNamesCount = wellNames.Length;

                                foreach (var wellName in wellNames)
                                {
                                    var well = await dbContext.Wells
                                   .FirstOrDefaultAsync(x => x.Name.ToUpper().Trim() == wellName.ToUpper().Trim() || x.WellOperatorName.ToUpper().Trim() == wellName.ToUpper().Trim());

                                    if (well is not null)
                                    {
                                        ValueJson? valueObject = null;
                                        try
                                        {
                                            valueObject = JsonSerializer.Deserialize<ValueJson>(jsonContent, new JsonSerializerOptions
                                            {
                                                PropertyNameCaseInsensitive = true
                                            });

                                            if (valueObject is not null && valueObject.Timestamp == date)
                                            {
                                                Console.WriteLine("Capturou");
                                                var value = new Value
                                                {
                                                    Id = Guid.NewGuid(),
                                                    Amount = wellNamesCount > 1 ? 0 : valueObject.Value,
                                                    GroupAmount = wellNamesCount > 1 ? valueObject.Value : null,
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
                                            else if (valueObject is not null && valueObject.Timestamp != date)
                                            {
                                                Console.WriteLine("Capturou com data diferente");

                                                valueObject = new ValueJson
                                                {
                                                    Value = 0,
                                                    Timestamp = date.Date.AddSeconds(-1),
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
                                                    Amount = wellNamesCount > 1 ? null : 0,
                                                    GroupAmount = wellNamesCount > 1 ? 0 : null,
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
                                        catch (Exception ex)
                                        {

                                            Console.WriteLine("Capturou com erro no value");
                                            Console.WriteLine(ex);

                                            valueObject = new ValueJson
                                            {
                                                Value = null,
                                                Timestamp = date.Date.AddSeconds(-1),
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
                                                Amount = wellNamesCount > 1 ? null : 0,
                                                GroupAmount = wellNamesCount > 1 ? 0 : null,
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

                    try
                    {
                        // WFL1
                        var listAtrByDate = await dbContext.WellValues
                            .Include(wv => wv.Value)
                                .ThenInclude(v => v.Attribute)
                                .ThenInclude(a => a.Element)
                            .Include(wv => wv.Well)
                            .Where(wv => wv.Value.Date == date.AddHours(-3) && wv.Value.Attribute.Element.Parameter == "Vazão da WFL1")
                            .ToListAsync();

                        if (listAtrByDate is not null && listAtrByDate.Count > 0)
                        {
                            var potencialWells = 0d;

                            foreach (var wv in listAtrByDate)
                            {
                                var PDG1byWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Pressão PDG 1" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var PDG2byWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Pressão PDG 2" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var PDGValue = PDG1byWell is not null && PDG1byWell.Amount is not null ? PDG1byWell.Amount.Value : PDG2byWell is not null && PDG2byWell.Amount is not null ? PDG2byWell.Amount.Value : 0;

                                var iiByWell = await dbContext.InjectivityIndex
                                    .Include(ii => ii.ManualWellConfiguration)
                                        .ThenInclude(mwc => mwc.Well)
                                    .Where(ii => ii.ManualWellConfiguration.Well.WellOperatorName == wv.Well.WellOperatorName && ii.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var iiValue = iiByWell is not null ? iiByWell.Value : 0;

                                var buildUpByWell = await dbContext.BuildUp
                                    .Include(b => b.ManualWellConfiguration)
                                        .ThenInclude(mwc => mwc.Well)
                                    .Where(b => b.ManualWellConfiguration.Well.WellOperatorName == wv.Well.WellOperatorName && b.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var bValue = buildUpByWell is not null ? buildUpByWell.Value : 0;

                                if (wv.Value is not null)
                                {
                                    potencialWells += (PDGValue - bValue) * iiValue;
                                }
                            }

                            foreach (var wv in listAtrByDate)
                            {
                                var PDG1byWell = await dbContext.Values
                                   .Include(v => v.Attribute)
                                       .ThenInclude(a => a.Element)
                                   .Where(v => v.Date == date.AddHours(-3) &&
                                               v.Attribute.WellName == wv.Well.WellOperatorName &&
                                               v.Attribute.Element.Parameter == "Pressão PDG 1" &&
                                               v.Attribute.IsOperating == true)
                                   .FirstOrDefaultAsync();
                                var PDG2byWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Pressão PDG 2" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();

                                var PDGValue = PDG1byWell is not null && PDG1byWell.Amount is not null ? PDG1byWell.Amount.Value : PDG2byWell is not null && PDG2byWell.Amount is not null ? PDG2byWell.Amount.Value : 0;

                                var iiByWell = await dbContext.InjectivityIndex
                                    .Include(ii => ii.ManualWellConfiguration)
                                        .ThenInclude(mwc => mwc.Well)
                                    .Where(ii => ii.ManualWellConfiguration.Well.WellOperatorName == wv.Well.WellOperatorName && ii.IsOperating == true)
                                    .FirstOrDefaultAsync();

                                var iiValue = iiByWell is not null ? iiByWell.Value : 0;

                                var buildUpByWell = await dbContext.BuildUp
                                    .Include(b => b.ManualWellConfiguration)
                                        .ThenInclude(mwc => mwc.Well)
                                    .Where(b => b.ManualWellConfiguration.Well.WellOperatorName == wv.Well.WellOperatorName && b.IsOperating == true)
                                    .FirstOrDefaultAsync();

                                var bValue = buildUpByWell is not null ? buildUpByWell.Value : 0;

                                if (wv.Value is not null)
                                {
                                    var PIA = (PDGValue - bValue) * iiValue;
                                    var GroupAmount = wv.Value.GroupAmount is not null ? wv.Value.GroupAmount.Value : 0;
                                    wv.Value.Amount = potencialWells == 0 ? 0 : (PIA / potencialWells) * GroupAmount;
                                }
                            }

                            await dbContext.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        Print($"Outro error: {e}");
                    }

                    try
                    {
                        Console.WriteLine("GFL1");
                        // GFL1
                        var listAtrByDate = await dbContext.WellValues
                            .Include(wv => wv.Value)
                                .ThenInclude(v => v.Attribute)
                                .ThenInclude(a => a.Element)
                            .Include(wv => wv.Well)
                            .Where(wv => wv.Value.Date == date.AddHours(-3) && wv.Value.Attribute.Element.Parameter == "Vazão da GFL1")
                            .ToListAsync();

                        if (listAtrByDate is not null && listAtrByDate.Count > 0)
                        {
                            var potencialWells = 0d;
                            foreach (var wv in listAtrByDate)
                            {
                                var GasLiftbyWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Vazão de Gas Lift" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var GasLiftbyWellValue = GasLiftbyWell is not null && GasLiftbyWell.Amount is not null ? GasLiftbyWell.Amount.Value : 0;

                                if (wv.Value is not null)
                                    potencialWells += GasLiftbyWellValue;
                            }

                            Console.WriteLine("Total da vazao");
                            Console.WriteLine(potencialWells);

                            foreach (var wv in listAtrByDate)
                            {
                                var GasLiftbyWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Vazão de Gas Lift" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var GasLiftbyWellValue = GasLiftbyWell is not null && GasLiftbyWell.Amount is not null ? GasLiftbyWell.Amount.Value : 0;

                                if (wv.Value is not null)
                                {
                                    Console.WriteLine("Vazo de Gas lift");
                                    Console.WriteLine(GasLiftbyWellValue);

                                    var PIG = GasLiftbyWellValue / potencialWells;
                                    Console.WriteLine("Total Potencial do poço");
                                    Console.WriteLine(PIG);
                                    var GroupAmount = wv.Value.GroupAmount is not null ? wv.Value.GroupAmount.Value : 0;
                                    Console.WriteLine("GroupAmount");
                                    Console.WriteLine(GroupAmount);

                                    Console.WriteLine("Calculo final");
                                    Console.WriteLine(potencialWells == 0 ? 0 : PIG * GroupAmount);
                                    wv.Value.Amount = potencialWells == 0 ? 0 : PIG * GroupAmount;
                                }
                            }

                            await dbContext.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        Print($"HTTP request error: {e}");
                    }

                    try
                    {
                        // GFL4
                        var listAtrByDate = await dbContext.WellValues
                            .Include(wv => wv.Value)
                                .ThenInclude(v => v.Attribute)
                                .ThenInclude(a => a.Element)
                            .Include(wv => wv.Well)
                            .Where(wv => wv.Value.Date == date.AddHours(-3) && wv.Value.Attribute.Element.Parameter == "Vazão da GFL4")
                            .ToListAsync();

                        if (listAtrByDate is not null && listAtrByDate.Count > 0)
                        {
                            var potencialWells = 0d;
                            foreach (var wv in listAtrByDate)
                            {
                                var GasLiftbyWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Vazão de Gas Lift" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var GasLiftbyWellValue = GasLiftbyWell is not null && GasLiftbyWell.Amount is not null ? GasLiftbyWell.Amount.Value : 0;

                                if (wv.Value is not null)
                                    potencialWells += GasLiftbyWellValue;
                            }

                            foreach (var wv in listAtrByDate)
                            {
                                var GasLiftbyWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Vazão de Gas Lift" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var GasLiftbyWellValue = GasLiftbyWell is not null && GasLiftbyWell.Amount is not null ? GasLiftbyWell.Amount.Value : 0;

                                if (wv.Value is not null)
                                {
                                    var PIG = GasLiftbyWellValue / potencialWells;
                                    var GroupAmount = wv.Value.GroupAmount is not null ? wv.Value.GroupAmount.Value : 0;
                                    wv.Value.Amount = potencialWells == 0 ? 0 : PIG * GroupAmount;
                                }
                            }

                            await dbContext.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        Print($"HTTP request error: {e}");
                    }

                    try
                    {
                        // GFL6
                        var listAtrByDate = await dbContext.WellValues
                            .Include(wv => wv.Value)
                                .ThenInclude(v => v.Attribute)
                                .ThenInclude(a => a.Element)
                            .Include(wv => wv.Well)
                            .Where(wv => wv.Value.Date == date.AddHours(-3) && wv.Value.Attribute.Element.Parameter == "Vazão da GFL6")
                            .ToListAsync();

                        if (listAtrByDate is not null && listAtrByDate.Count > 0)
                        {
                            var potencialWells = 0d;
                            foreach (var wv in listAtrByDate)
                            {
                                var GasLiftbyWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Vazão de Gas Lift" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var GasLiftbyWellValue = GasLiftbyWell is not null && GasLiftbyWell.Amount is not null ? GasLiftbyWell.Amount.Value : 0;

                                if (wv.Value is not null)
                                    potencialWells += GasLiftbyWellValue;
                            }

                            foreach (var wv in listAtrByDate)
                            {
                                var GasLiftbyWell = await dbContext.Values
                                    .Include(v => v.Attribute)
                                        .ThenInclude(a => a.Element)
                                    .Where(v => v.Date == date.AddHours(-3) &&
                                                v.Attribute.WellName == wv.Well.WellOperatorName &&
                                                v.Attribute.Element.Parameter == "Vazão de Gas Lift" &&
                                                v.Attribute.IsOperating == true)
                                    .FirstOrDefaultAsync();
                                var GasLiftbyWellValue = GasLiftbyWell is not null && GasLiftbyWell.Amount is not null ? GasLiftbyWell.Amount.Value : 0;

                                if (wv.Value is not null)
                                {
                                    var PIG = GasLiftbyWellValue / potencialWells;
                                    var GroupAmount = wv.Value.GroupAmount is not null ? wv.Value.GroupAmount.Value : 0;
                                    wv.Value.Amount = potencialWells == 0 ? 0 : PIG * GroupAmount;
                                }
                            }

                            await dbContext.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        Print($"HTTP request error: {e}");
                    }
                }

            }
            catch (Exception e)
            {
                Print($"Outro error: {e}");
            }

            Console.WriteLine("Terminou de executar");
        }

        //public static void Execute()
        //{
        //    ExecuteAsync().Wait();
        //}
        private static void Print<T>(T text)
        {
            Console.WriteLine(text);
        }
    }
}
