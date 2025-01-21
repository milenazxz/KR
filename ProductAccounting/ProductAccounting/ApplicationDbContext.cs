using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Warehouses> warehouses { get; set; }
        public DbSet<Supplires> supplires { get; set; }
        public DbSet<Items> items { get; set; }
        public DbSet<employees> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Warehouses>()
                .HasOne(W => W.IdHeadNavigation)
                .WithMany(e => e.Warehouses)
                .HasForeignKey(W => W.id_head)
                .OnDelete(DeleteBehavior.SetNull);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

}
