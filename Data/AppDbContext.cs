using Microsoft.EntityFrameworkCore;
using NarrativusAPI.Models;

namespace NarrativusAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
        
    }
}