using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Model;
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
            optionsBuilder.UseNpgsql(@"Server=db; Port=5432; Database=postgres; Uid=postgres; Pwd=postgres;");
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
            .HasKey(e => e.Id);
            modelBuilder.Entity<Pedido>()
            .HasOne(p => p.pedidoProduto)
            .WithOne(pp => pp.pedido)
            .HasForeignKey<PedidoProduto>(pp => pp.idPedido);
            modelBuilder.Entity<Produto>()
            .HasOne(p => p.pedidoProduto)
            .WithOne(pp => pp.produto)
            .HasForeignKey<PedidoProduto>(pp => pp.idProduto);
            modelBuilder.Entity<PedidoProduto>()
            .HasOne(pp => pp.pedido)
            .WithOne(p => p.pedidoProduto)
            .HasForeignKey<PedidoProduto>(pp => pp.idPedido);
            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.produto)
                .WithOne(p => p.pedidoProduto)
                .HasForeignKey<PedidoProduto>(pp => pp.idProduto);

        }
    }
}