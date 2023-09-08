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
                            var dif = (dateToday - reason.StartDate).TotalHours / 24;
                            reason.EndDate = reason.StartDate.Date.AddDays(1).AddMilliseconds(-10);

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

                            DateTime refStartDate = reason.StartDate.Date.AddDays(1);
                            DateTime refStartEnd = refStartDate.AddDays(1).AddMilliseconds(-10);
                            for (int j = 0; j < dif; j++)
                            {
                                var rest = dif - j;
                                var newEventReason = new EventReason
                                {
                                    Id = Guid.NewGuid(),
                                    SystemRelated = reason.SystemRelated,
                                    Comment = reason.Comment,
                                    WellEvent = wellEvent,
                                    StartDate = refStartDate,
                                    IsActive = true,
                                    IsJobGenerated = true,
                                };
                                if (rest < 1)
                                {
                                    newEventReason.EndDate = null;
                                }
                                else
                                {
                                    newEventReason.EndDate = refStartEnd;

                                    var resultReasonTimeSpan = (newEventReason.EndDate.Value - newEventReason.StartDate).TotalHours;
                                    int reasonHours = (int)resultReasonTimeSpan;
                                    var reasonMinutesDecimal = (resultReasonTimeSpan - reasonHours) * 60;
                                    int reasonMinutes = (int)reasonMinutesDecimal;
                                    var reasonSecondsDecimal = (reasonMinutesDecimal - reasonMinutes) * 60;
                                    int reasonSeconds = (int)reasonSecondsDecimal;
                                    string ReasonFormattedHours;
                                    if (reasonHours >= 1000)
                                    {
                                        int digitCount = (int)Math.Floor(Math.Log10(hours)) + 1;
                                        ReasonFormattedHours = hours.ToString(new string('0', digitCount));
                                    }
                                    else
                                    {
                                        ReasonFormattedHours = hours.ToString("00");
                                    }
                                    var reasonFormattedTime = $"{formattedHours}:{minutes}:{seconds}";
                                    newEventReason.Interval = reasonFormattedTime;

                                }
                                refStartDate = newEventReason.StartDate.AddDays(1);
                                refStartEnd = refStartDate.AddMilliseconds(-10);

                                await dbContext.EventReasons.AddAsync(newEventReason);
                            }
                        }
                    }
                }

                await dbContext.SaveChangesAsync();
                Console.WriteLine($"Job UpdateDowntime executado em {dateToday} com sucesso.");
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