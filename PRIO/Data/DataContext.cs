using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PRIO.Data.Mappings.ClusterMapping;
using PRIO.Data.Mappings.CompletionMapping;
using PRIO.Data.Mappings.FieldMapping;
using PRIO.Data.Mappings.FieldMappings;
using PRIO.Data.Mappings.FileTypeMappings;
using PRIO.Data.Mappings.InstallationMapping;
using PRIO.Data.Mappings.MeasurementMappping;
using PRIO.Data.Mappings.MeasuringEquipmentMapping;
using PRIO.Data.Mappings.ReservoirMapping;
using PRIO.Data.Mappings.SessionMappings;
using PRIO.Data.Mappings.UserMapping;
using PRIO.Data.Mappings.WellMapping;
using PRIO.Data.Mappings.WellMappings;
using PRIO.Data.Mappings.ZoneMapping;
using PRIO.Data.Mappings.ZoneMappings;
using PRIO.Models.BaseModels;
using PRIO.Models.Clusters;
using PRIO.Models.Completions;
using PRIO.Models.Fields;
using PRIO.Models.FileTypes;
using PRIO.Models.Installations;
using PRIO.Models.Measurements;
using PRIO.Models.MeasuringEquipments;
using PRIO.Models.Reservoirs;
using PRIO.Models.Users;
using PRIO.Models.Wells;
using PRIO.Models.Zones;

namespace PRIO.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<ClusterHistory> ClustersHistories { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<InstallationHistory> InstallationHistories { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldHistory> FieldHistories { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<ZoneHistory> ZoneHistories { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }
        public DbSet<ReservoirHistory> ReservoirHistories { get; set; }
        public DbSet<Completion> Completions { get; set; }
        public DbSet<Well> Wells { get; set; }
        public DbSet<WellHistory> WellHistories { get; set; }
        public DbSet<MeasuringEquipment> MeasuringEquipments { get; set; }

        #region Measurement & Relations
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<Volume> Volume { get; set; }
        public DbSet<Calibration> Calibrations { get; set; }
        public DbSet<Bsw> Bsws { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envVars = DotEnv.Read();

            var server = envVars["SERVER"];
            var database = envVars["DATABASE"];
            var userId = envVars["USER_ID"];
            var password = envVars["PASSWORD"];
            var encrypt = envVars["ENCRYPT"];

            optionsBuilder.UseSqlServer($"Server={server};Database={database};User ID={userId};Password={password};Encrypt={encrypt};");
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

            var modifiedEntriesOutraClasseBase = ChangeTracker.Entries<BaseHistoryModel>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var modifiedEntries = modifiedEntriesBaseModel.Cast<EntityEntry>()
                .Concat(modifiedEntriesOutraClasseBase.Cast<EntityEntry>());

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseModel baseModel)
                {
                    baseModel.CreatedAt = DateTime.UtcNow;
                    baseModel.UpdatedAt = DateTime.UtcNow;
                }
                if (entry.Entity is BaseHistoryModel baseHistoryModel)
                {
                    baseHistoryModel.CreatedAt = DateTime.UtcNow;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new SessionMap());

            modelBuilder.ApplyConfiguration(new ClusterMap());
            modelBuilder.ApplyConfiguration(new ClusterHistoryMap());

            modelBuilder.ApplyConfiguration(new InstallationMap());
            modelBuilder.ApplyConfiguration(new InstallationHistoryMap());

            modelBuilder.ApplyConfiguration(new FieldMap());
            modelBuilder.ApplyConfiguration(new FieldHistoryMap());

            modelBuilder.ApplyConfiguration(new ZoneMap());
            modelBuilder.ApplyConfiguration(new ZoneHistoryMap());

            modelBuilder.ApplyConfiguration(new ReservoirMap());
            modelBuilder.ApplyConfiguration(new ReservoirHistoryMap());

            modelBuilder.ApplyConfiguration(new CompletionMap());

            modelBuilder.ApplyConfiguration(new WellMap());
            modelBuilder.ApplyConfiguration(new WellHistoryMap());

            modelBuilder.ApplyConfiguration(new MeasuringEquipmentMap());

            #region Measurement & Relations
            modelBuilder.ApplyConfiguration(new MeasurementMap());
            modelBuilder.ApplyConfiguration(new FileTypeMap());
            modelBuilder.ApplyConfiguration(new VolumeMap());
            modelBuilder.ApplyConfiguration(new CalibrationMap());
            modelBuilder.ApplyConfiguration(new BswMap());

            #endregion
        }
    }
}
