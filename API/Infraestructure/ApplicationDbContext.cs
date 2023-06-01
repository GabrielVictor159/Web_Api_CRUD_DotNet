using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web_Api_CRUD.Domain;
namespace Web_Api_CRUD.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cliente> clientes => Set<Cliente>();
        public DbSet<Pedido> pedidos => Set<Pedido>();
        public DbSet<Produto> produtos => Set<Produto>();
        public DbSet<PedidoProduto> pedidoProdutos => Set<PedidoProduto>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DBCONN");
            optionsBuilder.UseNpgsql (connectionString, options =>
            {
                options.MigrationsHistoryTable("_MigrationHistory", "Ecommerce");
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
             .HasKey(e => e.Id);

            modelBuilder.Entity<Pedido>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Produto>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<PedidoProduto>()
                .HasKey(e => new { e.IdPedido, e.IdProduto });

            modelBuilder.Entity<Cliente>()
            .HasMany(e => e.pedidos)
            .WithOne(p => p.cliente)
            .HasForeignKey(p => p.idCliente);
            modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Lista)
            .WithOne(p => p.Pedido)
            .HasForeignKey(p => p.IdPedido);
            modelBuilder.Entity<Produto>()
            .HasMany(p => p.Lista)
            .WithOne(p => p.Produto)
            .HasForeignKey(p => p.IdProduto);
        }
    }
}