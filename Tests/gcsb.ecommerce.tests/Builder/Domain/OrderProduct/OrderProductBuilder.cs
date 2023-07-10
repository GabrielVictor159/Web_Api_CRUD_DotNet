using System;
using Bogus;
using gcsb.ecommerce.tests.Builder.Domain.Product;

namespace gcsb.ecommerce.tests.Builder.Domain.OrderProduct
{
    public class OrderProductBuilder
    {
        public int Amount { get; private set; }
        public domain.Product.Product Product { get; private set; } = new domain.Product.Product("Initial Product",(decimal)994);
        public Guid IdOrder { get; private set; }
        public static OrderProductBuilder New(Faker faker)
        {
            return new OrderProductBuilder()
            {
                Amount = faker.Random.Int(1, 100),
                Product = ProductBuilder.New(faker).Build(),
                IdOrder = Guid.NewGuid()
            };
        }
        public OrderProductBuilder WithAmount(int amount)
        {
            this.Amount = amount;
            return this;
        }
        public OrderProductBuilder WithProduct(domain.Product.Product product)
        {
            this.Product = product;
            return this;
        }
        public OrderProductBuilder WithIdOrder(Guid idOrder)
        {
            this.IdOrder = idOrder;
            return this;
        }
        public domain.OrderProduct.OrderProduct Build()
            => new domain.OrderProduct.OrderProduct( Amount, IdOrder, Product);
    }
}
