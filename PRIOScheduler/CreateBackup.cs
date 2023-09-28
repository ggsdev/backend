using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIOScheduler
{
    public class CreateBackup
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
            string caminhoBackup = @"C:\backup\";
            string nomeCompletoArquivoBackup = Path.Combine(caminhoBackup, $"PRIO_BRAVO_{dateToday.ToString().Split(" ")[0].Replace("/", "-")}");

            try
            {

                var novoBackup = new Backup
                {
                    Id = Guid.NewGuid(),
                    Directory = nomeCompletoArquivoBackup,
                    date = dateToday
                };

                dbContext.Backups.Add(novoBackup);
                await dbContext.SaveChangesAsync();

                string comandoBackup = $"BACKUP DATABASE [{_envVars["DATABASE"]}] TO DISK = N'{nomeCompletoArquivoBackup}.bak'";
                dbContext.Database.ExecuteSqlRaw(comandoBackup);

                Console.WriteLine($"Backup do banco de dados realizado com sucesso. Arquivo: {nomeCompletoArquivoBackup}");
                return;
            }
            catch (Exception ex)
            {
                var found = dbContext.Backups.Where(x => x.date == dateToday);
                if (found is not null)
                {
                    Console.WriteLine($"Já existe um job gerado nesse dia: {ex.Message}");
                    return;
                }
            }
            return;
        }

        public static void Execute()
        {
            ExecuteAsync().Wait();
        }
    }
}
