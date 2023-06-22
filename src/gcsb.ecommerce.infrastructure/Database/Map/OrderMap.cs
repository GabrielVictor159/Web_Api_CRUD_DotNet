using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gcsb.ecommerce.infrastructure.Database.Map
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", "Ecommerce");
            builder.HasKey(e => e.Id);
            builder.Property(e=>e.OrderDate).IsRequired();
            builder.Property(e=>e.TotalOrder).IsRequired();
        }
    }
}