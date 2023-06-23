using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.infrastructure.Database.Entities;
using gcsb.ecommerce.infrastructure.Database.Map;
using Microsoft.EntityFrameworkCore;

namespace gcsb.ecommerce.infrastructure.Database
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
        public DbSet<Product> Products => Set<Product>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DBCONN");
            var inMemory = Environment.GetEnvironmentVariable("USEINMEMORY");
            if(connectionString!=null && inMemory==null)
            {
            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.MigrationsHistoryTable("_MigrationHistory", "Ecommerce");
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });
            }
            else
            {
                optionsBuilder.UseInMemoryDatabase("EcommerceInMemory");
            }
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderProductMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            base.OnModelCreating(modelBuilder);
        }
    
        
    }
}