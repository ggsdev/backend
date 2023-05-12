using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PRIO.Data.Mappings;
using PRIO.Models;
using PRIO.Utils;

namespace PRIO.Data
{
    public class DataContext : DbContext
    {
        private static readonly MemoryCache _cache = new(new MemoryCacheOptions());
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<Reservoir> Reservoirs { get; set; }
        public DbSet<Completion> Completions { get; set; }
        public DbSet<Well> Wells { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<FileType> FileTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var secrets = _cache.GetOrCreate("secrets", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return FetchingSecrets.FetchSecretsAsync().GetAwaiter().GetResult();
            });
            optionsBuilder.UseSqlServer($"Server={secrets.DatabaseServer};Database={secrets.DatabaseName};User ID={secrets.DatabaseUser};Password={secrets.DatabasePassword};Encrypt=false;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ClusterMap());
            modelBuilder.ApplyConfiguration(new CompletionMap());
            modelBuilder.ApplyConfiguration(new FieldMap());
            modelBuilder.ApplyConfiguration(new InstallationMap());
            modelBuilder.ApplyConfiguration(new ReservoirMap());
            modelBuilder.ApplyConfiguration(new SessionMap());
            modelBuilder.ApplyConfiguration(new WellMap());
            modelBuilder.ApplyConfiguration(new UnitMap());
            modelBuilder.ApplyConfiguration(new FileTypeMap());
            modelBuilder.ApplyConfiguration(new MeasurementMap());
        }
    }
}
