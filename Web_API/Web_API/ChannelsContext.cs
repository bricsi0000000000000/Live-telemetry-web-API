using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Web_API.Models;

namespace Web_API_data_access_layer
{
    public class ChannelsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=Channels;Uid=root;Pwd=;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Time> Times { get; set; }
        public DbSet<Speed> Speeds{ get; set; }
    }
}
