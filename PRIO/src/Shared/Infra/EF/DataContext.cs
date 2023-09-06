using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Mappings;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Mappings;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Mappings;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
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
using PRIO.src.Modules.Measuring.WellEvents.EF.Mappings;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;
using PRIO.src.Shared.Auxiliaries.Infra.EF.Models;
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
        #region Session
        public DbSet<Session> Sessions { get; set; }
        #endregion

        #region Control Access
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupOperation> GroupOperations { get; set; }
        public DbSet<UserOperation> UserOperations { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<GlobalOperation> GlobalOperations { get; set; }
        #endregion

        #region Hierarchy
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }
        public DbSet<Completion> Completions { get; set; }
        public DbSet<Well> Wells { get; set; }
        #endregion

        #region Measurement & Relations
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
        #endregion

        #region BTP
        public DbSet<BTP> BTPs { get; set; }
        public DbSet<WellTests> WellTests { get; set; }
        public DbSet<BTPBase64> BTPBases64 { get; set; }
        public DbSet<InstallationBTP> InstallationBTPs { get; set; }

        #endregion

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
            modelBuilder.ApplyConfiguration(new SystemHistoryMap());

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new SessionMap());

            modelBuilder.ApplyConfiguration(new ClusterMap());

            modelBuilder.ApplyConfiguration(new InstallationMap());

            modelBuilder.ApplyConfiguration(new FieldMap());

            modelBuilder.ApplyConfiguration(new ZoneMap());

            modelBuilder.ApplyConfiguration(new ReservoirMap());

            modelBuilder.ApplyConfiguration(new CompletionMap());

            modelBuilder.ApplyConfiguration(new WellMap());

            modelBuilder.ApplyConfiguration(new MeasuringEquipmentMap());
            modelBuilder.ApplyConfiguration(new MeasurementHistoryMap());
            modelBuilder.ApplyConfiguration(new ProductionMap());
            modelBuilder.ApplyConfiguration(new OilMap());
            modelBuilder.ApplyConfiguration(new GasLinearMap());
            modelBuilder.ApplyConfiguration(new GasDiferencialMap());
            modelBuilder.ApplyConfiguration(new GasMap());
            modelBuilder.ApplyConfiguration(new MeasuringPointMap());
            modelBuilder.ApplyConfiguration(new BTPMap());
            modelBuilder.ApplyConfiguration(new WellTestMap());
            modelBuilder.ApplyConfiguration(new BTPBase64Map());
            modelBuilder.ApplyConfiguration(new BTPMap());
            modelBuilder.ApplyConfiguration(new InstallationBTPMap());
            modelBuilder.ApplyConfiguration(new FieldFRsMap());
            modelBuilder.ApplyConfiguration(new NFSMMap());
            modelBuilder.ApplyConfiguration(new NFSMsProductionsMap());
            modelBuilder.ApplyConfiguration(new NFSMHistoryMap());

            modelBuilder.ApplyConfiguration(new GroupMap());
            modelBuilder.ApplyConfiguration(new MenuMap());
            modelBuilder.ApplyConfiguration(new UserPermissionMap());
            modelBuilder.ApplyConfiguration(new GroupPermissionMap());
            modelBuilder.ApplyConfiguration(new UserOperationMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new WellEventMap());
            modelBuilder.ApplyConfiguration(new EventReasonMap());

            #region Production
            modelBuilder.ApplyConfiguration(new FieldProductionMap());
            modelBuilder.ApplyConfiguration(new WellProductionMap());
            modelBuilder.ApplyConfiguration(new CompletionProductionMap());
            modelBuilder.ApplyConfiguration(new ReservoirProductionMap());
            modelBuilder.ApplyConfiguration(new ZoneProductionMap());
            #endregion

            #region Measurement & Relations
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


            modelBuilder.ApplyConfiguration(new MeasurementMap());
            modelBuilder.ApplyConfiguration(new FileTypeMap());
            modelBuilder.ApplyConfiguration(new VolumeMap());
            modelBuilder.ApplyConfiguration(new CalibrationMap());
            modelBuilder.ApplyConfiguration(new BswMap());

            #endregion
        }
    }
}
