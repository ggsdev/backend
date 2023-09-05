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
                var wellTests = await dbContext.WellEvents
                    .Where(
                    x => x.StartDate < dateYesterday && x.EndDate != null && x.EndDate.Value.Date == dateYesterday ||
                    x.StartDate < dateYesterday && x.EndDate != null && x.EndDate.Value.Date > dateYesterday ||
                    x.StartDate.Date == dateYesterday.Date && x.EndDate != null && x.EndDate.Value.Date == dateYesterday.Date ||
                    x.StartDate.Date == dateYesterday.Date && x.EndDate != null && x.EndDate.Value.Date > dateYesterday.Date ||
                    x.StartDate < dateYesterday && x.EndDate == null ||
                    x.StartDate.Date == dateYesterday && x.EndDate == null
                )
                .ToListAsync();

                Console.WriteLine(wellTests.Count);

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
