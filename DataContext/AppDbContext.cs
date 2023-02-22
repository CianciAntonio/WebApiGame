using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Games> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matches>()
                .HasOne(e => e.player)
                .WithMany(e => e.matches)
                .HasForeignKey(n => n.idPlayer);

            modelBuilder.Entity<Matches>()
                .HasOne(e => e.game)
                .WithMany(e => e.match)
                .HasForeignKey(n => n.idGame);

            modelBuilder.Entity<Players>();

            modelBuilder.Entity<Games>();

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimeStamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimeStamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is TimeStamps && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((TimeStamps)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((TimeStamps)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }
        }
    }
}
