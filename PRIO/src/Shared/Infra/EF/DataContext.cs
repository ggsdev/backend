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
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Mappings;
using PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Models;
using PRIO.src.Shared.Auxiliaries.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.EF.Mappings;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;

namespace PRIO.src.Shared.Infra.EF
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<GlobalOperation> GlobalOperations { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }
        public DbSet<Completion> Completions { get; set; }
        public DbSet<Well> Wells { get; set; }
        public DbSet<MeasuringEquipment> MeasuringEquipments { get; set; }
        public DbSet<SystemHistory> SystemHistories { get; set; }
        public DbSet<Auxiliary> Auxiliaries { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupOperation> GroupOperations { get; set; }
        public DbSet<UserOperation> UserOperations { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Menu> Menus { get; set; }

        #region Measurement & Relations
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<OilVolumeCalculation> OilVolumeCalculations { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<TOGRecoveredOil> TOGRecoveredOils { get; set; }
        public DbSet<DrainVolume> DrainVolumes { get; set; }
        public DbSet<DOR> DORs { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<Volume> Volume { get; set; }
        public DbSet<Calibration> Calibrations { get; set; }
        public DbSet<Bsw> Bsws { get; set; }
        #endregion

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envVars = DotEnv.Read();

            if (envVars.ContainsKey("SERVER") &&
               envVars.ContainsKey("DATABASE") &&
               envVars.ContainsKey("USER_ID") &&
               envVars.ContainsKey("PASSWORD") &&
               envVars.ContainsKey("ENCRYPT") &&
               envVars.ContainsKey("PORT"))
            {
                var server = envVars["SERVER"];
                var database = envVars["DATABASE"];
                var userId = envVars["USER_ID"];
                var password = envVars["PASSWORD"];
                var encrypt = envVars["ENCRYPT"];
                var port = envVars["PORT"];

                optionsBuilder.UseSqlServer($"Server={server},{port};Database={database};User ID={userId};Password={password};Encrypt={encrypt};");
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
                        baseModel.CreatedAt = DateTime.UtcNow;
                        baseModel.UpdatedAt = DateTime.UtcNow;
                    }
                    if (entry.Entity is SystemHistory systemHistoryModel)
                    {
                        systemHistoryModel.CreatedAt = DateTime.UtcNow;
                    }
                }
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is BaseModel baseModel)
                    {
                        baseModel.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.ApplyConfiguration(new SystemHistoryMap());

            modelBuilder.ApplyConfiguration(new GroupMap());
            modelBuilder.ApplyConfiguration(new MenuMap());
            modelBuilder.ApplyConfiguration(new UserPermissionMap());
            modelBuilder.ApplyConfiguration(new GroupPermissionMap());
            modelBuilder.ApplyConfiguration(new UserOperationMap());

            #region Measurement & Relations
            modelBuilder.ApplyConfiguration(new MeasurementMap());
            modelBuilder.ApplyConfiguration(new SectionMap());
            modelBuilder.ApplyConfiguration(new TOGRecoveredOilMap());
            modelBuilder.ApplyConfiguration(new DORMap());
            modelBuilder.ApplyConfiguration(new DrainVolumeMap());
            modelBuilder.ApplyConfiguration(new FileTypeMap());
            modelBuilder.ApplyConfiguration(new VolumeMap());
            modelBuilder.ApplyConfiguration(new CalibrationMap());
            modelBuilder.ApplyConfiguration(new BswMap());

            #endregion
        }
    }
}
