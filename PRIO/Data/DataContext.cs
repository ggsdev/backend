﻿using Microsoft.EntityFrameworkCore;
using PRIO.Data.Mappings;
using PRIO.Data.Mappings.MeasurementMappping;
using PRIO.Models;
using PRIO.Models.Measurements;
using dotenv.net;

namespace PRIO.Data
{
    public class DataContext : DbContext
    {
        //private static readonly MemoryCache _cache = new(new MemoryCacheOptions());

        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }
        public DbSet<Completion> Completions { get; set; }
        public DbSet<Well> Wells { get; set; }
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
            //var secrets = _cache.GetOrCreate("secrets", entry =>
            //{
            //    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            //    return FetchingSecrets.FetchSecretsAsync().GetAwaiter().GetResult();
            //});

            //optionsBuilder.UseSqlServer($"Server={secrets.DatabaseServer};Database={secrets.DatabaseName};User ID={secrets.DatabaseUser};Password={secrets.DatabasePassword};Encrypt=false;");

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
            var modifiedEntries = ChangeTracker.Entries<BaseModel>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.DeletedAt = null;
                    entry.Entity.IsActive = true;
                }

                entry.Entity.UpdatedAt = DateTime.UtcNow;
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
