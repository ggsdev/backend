using AutoMapper;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRIO;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Files.XML._001;
using PRIO.Files.XML._002;
using PRIO.Files.XML._003;
using PRIO.Files.XML._039;
using PRIO.Middlewares;
using PRIO.Models;
using PRIO.Models.Clusters;
using PRIO.Models.Installations;
using PRIO.Models.Measurements;
using PRIO.Services;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
ConfigureMiddlewares(app);
app.UseAuthorization();

app.MapControllers();
app.Run();

static void ConfigureServices(IServiceCollection services)
{

    var key = Encoding.ASCII.GetBytes(ConfigurationKeys.JwtKey);
    services.AddControllers(config =>
    {
        var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
        config.Filters.Add(new AuthorizeFilter(policy));
    });

    var mapperConfig = new MapperConfiguration(cfg =>
    {
        #region 039
        cfg.CreateMap<BSW, Bsw>()
        .ForMember(dest => dest.DHA_FALHA_BSW_039, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.DHA_FALHA_BSW_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_FALHA_BSW_039, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

        cfg.CreateMap<CALIBRACAO, Calibration>()
        .ForMember(dest => dest.DHA_FALHA_CALIBRACAO_039, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.DHA_FALHA_CALIBRACAO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_FALHA_CALIBRACAO_039, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));

        cfg.CreateMap<DADOS_BASICOS_039, Measurement>()
        .ForMember(dest => dest.DHA_OCORRENCIA_039, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.DHA_OCORRENCIA_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_OCORRENCIA_039, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
         .ForMember(dest => dest.DHA_DETECCAO_039, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.DHA_DETECCAO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_DETECCAO_039, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
          .ForMember(dest => dest.DHA_RETORNO_039, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.DHA_RETORNO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_RETORNO_039, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));

        cfg.CreateMap<VOLUME, Volume>()
        .ForMember(dest => dest.DHA_MEDICAO_039, opt => opt.MapFrom(src =>
            string.IsNullOrEmpty(src.DHA_MEDICAO_039) ? null : (DateTime?)DateTime.ParseExact(src.DHA_MEDICAO_039, "dd/MM/yyyy", CultureInfo.InvariantCulture)));

        cfg.CreateMap<Measurement, _039DTO>();
        #endregion

        #region 001
        cfg.CreateMap<_001PMO, Measurement>();
        cfg.CreateMap<Measurement, _001DTO>();
        #endregion

        #region 002
        cfg.CreateMap<_002PMGL, Measurement>();
        cfg.CreateMap<Measurement, _002DTO>();
        #endregion

        #region 003
        cfg.CreateMap<_003PMGD, Measurement>();
        cfg.CreateMap<Measurement, _003DTO>();
        #endregion


        cfg.CreateMap<Cluster, ClusterDTO>();
        cfg.CreateMap<Installation, InstallationDTO>();
        cfg.CreateMap<Field, FieldDTO>();
        cfg.CreateMap<Zone, ZoneDTO>();
        cfg.CreateMap<Reservoir, ReservoirDTO>();
        cfg.CreateMap<Well, WellDTO>();
        cfg.CreateMap<Completion, CompletionDTO>();
        cfg.CreateMap<User, UserDTO>();


    });

    IMapper mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper);

    DotEnv.Load();

    services.AddEndpointsApiExplorer();
    services.AddDbContext<DataContext>();
    services.AddScoped<TokenServices>();
    services.AddScoped<UserServices>();
    services.AddScoped<FieldServices>();

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

    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = ConfigurationKeys.JwtKey
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
    app.UseRouting();
}
