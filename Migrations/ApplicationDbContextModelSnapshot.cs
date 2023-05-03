﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Web_Api_CRUD.Infraestructure;

#nullable disable

namespace Web_Api_CRUD.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Web_Api_CRUD.Model.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("clientes");
                });

            modelBuilder.Entity("Web_Api_CRUD.Model.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("numeric");

                    b.Property<Guid>("idCliente")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("idCliente");

                    b.ToTable("pedidos");
                });

            modelBuilder.Entity("Web_Api_CRUD.Model.PedidoProduto", b =>
                {
                    b.Property<Guid>("IdPedido")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdProduto")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer");

                    b.Property<decimal>("ValorTotalLinha")
                        .HasColumnType("numeric");

                    b.HasKey("IdPedido", "IdProduto");

                    b.HasIndex("IdProduto");

                    b.ToTable("pedidoProdutos");
                });

            modelBuilder.Entity("Web_Api_CRUD.Model.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("produtos");
                });

            modelBuilder.Entity("Web_Api_CRUD.Model.Pedido", b =>
                {
                    b.HasOne("Web_Api_CRUD.Model.Cliente", "cliente")
                        .WithMany("pedidos")
                        .HasForeignKey("idCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cliente");
                });

            modelBuilder.Entity("Web_Api_CRUD.Model.PedidoProduto", b =>
                {
                    b.HasOne("Web_Api_CRUD.Model.Pedido", "Pedido")
                        .WithMany("Lista")
                        .HasForeignKey("IdPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_Api_CRUD.Model.Produto", "Produto")
                        .WithMany("Lista")
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Web_Api_CRUD.Model.Cliente", b =>
                {
                    b.Navigation("pedidos");
                });

            modelBuilder.Entity("Web_Api_CRUD.Model.Pedido", b =>
                {
                    b.Navigation("Lista");
                });

            modelBuilder.Entity("Web_Api_CRUD.Model.Produto", b =>
                {
                    b.Navigation("Lista");
                });
#pragma warning restore 612, 618
        }
    }
}
