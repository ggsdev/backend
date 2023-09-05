using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Shared.Infra.EF;
using System.Data;

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
                    .Include(x => x.EventReasons)
                    .Where(x => x.StartDate < dateToday && x.EndDate == null).Where(x => x.EventStatus == "F")
                    .ToListAsync();

                foreach (var wellEvent in wellEvents)
                {
                    for (int i = 0; i < wellEvent.EventReasons.Count; i++)
                    {
                        var reason = wellEvent.EventReasons[i];

                        if (reason.StartDate < dateToday && reason.EndDate is null)
                        {
                            reason.EndDate = dateToday.AddMilliseconds(-10);

                            var resultTimeSpan = (reason.EndDate.Value - reason.StartDate).TotalHours;

                            int hours = (int)resultTimeSpan;
                            var minutesDecimal = (resultTimeSpan - hours) * 60;
                            int minutes = (int)minutesDecimal;
                            var secondsDecimal = (minutesDecimal - minutes) * 60;
                            int seconds = (int)secondsDecimal;

                            string formattedHours;
                            if (hours >= 1000)
                            {
                                int digitCount = (int)Math.Floor(Math.Log10(hours)) + 1;
                                formattedHours = hours.ToString(new string('0', digitCount));
                            }
                            else
                            {
                                formattedHours = hours.ToString("00");
                            }
                            var formattedTime = $"{formattedHours}:{minutes}:{seconds}";

                            reason.Interval = formattedTime;
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
                        }
                    }
                }

                await dbContext.SaveChangesAsync();
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
