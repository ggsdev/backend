using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
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
                var wellEvents = await dbContext.WellEvents.Include(x => x.Reason)
                    .Where(x => x.StartDate < dateToday && x.EndDate == null).Where(x => x.EventStatus == "F")
                .ToListAsync();

                foreach (var wellEvent in wellEvents)
                {
                    foreach (var reason in wellEvent.EventReasons)
                    {
                        if (reason.StartDate < dateToday && reason.EndDate is null)
                        {
                            reason.EndDate = dateToday.AddMilliseconds(-1);
                            reason.Interval = (reason.EndDate - reason.StartDate).ToString();
                            var newEventReason = new EventReason
                            {
                                Id = Guid.NewGuid(),
                                SystemRelated = reason.SystemRelated,
                                Comment = reason.Comment,
                                WellEvent = wellEvent,
                                StartDate = dateToday,
                                IsActive = true,
                            };
                            await dbContext.EventReasons.AddAsync(newEventReason);
                            //await dbContext.SaveChangesAsync();
                        }
                    }
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
