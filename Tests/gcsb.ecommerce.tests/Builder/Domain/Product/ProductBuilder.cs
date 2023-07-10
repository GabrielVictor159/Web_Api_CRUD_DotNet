using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;

namespace gcsb.ecommerce.tests.Builder.Domain.Product
{
    public class ProductBuilder
    {
        public Guid Id { get; private set; }
        public String Name { get;private set; } = "";
        public decimal Value { get;private set; } = 0;
         public static ProductBuilder New(Faker faker)
        {
            return new ProductBuilder()
                .WithId(Guid.NewGuid())
                .WithName(faker.Random.String2(10))
                .WithValue(faker.Random.Decimal(1,1000));
        }
        public ProductBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public ProductBuilder WithName(String name)
        {
            Name=name;
            return this;
        }
        public ProductBuilder WithValue(decimal value)
        {
            Value=value;
            return this;
        }
        public domain.Product.Product Build()
        => new domain.Product.Product(Id,Name,Value);
    }
}