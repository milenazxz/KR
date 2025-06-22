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
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Warehouses> warehouses { get; set; }
        public DbSet<suppliers> suppliers { get; set; }
        public DbSet<Items> items { get; set; }
        public DbSet<employees> employees { get; set; }
        public DbSet<Clients> clients{ get; set; }


        public DbSet<Sales> sales { get; set; }
        public DbSet<Writeoffs> writeoffs { get; set; }
        public DbSet<Supplies> supplies { get; set; }


        public DbSet<ItemForSale> itemsforsales { get; set; }
        public DbSet<ItemsForSupply> itemsforsupply { get; set; }
        public DbSet<ItemsForWriteOff> itemsForWriteOffs { get; set; }

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
                .HasForeignKey(s=>s.id_warehouse)
                .OnDelete(DeleteBehavior.SetNull);

            //Настройка модели ItemsForSale
            modelBuilder.Entity<ItemForSale>(entity =>
            {
                entity.ToTable("itemsforsale"); // Явно указываем имя таблицы в snake_case

                entity.HasKey(e => e.id);

                entity.HasOne(e => e.IdSaleNavigation)
                    .WithMany(s => s.ItemForSales) // Убедитесь, что в классе Sale есть public ICollection<ItemForSale> ItemForSales
                    .HasForeignKey(e => e.id_sale)
                    .OnDelete(DeleteBehavior.SetNull); // Будьте осторожны с SetNull — нужно, чтобы FK позволял NULL

                entity.HasOne(e => e.IdItemNavigation)
                    .WithMany(i => i.ItemForSales) // В классе Item должно быть public ICollection<ItemForSale> ItemForSales
                    .HasForeignKey(e => e.id_item)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            //Настройка модели ItemsForSupply
            modelBuilder.Entity<ItemsForSupply>(entity =>
            {
                entity.ToTable("itemsforsupply");
                entity.HasKey(e => e.id);

                entity.HasOne(e => e.IdSupplyNavigation)
                .WithMany(sup => sup.itemsforsupplies)
                .HasForeignKey(e => e.id_supply)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.IdItemNavigation)
                .WithMany(i => i.itemsForSupplies)
                .HasForeignKey(e => e.id_item)
                .OnDelete(DeleteBehavior.SetNull);
            });

            //Настройка модели ItemsForWriteOff
            modelBuilder.Entity<ItemsForWriteOff>(entity => 
            {
                entity.ToTable("itemsforwriteoff");

                entity.HasKey(e => e.id);

                entity.HasOne(e => e.IdWriteOffNavigation)
                .WithMany(wr => wr.writeOffs)
                .HasForeignKey(e => e.id_writeoff)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.IdItemNavigation)
                .WithMany(i => i.itemsForWriteOffs)
                .HasForeignKey(e => e.id_item)
                .OnDelete(DeleteBehavior.SetNull);
            });
             
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

}
