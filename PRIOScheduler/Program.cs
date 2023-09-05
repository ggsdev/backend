using dotenv.net;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Shared.Infra.EF;
using PRIOScheduler;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<DataContext>();
builder.Services.AddDbContext<DataContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("dbConnection")));

//builder.Services.AddScoped<UpdateDowntime>();

DotEnv.Load();
var envVars = DotEnv.Read();
var app = builder.Build();

GlobalConfiguration.Configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage($"Server={envVars["SERVER"]},{envVars["PORT"]}\\{envVars["SERVER_INSTANCE"]};Database={envVars["DATABASE"]};User ID={envVars["USER_ID"]};Password={envVars["PASSWORD"]};Encrypt={envVars["ENCRYPT"]}");

RecurringJob.AddOrUpdate(
          "myrecurringjob",
          () => UpdateDowntime.Execute(),
          Cron.Minutely());

using (var server = new BackgroundJobServer())
{
    Console.WriteLine("Hangfire server started. Press Enter to exit.");
    Console.ReadLine();
}
