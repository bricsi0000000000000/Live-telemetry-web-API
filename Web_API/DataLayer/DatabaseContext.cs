using Microsoft.EntityFrameworkCore;
using DataLayer.Models.Sensors;
using DataLayer.Models;

namespace DataLayer
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=DataCenter;Uid=root;Pwd=;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Time> Times { get; set; }
        public DbSet<Speed> Speeds { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}
