using PRIO;
using PRIO.Data;
using PRIO.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

ConfigureMiddlewares(app);

void ConfigureServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddControllers();
    services.AddDbContext<DataContext>();
    services.AddScoped<TokenService>();
}
static void ConfigureMiddlewares(IApplicationBuilder app)
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseRouting();
}