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
            .HasKey(e => new { e.idPedido, e.idProduto });
            modelBuilder.Entity<Cliente>()
            .HasMany(p => p.pedidos)
            .WithOne(p => p.cliente)
            .HasForeignKey(p => p.Id);
            modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Lista)
            .WithOne(p => p.pedido)
            .HasForeignKey(p => p.idPedido);
            modelBuilder.Entity<Produto>()
            .HasMany(p => p.Lista)
            .WithOne(p => p.produto)
            .HasForeignKey(p => p.idProduto);
            modelBuilder.Entity<PedidoProduto>()
            .HasOne(pp => pp.pedido)
            .WithMany(p => p.Lista)
            .HasForeignKey(pp => pp.idPedido);
            modelBuilder.Entity<PedidoProduto>()
            .HasOne(pp => pp.produto)
            .WithMany(p => p.Lista)
            .HasForeignKey(pp => pp.idProduto);

        }
    }
}