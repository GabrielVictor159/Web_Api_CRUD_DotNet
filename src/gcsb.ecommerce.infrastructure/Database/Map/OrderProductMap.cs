using gcsb.ecommerce.infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gcsb.ecommerce.infrastructure.Database.Map
{
    public class OrderProductMap : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProduct", "Ecommerce");
            builder.HasKey(e => new { e.IdOrder, e.IdProduct });
            builder.Property(e => e.Amount).IsRequired();
            builder.Property(e => e.TotalOrderLine).IsRequired();
            builder.Property(e => e.IdOrder).IsRequired();
            builder.Property(e => e.IdProduct).IsRequired();

            builder.HasOne(e => e.Order)
                .WithMany(e => e.ListOrderProduct)
                .HasForeignKey(e => e.IdOrder);

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.IdProduct);
        }
    }
}
