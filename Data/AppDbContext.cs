using Microsoft.EntityFrameworkCore;
using NarrativusAPI.Models;

namespace NarrativusAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.Owner)
                .WithMany(c => c.Relationships)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.RelatedTo)
                .WithMany()
                .HasForeignKey(r => r.RelatedToId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}