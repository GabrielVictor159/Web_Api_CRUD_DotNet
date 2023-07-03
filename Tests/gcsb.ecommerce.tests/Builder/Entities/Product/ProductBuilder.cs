using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;

namespace gcsb.ecommerce.tests.Builder.Entities.Product
{
    public class ProductBuilder
    {
         public Guid Id { get; private set; }
        public string Name { get; private set; } ="";
        public decimal Value { get; private set; }
        public dynamic? Context { get; private set; }
        public static ProductBuilder New(Faker faker, dynamic context)
        {
            return new ProductBuilder()
                    .WithContext(context)
                    .WithId(Guid.NewGuid())
                    .WithName(faker.Commerce.ProductName())
                    .WithValue(decimal.Parse(faker.Commerce.Price()));
        }
        public ProductBuilder WithContext(dynamic context)
        {
            Context = context;
            return this;
        }
        public ProductBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public ProductBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public ProductBuilder WithValue(decimal value)
        {
            Value = value;
            return this;
        }

        public async Task<infrastructure.Database.Entities.Product> Build()
        {
            var entity = new infrastructure.Database.Entities.Product()
            {
                Id = Id,
                Name = Name,
                Value = Value
            };
             if (Context != null)
            {
                await Context.AddAsync(entity);
                await Context.SaveChangesAsync();
            }
            return entity;

        }

    }
}