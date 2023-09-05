using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Shared.Infra.EF;

namespace PRIOScheduler
{
    public static class UpdateDowntime
    {
        private static readonly IDictionary<string, string> _envVars = DotEnv.Read();

        private static readonly string _connectionString = $"Server={_envVars["SERVER"]},{_envVars["PORT"]}\\{_envVars["SERVER_INSTANCE"]};Database={_envVars["DATABASE"]};User ID={_envVars["USER_ID"]};Password={_envVars["PASSWORD"]};Encrypt={_envVars["ENCRYPT"]}";

        public static async Task ExecuteAsync()
        {
            try
            {
                var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                    .UseSqlServer(_connectionString)
                    .Options;

                using var dbContext = new DataContext(dbContextOptions);

                var dateYesterday = DateTime.UtcNow.AddHours(-3).Date.AddDays(-1);

                var production = await dbContext.Productions
                    .Include(p => p.WellProductions)
                    .FirstOrDefaultAsync(x => x.MeasuredAt.Date == dateYesterday);

                if (production is not null)
                {
                    var wellEvents = await dbContext.WellEvents
                        .Include(we => we.Well)
                        .Where(x => (production != null && x.StartDate <= production.MeasuredAt.Date && x.EndDate == null)
                        || (production != null && x.StartDate.Date <= production.MeasuredAt.Date && x.EndDate != null && production.MeasuredAt.Date >= x.EndDate.Value.Date))
                        .Where(x => x.EventStatus == "F")
                        .ToListAsync();

                    foreach (var wellEvent in wellEvents)
                    {
                        var totalDowtimeDaily = 0d;

                        if (wellEvent.StartDate < production.MeasuredAt && wellEvent.EndDate is not null && wellEvent.EndDate.Value.Date == production.MeasuredAt)
                        {
                            totalDowtimeDaily += ((wellEvent.EndDate.Value - production.MeasuredAt).TotalMinutes) / 60;
                        }
                        else if (wellEvent.StartDate < production.MeasuredAt && wellEvent.EndDate is not null && wellEvent.EndDate.Value.Date > production.MeasuredAt)
                        {
                            totalDowtimeDaily += 24;
                        }
                        else if (wellEvent.StartDate.Date == production.MeasuredAt.Date && wellEvent.EndDate is not null && wellEvent.EndDate.Value.Date == production.MeasuredAt.Date)
                        {
                            totalDowtimeDaily += ((wellEvent.EndDate.Value - wellEvent.StartDate).TotalMinutes) / 60;
                        }
                        else if (wellEvent.StartDate.Date == production.MeasuredAt.Date && wellEvent.EndDate is not null && wellEvent.EndDate.Value.Date > production.MeasuredAt.Date)
                        {
                            totalDowtimeDaily += ((production.MeasuredAt.AddDays(1) - wellEvent.StartDate).TotalMinutes) / 60;
                        }
                        else if (wellEvent.StartDate < production.MeasuredAt && wellEvent.EndDate is null)
                        {
                            totalDowtimeDaily += 24;
                        }
                        else if (wellEvent.StartDate.Date == production.MeasuredAt && wellEvent.EndDate is null)
                        {
                            totalDowtimeDaily += ((production.MeasuredAt.AddDays(1) - wellEvent.StartDate).TotalMinutes) / 60;
                        }


                    }

                }

                foreach (var wellProduction in production.WellProductions)
                {



                }

                Console.WriteLine($"Job executado com sucesso.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Algo aconteceu na execução do job: {ex.Message}");
            }

        }

        public static void Execute()
        {
            ExecuteAsync().Wait();
        }
    }
}
