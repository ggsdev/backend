using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PRIO.Data.Mappings.ControlAccessMappings;
using PRIO.Data.Mappings.HierarchyMappings;
using PRIO.Data.Mappings.MeasurementMappping;
using PRIO.Models.BaseModels;
using PRIO.Models.HierarchyModels;
using PRIO.Models.Measurements;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }
        public DbSet<Completion> Completions { get; set; }
        public DbSet<Well> Wells { get; set; }
        public DbSet<MeasuringEquipment> MeasuringEquipments { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<UserPermission> Permissions { get; set; }
        public DbSet<GroupPermissions> GroupPermissions { get; set; }
        public DbSet<Menu> Menus { get; set; }

        #region Measurement & Relations
        public DbSet<Measurement> Measurements { get; set; }
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
                envVars.ContainsKey("ENCRYPT"))
            {
                var server = envVars["SERVER"];
                var database = envVars["DATABASE"];
                var userId = envVars["USER_ID"];
                var password = envVars["PASSWORD"];
                var encrypt = envVars["ENCRYPT"];

                optionsBuilder.UseSqlServer($"Server={server};Database={database};User ID={userId};Password={password};Encrypt={encrypt};");
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

            var modifiedEntriesOutraClasseBase = ChangeTracker.Entries<BaseHistoryModel>()
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
                    if (entry.Entity is BaseHistoryModel baseHistoryModel)
                    {
                        baseHistoryModel.CreatedAt = DateTime.UtcNow;
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

            modelBuilder.ApplyConfiguration(new GroupMap());
            modelBuilder.ApplyConfiguration(new MenuMap());
            modelBuilder.ApplyConfiguration(new PermissionMap());

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
