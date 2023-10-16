using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Shared.Infra.EF;

namespace PRIOScheduler
{
    public class UpdateWellTest
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

                var dateToday = DateTime.UtcNow.Date;
                var dateSplit = dateToday.ToString().Split(" ")[0];

                var wellTests = await dbContext.WellTests
                    .Include(x => x.Well)
                    .Where(x => x.ApplicationDate == dateSplit && x.IsValid == true && x.IsActive == false)
                    .ToListAsync();

                foreach (var wellTest in wellTests)
                {
                    var oldWellTest = await dbContext.WellTests
                        .Include(x => x.Well)
                        .Where(x => x.Well.Id == wellTest.Well.Id && x.IsValid == true && x.IsActive == true)
                        .FirstOrDefaultAsync();

                    if (oldWellTest is not null)
                        oldWellTest.IsActive = false;

                    wellTest.IsActive = true;
                }

                await dbContext.SaveChangesAsync();
                Console.WriteLine($"Job UpdateWellTest executado em {dateToday} com sucesso.");
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