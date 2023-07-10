﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using gcsb.ecommerce.infrastructure.Database;

#nullable disable

namespace gcsb.ecommerce.infrastructure.Database.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230625035300_SecondMigration")]
    partial class SecondMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Client", "Ecommerce");
                });

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cupons")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("IdClient")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdPayment")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("TotalOrder")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.ToTable("Order", "Ecommerce");
                });

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.OrderProduct", b =>
                {
                    b.Property<Guid>("IdOrder")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IdProduct")
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalOrderLine")
                        .HasColumnType("numeric");

                    b.HasKey("IdOrder", "IdProduct");

                    b.HasIndex("IdProduct");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct", "Ecommerce");
                });

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Product", "Ecommerce");
                });

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.Order", b =>
                {
                    b.HasOne("gcsb.ecommerce.infrastructure.Database.Entities.Client", "Client")
                        .WithMany("ListOrder")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.OrderProduct", b =>
                {
                    b.HasOne("gcsb.ecommerce.infrastructure.Database.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gcsb.ecommerce.infrastructure.Database.Entities.Product", null)
                        .WithMany("ListOrderProduct")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gcsb.ecommerce.infrastructure.Database.Entities.Order", null)
                        .WithMany("ListOrderProduct")
                        .HasForeignKey("OrderId");

                    b.HasOne("gcsb.ecommerce.infrastructure.Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.Client", b =>
                {
                    b.Navigation("ListOrder");
                });

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.Order", b =>
                {
                    b.Navigation("ListOrderProduct");
                });

            modelBuilder.Entity("gcsb.ecommerce.infrastructure.Database.Entities.Product", b =>
                {
                    b.Navigation("ListOrderProduct");
                });
#pragma warning restore 612, 618
        }
    }
}
