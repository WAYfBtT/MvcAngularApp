using DAL.Entities;
using DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;

namespace DAL.DataContext
{
    public class UrlShortenerDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }

        public UrlShortenerDataContext(DbContextOptions<UrlShortenerDataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Username).HasMaxLength(64).IsRequired();
                x.Property(x => x.UsernameNormalized).HasMaxLength(64).IsRequired();
                x.HasIndex(x => x.UsernameNormalized).IsUnique();
                x.Property(x => x.Password).HasMaxLength(128).IsRequired();
            });

            modelBuilder.Entity<Url>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.LongUrl).HasMaxLength(2048).IsRequired();
                x.HasIndex(x => x.LongUrl).IsUnique();
                x.Property(x => x.ShortUrl).HasMaxLength(2048).IsRequired();
                x.HasIndex(x => x.ShortUrl).IsUnique();
                x.Property(x => x.CreatedAtUtc).HasConversion(
                    x => x,
                    x => new DateTime(x.Ticks, DateTimeKind.Utc)).IsRequired();
            });

            modelBuilder.Entity<Url>()
                .HasOne(u => u.User)
                .WithMany(u => u.Urls)
                .HasForeignKey(u => u.CreatedBy)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        public override int SaveChanges()
        {
            AuditEntities();

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                SetValuesToDataBaseValues(ex);
                throw;
            }
        }

        private static void SetValuesToDataBaseValues(DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var databaseValues = entry.GetDatabaseValues();
                if (databaseValues == null)
                    continue;
                entry.OriginalValues.SetValues(databaseValues);
            }
        }

        private void AuditEntities()
        {
            var addedAuditedEntities = ChangeTracker.Entries<ICreatedAtUtcEntity>()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            foreach (var entity in addedAuditedEntities)
                entity.CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
