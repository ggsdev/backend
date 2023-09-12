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

                            var FirstresultIntervalTimeSpan = (reason.StartDate.Date.AddDays(1).AddMilliseconds(-10) - reason.StartDate).TotalHours;
                            int FirstintervalHours = (int)FirstresultIntervalTimeSpan;
                            var FirstintervalMinutesDecimal = (FirstresultIntervalTimeSpan - FirstintervalHours) * 60;
                            int FirstintervalMinutes = (int)FirstintervalMinutesDecimal;
                            var FirstintervalSecondsDecimal = (FirstintervalMinutesDecimal - FirstintervalMinutes) * 60;
                            int FirstintervalSeconds = (int)FirstintervalSecondsDecimal;
                            string FirstReasonFormattedHours;
                            string firstFormattedMinutes = FirstintervalMinutes < 10 ? $"0{FirstintervalMinutes}" : FirstintervalMinutes.ToString();
                            string firstFormattedSecond = FirstintervalSeconds < 10 ? $"0{FirstintervalSeconds}" : FirstintervalSeconds.ToString();
                            if (FirstintervalHours >= 1000)
                            {
                                int digitCount = (int)Math.Floor(Math.Log10(FirstintervalHours)) + 1;
                                FirstReasonFormattedHours = FirstintervalHours.ToString(new string('0', digitCount));
                            }
                            else
                            {
                                FirstReasonFormattedHours = FirstintervalHours.ToString("00");
                            }
                            var FirstReasonFormattedTime = $"{FirstReasonFormattedHours}:{firstFormattedMinutes}:{firstFormattedSecond}";
                            reason.Interval = FirstReasonFormattedTime;

                            DateTime refStartDate = reason.StartDate.Date.AddDays(1);
                            DateTime refStartEnd = refStartDate.AddDays(1).AddMilliseconds(-10);

                            var resultIntervalTimeSpan = (refStartEnd - refStartDate).TotalHours;
                            int intervalHours = (int)resultIntervalTimeSpan;
                            var intervalMinutesDecimal = (resultIntervalTimeSpan - intervalHours) * 60;
                            int intervalMinutes = (int)intervalMinutesDecimal;
                            var intervalSecondsDecimal = (intervalMinutesDecimal - intervalMinutes) * 60;
                            int intervalSeconds = (int)intervalSecondsDecimal;


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

                                    string ReasonFormattedMinutes = intervalMinutes < 10 ? $"0{intervalMinutes}" : intervalMinutes.ToString();
                                    string ReasonFormattedSecond = intervalSeconds < 10 ? $"0{intervalSeconds}" : intervalSeconds.ToString();
                                    string ReasonFormattedHours;
                                    if (intervalHours >= 1000)
                                    {
                                        int digitCount = (int)Math.Floor(Math.Log10(intervalHours)) + 1;
                                        ReasonFormattedHours = intervalHours.ToString(new string('0', digitCount));
                                    }
                                    else
                                    {
                                        ReasonFormattedHours = intervalHours.ToString("00");
                                    }
                                    var reasonFormattedTime = $"{ReasonFormattedHours}:{ReasonFormattedMinutes}:{ReasonFormattedSecond}";
                                    newEventReason.Interval = reasonFormattedTime;
                                    refStartDate = newEventReason.StartDate.AddDays(1);
                                    refStartEnd = refStartDate.AddDays(1).AddMilliseconds(-10);
                                }

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