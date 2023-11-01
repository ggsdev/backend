using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Mappings;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Mappings;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Mappings;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Mappings;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Mappings;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Mappings;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileExport.Templates.Infra.EF.Mappings;
using PRIO.src.Modules.FileExport.Templates.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XLSX.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Infra.EF.Mappings;
using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Mappings;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Mappings;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Mappings;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Mappings;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Mappings;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Mappings;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Mappings;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Mappings;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Mappings;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Modules.PI.Infra.EF.Mappings;
using PRIO.src.Modules.PI.Infra.EF.Models;
using PRIO.src.Shared.Auxiliaries.Infra.EF.Mapping;
using PRIO.src.Shared.Auxiliaries.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Mappings;
using PRIO.src.Shared.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.EF.Mappings;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;

namespace PRIO.src.Shared.Infra.EF
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException(string message) : base(message)
        {
        }
    }
    public class DataContext : DbContext
    {

        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupOperation> GroupOperations { get; set; }
        public DbSet<UserOperation> UserOperations { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<GlobalOperation> GlobalOperations { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }
        public DbSet<Completion> Completions { get; set; }
        public DbSet<Well> Wells { get; set; }
        public DbSet<MeasurementHistory> MeasurementHistories { get; set; }
        public DbSet<MeasuringEquipment> MeasuringEquipments { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Oil> Oils { get; set; }
        public DbSet<GasLinear> GasesLinears { get; set; }
        public DbSet<Gas> Gases { get; set; }
        public DbSet<GasDiferencial> GasesDiferencials { get; set; }
        public DbSet<MeasuringPoint> MeasuringPoints { get; set; }
        public DbSet<OilVolumeCalculation> OilVolumeCalculations { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<TOGRecoveredOil> TOGRecoveredOils { get; set; }
        public DbSet<DrainVolume> DrainVolumes { get; set; }
        public DbSet<DOR> DORs { get; set; }
        public DbSet<AssistanceGas> AssistanceGases { get; set; }
        public DbSet<ExportGas> ExportGases { get; set; }
        public DbSet<HighPressureGas> HighPressureGases { get; set; }
        public DbSet<HPFlare> HPFlares { get; set; }
        public DbSet<ImportGas> ImportGases { get; set; }
        public DbSet<LowPressureGas> LowPressureGases { get; set; }
        public DbSet<LPFlare> LPFlares { get; set; }
        public DbSet<PilotGas> PilotGases { get; set; }
        public DbSet<PurgeGas> PurgeGases { get; set; }
        public DbSet<GasVolumeCalculation> GasVolumeCalculations { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<Volume> Volume { get; set; }
        public DbSet<Calibration> Calibrations { get; set; }
        public DbSet<Water> Waters { get; set; }
        public DbSet<Bsw> Bsws { get; set; }
        public DbSet<BTP> BTPs { get; set; }
        public DbSet<WellTests> WellTests { get; set; }
        public DbSet<BTPBase64> BTPBases64 { get; set; }
        public DbSet<InstallationBTP> InstallationBTPs { get; set; }
        public DbSet<FieldFR> FieldsFRs { get; set; }
        public DbSet<SystemHistory> SystemHistories { get; set; }
        public DbSet<Auxiliary> Auxiliaries { get; set; }
        public DbSet<CommentInProduction> Comments { get; set; }
        public DbSet<ValidateBTP> Validates { get; set; }
        public DbSet<NFSMsProductions> NFSMsProductions { get; set; }
        public DbSet<NFSM> NFSMs { get; set; }
        public DbSet<NFSMHistory> NFSMImportHistories { get; set; }
        public DbSet<WellProduction> WellProductions { get; set; }
        public DbSet<FieldProduction> FieldsProductions { get; set; }
        public DbSet<CompletionProduction> CompletionProductions { get; set; }
        public DbSet<ReservoirProduction> ReservoirProductions { get; set; }
        public DbSet<ZoneProduction> ZoneProductions { get; set; }
        public DbSet<EventReason> EventReasons { get; set; }
        public DbSet<WellEvent> WellEvents { get; set; }
        public DbSet<WellLosses> WellLosses { get; set; }
        public DbSet<Backup> Backups { get; set; }
        public DbSet<InstallationsAccess> InstallationsAccess { get; set; }
        public DbSet<Database> Databases { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Modules.PI.Infra.EF.Models.Attribute> Attributes { get; set; }
        public DbSet<WellsValues> WellValues { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<ManualWellConfiguration> ManualWellConfiguration { get; set; }
        public DbSet<ProductivityIndex> ProductivityIndex { get; set; }
        public DbSet<InjectivityIndex> InjectivityIndex { get; set; }
        public DbSet<BuildUp> BuildUp { get; set; }
        public DbSet<InjectionWaterWell> InjectionWaterWell { get; set; }
        public DbSet<InjectionWaterGasField> InjectionWaterGasField { get; set; }
        public DbSet<FieldsBalance> FieldsBalance { get; set; }
        public DbSet<InstallationsBalance> InstallationsBalance { get; set; }
        public DbSet<UEPsBalance> UEPsBalance { get; set; }
        public DbSet<InjectionGasWell> InjectionGasWell { get; set; }
        public DbSet<WellSensor> WellSensor { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<ClosingOpeningFileXLSX> ClosingOpeningFilesXLSX { get; set; }
        public DbSet<WellEventXML042Base64> WellEventXML042Base64 { get; set; }
        public DbSet<WellTestXML042Base64> WellTestXML042Base64 { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envVars = DotEnv.Read();

            if (envVars.ContainsKey("SERVER"))
            {
                var server = envVars["SERVER"];
                var database = envVars["DATABASE"];
                var userId = envVars["USER_ID"];
                var password = envVars["PASSWORD"];
                var encrypt = envVars["ENCRYPT"];
                var port = envVars["PORT"];
                var instance = envVars["SERVER_INSTANCE"];


                optionsBuilder.UseSqlServer($"Server={server},{port}\\{instance};Database={database};User ID={userId};Password={password};Encrypt={encrypt};");
            }

        }
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void UpdateTimestamps()
        {
            var modifiedEntriesBaseModel = ChangeTracker.Entries<BaseModel>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var modifiedEntriesOutraClasseBase = ChangeTracker.Entries<SystemHistory>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var modifiedEntries = modifiedEntriesBaseModel.Cast<EntityEntry>()
                .Concat(modifiedEntriesOutraClasseBase.Cast<EntityEntry>());

            foreach (var entry in modifiedEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is BaseModel baseModel)
                    {
                        baseModel.CreatedAt = DateTime.UtcNow.AddHours(-3);
                        baseModel.UpdatedAt = DateTime.UtcNow.AddHours(-3);
                    }
                    if (entry.Entity is SystemHistory systemHistoryModel)
                    {
                        systemHistoryModel.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    }
                }
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is BaseModel baseModel)
                    {
                        baseModel.UpdatedAt = DateTime.UtcNow.AddHours(-3);
                    }
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateHierarchyMap(modelBuilder);
            CreateMeasurementMap(modelBuilder);
            CreateAccessControlMap(modelBuilder);
            CreateEventMap(modelBuilder);
            CreateProductionMap(modelBuilder);
            CreateConfigCalcMap(modelBuilder);
            CreatePIMap(modelBuilder);
            CreateSystemMap(modelBuilder);
            CreateFileExportMap(modelBuilder);
            CreateWellTestMap(modelBuilder);
            CreateManualConfigurationWellMap(modelBuilder);
            CreateBalanceMap(modelBuilder);
        }
        private static void CreateAccessControlMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserPermissionMap());
            modelBuilder.ApplyConfiguration(new UserOperationMap());
            modelBuilder.ApplyConfiguration(new GroupMap());
            modelBuilder.ApplyConfiguration(new GroupPermissionMap());
            modelBuilder.ApplyConfiguration(new GroupOperationMap());
            modelBuilder.ApplyConfiguration(new InstallationsPermissionMap());
            modelBuilder.ApplyConfiguration(new GlobalOperationMap());
            modelBuilder.ApplyConfiguration(new MenuMap());
        }
        private static void CreateHierarchyMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClusterMap());
            modelBuilder.ApplyConfiguration(new InstallationMap());
            modelBuilder.ApplyConfiguration(new FieldMap());
            modelBuilder.ApplyConfiguration(new ZoneMap());
            modelBuilder.ApplyConfiguration(new ReservoirMap());
            modelBuilder.ApplyConfiguration(new CompletionMap());
            modelBuilder.ApplyConfiguration(new WellMap());
            modelBuilder.ApplyConfiguration(new MeasuringPointMap());
            modelBuilder.ApplyConfiguration(new MeasuringEquipmentMap());

        }
        private static void CreateMeasurementMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FileTypeMap());
            modelBuilder.ApplyConfiguration(new VolumeMap());
            modelBuilder.ApplyConfiguration(new CalibrationMap());
            modelBuilder.ApplyConfiguration(new BswMap());
            modelBuilder.ApplyConfiguration(new MeasurementMap());
            modelBuilder.ApplyConfiguration(new MeasurementHistoryMap());
            modelBuilder.ApplyConfiguration(new ProductionMap());
            modelBuilder.ApplyConfiguration(new OilMap());
            modelBuilder.ApplyConfiguration(new GasLinearMap());
            modelBuilder.ApplyConfiguration(new GasDiferencialMap());
            modelBuilder.ApplyConfiguration(new GasMap());
            modelBuilder.ApplyConfiguration(new BTPMap());
            modelBuilder.ApplyConfiguration(new InstallationBTPMap());
            modelBuilder.ApplyConfiguration(new FieldFRsMap());
            modelBuilder.ApplyConfiguration(new NFSMMap());
            modelBuilder.ApplyConfiguration(new NFSMsProductionsMap());
            modelBuilder.ApplyConfiguration(new NFSMHistoryMap());
        }
        private static void CreateEventMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WellEventMap());
            modelBuilder.ApplyConfiguration(new EventReasonMap());
        }
        private static void CreateProductionMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new FieldProductionMap());
            modelBuilder.ApplyConfiguration(new WellProductionMap());
            modelBuilder.ApplyConfiguration(new CompletionProductionMap());
            modelBuilder.ApplyConfiguration(new ReservoirProductionMap());
            modelBuilder.ApplyConfiguration(new ZoneProductionMap());
            modelBuilder.ApplyConfiguration(new WellLossesMap());
            modelBuilder.ApplyConfiguration(new WaterMap());
        }
        private static void CreateConfigCalcMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SectionMap());
            modelBuilder.ApplyConfiguration(new TOGRecoveredOilMap());
            modelBuilder.ApplyConfiguration(new DORMap());
            modelBuilder.ApplyConfiguration(new DrainVolumeMap());
            modelBuilder.ApplyConfiguration(new OilVolumeCalculationMap());
            modelBuilder.ApplyConfiguration(new AssistanceGasMap());
            modelBuilder.ApplyConfiguration(new ExportGasMap());
            modelBuilder.ApplyConfiguration(new GasVolumeCalculationMap());
            modelBuilder.ApplyConfiguration(new HighPressureGasMap());
            modelBuilder.ApplyConfiguration(new HPFlareMap());
            modelBuilder.ApplyConfiguration(new ImportGasMap());
            modelBuilder.ApplyConfiguration(new LowPressureGasMap());
            modelBuilder.ApplyConfiguration(new LPFlareMap());
            modelBuilder.ApplyConfiguration(new PilotGasMap());
            modelBuilder.ApplyConfiguration(new PurgeGasMap());
        }
        private static void CreatePIMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DatabasesMap());
            modelBuilder.ApplyConfiguration(new InstancesMap());
            modelBuilder.ApplyConfiguration(new ElementsMap());
            modelBuilder.ApplyConfiguration(new AttributesMap());
            modelBuilder.ApplyConfiguration(new WellsValuesMap());
            modelBuilder.ApplyConfiguration(new ValueMap());
        }
        private static void CreateSystemMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuxiliaryMap());
            modelBuilder.ApplyConfiguration(new SystemHistoryMap());
            modelBuilder.ApplyConfiguration(new SessionMap());
            modelBuilder.ApplyConfiguration(new BackupMap());
        }
        private static void CreateWellTestMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ValidateBTPMap());
            modelBuilder.ApplyConfiguration(new WellTestMap());
            modelBuilder.ApplyConfiguration(new BTPBase64Map());
            modelBuilder.ApplyConfiguration(new BTPMap());
        }
        private static void CreateManualConfigurationWellMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ManualWellConfigurationMap());
            modelBuilder.ApplyConfiguration(new BuildUpMap());
            modelBuilder.ApplyConfiguration(new ProductivityIndexMap());
            modelBuilder.ApplyConfiguration(new InjectivityIndexMap());
        }
        private static void CreateBalanceMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InjectionWaterGasFieldMap());
            modelBuilder.ApplyConfiguration(new InjectionWaterWellMap());
            modelBuilder.ApplyConfiguration(new InjectionGasWellMap());
            modelBuilder.ApplyConfiguration(new WellSensorMap());
            modelBuilder.ApplyConfiguration(new FieldsBalanceMap());
            modelBuilder.ApplyConfiguration(new InstallationsBalanceMap());
            modelBuilder.ApplyConfiguration(new UEPsBalanceMap());
        }

        private static void CreateFileExportMap(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TemplateMap());
            modelBuilder.ApplyConfiguration(new ClosingOpeningFileXLSXMap());
            modelBuilder.ApplyConfiguration(new WellEventXML042Base64Map());
            modelBuilder.ApplyConfiguration(new WellTestXML042Base64Map());
        }
    }
}
