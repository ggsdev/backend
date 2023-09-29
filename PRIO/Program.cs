using AutoMapper;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Factories;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Repositories;
using PRIO.src.Modules.ControlAccess.Groups.Infra.Http.Services;
using PRIO.src.Modules.ControlAccess.Groups.Interfaces;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Repositories;
using PRIO.src.Modules.ControlAccess.Menus.Infra.Http.Services;
using PRIO.src.Modules.ControlAccess.Menus.Interfaces;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Repositories;
using PRIO.src.Modules.ControlAccess.Operations.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Repositories;
using PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services;
using PRIO.src.Modules.ControlAccess.Users.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Repositories;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.FileImport.XLSX.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Respositories;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.NFSMS.Interfaces;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Clusters.Interfaces;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Repositories;
using PRIO.src.Modules.Hierarchy.Completions.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
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
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Comments.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Comments.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Equipments.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.Http.Services;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Measurements.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.Http.Services;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.Http.Services;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.Productions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.WellEvents.Infra.Http.Services;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Repositories;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.WellProductions.Interfaces;
using PRIO.src.Shared;
using PRIO.src.Shared.Auxiliaries.Infra.Http.Services;
using PRIO.src.Shared.Errors;
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
var envVars = DotEnv.Read();


IConfiguration configuration = builder.Configuration;
ConfigureServices(builder.Services, configuration);

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DataContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.Write(ex.ToString());
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ConfigureMiddlewares(app);

app.UseAuthentication();

app.UseAuthorization();

//app.UseOutputCache();

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
                .AllowAnyOrigin()
          .AllowAnyHeader()
          //.AllowCredentials()
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
    services.AddScoped<ErrorMessages>();
    services.AddEndpointsApiExplorer();
    services.AddDbContext<DataContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("dbConnection")));
    services.AddScoped<AuthorizationFilter>();

    services.AddScoped<TokenServices>();

    #region Hierarchy Repositories
    services.AddScoped<IFieldRepository, FieldRepository>();

    services.AddScoped<ISystemHistoryRepository, SystemHistoryRepository>();

    services.AddScoped<IClusterRepository, ClusterRepository>();

    services.AddScoped<IInstallationRepository, InstallationRepository>();

    services.AddScoped<IInstallationsAccessRepository, InstallationsAccessRepository>();

    services.AddScoped<IFieldRepository, FieldRepository>();

    services.AddScoped<IZoneRepository, ZoneRepository>();

    services.AddScoped<IReservoirRepository, ReservoirRepository>();

    services.AddScoped<IWellRepository, WellRepository>();

    services.AddScoped<IEquipmentRepository, EquipmentRepository>();

    services.AddScoped<ICompletionRepository, CompletionRepository>();

    services.AddScoped<IMeasurementRepository, MeasurementRepository>();
    services.AddScoped<IProductionRepository, ProductionRepository>();

    services.AddScoped<IOilVolumeCalculationRepository, OilVolumeCalculationRepository>();
    services.AddScoped<IGasVolumeCalculationRepository, GasVolumeCalculationRepository>();

    services.AddScoped<IMeasurementHistoryRepository, MeasurementHistoryRepository>();
    services.AddScoped<IBTPRepository, BTPRepository>();
    services.AddScoped<INFSMRepository, NFSMRepository>();

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
    services.AddScoped<MeasuringPointService>();
    services.AddScoped<SystemHistoryService>();
    services.AddScoped<AuxiliaryService>();
    services.AddScoped<XMLImportService>();
    services.AddScoped<MeasurementService>();
    services.AddScoped<CommentService>();
    services.AddScoped<WellEventService>();

    services.AddScoped<IMenuRepository, MenuRepository>();
    services.AddScoped<IMeasuringPointRepository, MeasuringPointRepository>();
    services.AddScoped<IGroupRepository, GroupRepository>();
    services.AddScoped<IGroupPermissionRepository, GroupPermissionRepository>();
    services.AddScoped<IGroupOperationRepository, GroupOperationRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
    services.AddScoped<IUserOperationRepository, UserOperationRepository>();
    services.AddScoped<IGlobalOperationsRepository, GlobalOperationsRepository>();
    services.AddScoped<ICommentRepository, CommentRepository>();
    services.AddScoped<IWellProductionRepository, WellProductionRepository>();
    services.AddScoped<IWellEventRepository, WellEventRepository>();
    #endregion

    #region Control Access Services
    services.AddScoped<MenuService>();
    services.AddScoped<UserService>();

    services.AddScoped<GroupService>();
    #endregion

    #region Measuring Services
    services.AddScoped<OilVolumeCalculationService>();
    services.AddScoped<GasVolumeCalculationService>();
    services.AddScoped<ProductionService>();
    services.AddScoped<FieldFRService>();
    services.AddScoped<NFSMService>();
    services.AddScoped<WellProductionService>();

    #endregion

    services.AddScoped<BTPService>();

    services.AddScoped<XLSXService>();

    #region Factories
    services.AddScoped<GroupFactory>();
    services.AddScoped<GroupPermissionFactory>();
    services.AddScoped<GroupOperationFactory>();
    services.AddScoped<UserPermissionFactory>();
    services.AddScoped<UserOperationFactory>();
    services.AddScoped<UserFactory>();
    services.AddScoped<InstallationAccessFactory>();
    #endregion

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

    services.AddOutputCache(x =>
        x.AddBasePolicy(x => x.Expire(TimeSpan.FromDays(5))));

    services.AddOutputCache(x =>
        x.AddPolicy(nameof(AuthProductionCachePolicy), AuthProductionCachePolicy.Instance));

    services.AddOutputCache(x =>
        x.AddPolicy(nameof(AuthProductionIdCachePolicy), AuthProductionIdCachePolicy.Instance));
}
static void ConfigureMiddlewares(IApplicationBuilder app)
{
    app.UseCors("CorsPolicy");
    app.UseMiddleware<UnauthorizedCaptureMiddleware>();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseRouting();
}

