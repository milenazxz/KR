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
        public DbSet<suppliers> suppliers { get; set; }
        public DbSet<Items> items { get; set; }
        public DbSet<employees> employees { get; set; }
        public DbSet<Clients> clients{ get; set; }


        public DbSet<Sales> sales { get; set; }
        public DbSet<Writeoffs> writeoffs { get; set; }
        public DbSet<Supplies> supplies { get; set; }


        public DbSet<Itemsforsale> itemsforsales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Настройка модели Warehouses
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Warehouses>()
                .HasOne(W => W.IdHeadNavigation)
                .WithMany(e => e.Warehouses)
                .HasForeignKey(W => W.id_head)
                .OnDelete(DeleteBehavior.SetNull);

            //Настройка модели Clients
            modelBuilder.Entity<Clients>()
                .Property(c => c.email)
                .HasDefaultValue("Неизвестен");

            modelBuilder.Entity<Clients>()
                .Property(c => c.phonenumber)
                .HasColumnType("character varying(12)");

            //Настройка модели Supplires
            modelBuilder.Entity<suppliers>()
                .Property(s => s.phonenumber)
                .HasColumnType("character varying(12)");

            modelBuilder.Entity<suppliers>()
                .Property(s => s.email)
                .HasDefaultValue("Неизвестен");

            //Настройка модели Sales
            modelBuilder.Entity<Sales>()
                .HasOne(s => s.IdEmpNavigation)
                .WithMany(e => e.Sales)
                .HasForeignKey(s => s.id_employee)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Sales>()
                .HasOne(s => s.IdClientNavigation)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.id_client)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Sales>()
                .HasOne(s => s.IdWarehNavigation)
                .WithMany(w => w.Sales)
                .HasForeignKey(s => s.id_warehouse)
                .OnDelete(DeleteBehavior.SetNull);

            //Настройка модели WriteOffs
            modelBuilder.Entity<Writeoffs>()
                .HasOne(w => w.IdEmpNavigation)
                .WithMany(e => e.Writeoffs)
                .HasForeignKey(w => w.id_employee)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Writeoffs>()
                .HasOne(w => w.IdWarehNavigation)
                .WithMany(war => war.Writeoffs)
                .HasForeignKey(w => w.id_warehouse)
                .OnDelete(DeleteBehavior.SetNull);
            
            //Настройка модели Supplies
            modelBuilder.Entity<Supplies>()
                .HasOne(s => s.IdEmpNavigation)
                .WithMany(e => e.Supplies)
                .HasForeignKey(s => s.id_employee)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Supplies>()
                .HasOne(s => s.IdSupNavigation)
                .WithMany(sup => sup.Supplies)
                .HasForeignKey(s=>s.id_supplier)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Supplies>()
                .HasOne(s => s.IdWarehNavigation)
                .WithMany(w => w.Supplies)
                .HasForeignKey(s=>s.id_supplier)
                .OnDelete(DeleteBehavior.SetNull);

            //Настройка модели ItemsForSale
            modelBuilder.Entity<Itemsforsale>()
                .HasOne(ifs => ifs.IdItemNavigation)
                .WithMany(it => it.itemsforsales)
                .HasForeignKey(i => i.id_item)
                .OnDelete(DeleteBehavior.SetNull);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

}
