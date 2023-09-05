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

                var dateToday = DateTime.UtcNow.AddHours(-3).Date;

                var wellEvents = await dbContext.WellEvents
                    .Where(x => (x.StartDate.Date < dateToday && x.EndDate == null))
                    .Where(x => x.EventStatus == "F")
                    .ToListAsync();

                Console.WriteLine(wellEvents.Any());




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
