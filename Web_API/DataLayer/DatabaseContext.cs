using Microsoft.EntityFrameworkCore;
using DataLayer.Models.Sensors;
using DataLayer.Models;

namespace DataLayer
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=RICSI; Initial Catalog=DataCenter; Integrated Security=SSPI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Session> Session { get; set; }
        public DbSet<Package> Package { get; set; }

        #region sensor tables

        public DbSet<Time> Time { get; set; }
        public DbSet<Speed> Speed { get; set; }
        public DbSet<Yaw> Yaw { get; set; }

        #endregion
    }
}
