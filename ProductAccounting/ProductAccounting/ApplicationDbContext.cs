﻿using Microsoft.EntityFrameworkCore;
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

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PostgreSqlConnection"].ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

}
