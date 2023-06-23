using gcsb.ecommerce.infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gcsb.ecommerce.infrastructure.Database.Map
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client", "Ecommerce");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.Role).IsRequired();

            builder.HasMany(e => e.ListOrder)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.IdClient);
        }
    }
}
