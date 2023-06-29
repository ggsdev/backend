using AutoMapper;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Repositories;
using PRIO.src.Modules.ControlAccess.Groups.Infra.Http.Services;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Repositories;
using PRIO.src.Modules.ControlAccess.Menus.Infra.Http.Services;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Repositories;
using PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XLSX.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Completions.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Installations.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Wells.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Zones.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Equipments.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Shared.Auxiliaries.Infra.Http.Services;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.Http.Filters;
using PRIO.src.Shared.Infra.Http.Middlewares;
using PRIO.src.Shared.Infra.Http.Services;
using PRIO.src.Shared.SystemHistories.Infra.EF.Repositories;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils.MappingProfiles;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

IConfiguration configuration = builder.Configuration;
ConfigureServices(builder.Services, configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ConfigureMiddlewares(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    var envVars = DotEnv.Read();

    services.AddControllers(config =>
    {
        var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
        config.Filters.Add(new AuthorizeFilter(policy));
        //config.ModelBinderProviders.Insert(0, new GuidBinderProvider());
    });

    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder
                .SetIsOriginAllowed(origin => true) // allow any origin
          .AllowAnyHeader()
          .AllowCredentials()
          .WithMethods("GET", "PATCH", "POST", "DELETE", "OPTIONS")
          .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
        });
    });

    var mapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<MainProfile>();
    });

    IMapper mapper = mapperConfig.CreateMapper();

    services.AddSingleton(mapper);
    services.AddEndpointsApiExplorer();
    services.AddDbContext<DataContext>();
    services.AddScoped<AuthorizationFilter>();

    services.AddScoped<TokenServices>();

    #region Hierarchy Repositories
    services.AddScoped<IFieldRepository, FieldRepository>();

    services.AddScoped<ISystemHistoryRepository, SystemHistoryRepository>();

    services.AddScoped<IClusterRepository, ClusterRepository>();

    services.AddScoped<IInstallationRepository, InstallationRepository>();

    services.AddScoped<IFieldRepository, FieldRepository>();

    services.AddScoped<IZoneRepository, ZoneRepository>();

    services.AddScoped<IReservoirRepository, ReservoirRepository>();

    services.AddScoped<IWellRepository, WellRepository>();

    services.AddScoped<IEquipmentRepository, EquipmentRepository>();

    services.AddScoped<ICompletionRepository, CompletionRepository>();

    #endregion

    #region Hierarchy Services
    services.AddScoped<ClusterService>();
    services.AddScoped<InstallationService>();
    services.AddScoped<FieldService>();
    services.AddScoped<ZoneService>();
    services.AddScoped<ReservoirService>();
    services.AddScoped<WellService>();
    services.AddScoped<CompletionService>();
    services.AddScoped<EquipmentService>();
    services.AddScoped<SystemHistoryService>();
    services.AddScoped<AuxiliaryService>();

    services.AddScoped<IMenuRepository, MenuRepository>();
    services.AddScoped<IGroupRepository, GroupRepository>();
    services.AddScoped<IGroupPermissionRepository, GroupPermissionRepository>();
    services.AddScoped<IGroupOperationRepository, GroupOperationRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
    services.AddScoped<IUserOperationRepository, UserOperationRepository>();
    #endregion

    #region Control Access Services
    services.AddScoped<MenuService>();
    services.AddScoped<UserService>();
    services.AddScoped<GroupService>();
    #endregion

    services.AddScoped<XLSXService>();

    var jwtKey = envVars["SECRET_KEY"];
    var key = Encoding.ASCII.GetBytes(jwtKey);

    services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    //services
    //    .AddMicrosoftIdentityWebAppAuthentication(configuration, "AzureAd")
    //    .EnableTokenAcquisitionToCallDownstreamApi(options => configuration.Bind("AzureAd", options))
    //    .AddMicrosoftGraph(options => configuration.Bind("AzureAd", options))
    //    .AddInMemoryTokenCaches();

    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = jwtKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    });
}
static void ConfigureMiddlewares(IApplicationBuilder app)
{
    app.UseMiddleware<UnauthorizedCaptureMiddleware>();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseCors("CorsPolicy");
    app.UseRouting();
}
