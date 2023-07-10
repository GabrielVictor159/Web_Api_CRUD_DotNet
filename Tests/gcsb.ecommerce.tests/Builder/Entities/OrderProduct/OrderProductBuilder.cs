using Bogus;

namespace gcsb.ecommerce.tests.Builder.Entities.OrderProduct
{
    public class OrderProductBuilder
    {
        public int Amount { get; private set; } = 2;
        public decimal TotalOrderLine { get; private set; }
        public Guid IdOrder { get; private set; }
        public Guid IdProduct { get; private set; }
        public infrastructure.Database.Entities.Order? Order { get; private set; }
        public infrastructure.Database.Entities.Product? Product { get; private set; }

        public static OrderProductBuilder New(
            Faker faker,
            infrastructure.Database.Entities.Order order, 
            infrastructure.Database.Entities.Product product)
        {
            return new OrderProductBuilder()
            .WithAmount(faker.Random.Int(1,10))
            .WithProduct(product)
            .WithOrder(order);
        }

        public OrderProductBuilder WithAmount(int amount)
        {
            Amount = amount;
            return this;
        }

        public OrderProductBuilder WithTotalOrderLine(decimal totalOrderLine)
        {
            TotalOrderLine = totalOrderLine;
            return this;
        }


        public OrderProductBuilder WithOrder(infrastructure.Database.Entities.Order order)
        {
            Order = order;
            IdOrder = order.Id;
            return this;
        }

        public OrderProductBuilder WithProduct(infrastructure.Database.Entities.Product product)
        {
            Product = product;
            TotalOrderLine = Amount*product.Value;
            IdProduct = product.Id;
            return this;
        }

        public infrastructure.Database.Entities.OrderProduct Build()
        {
            return new infrastructure.Database.Entities.OrderProduct()
            {
                Amount = Amount,
                TotalOrderLine = TotalOrderLine,
                IdOrder = IdOrder,
                IdProduct = IdProduct,
                Order = Order!,
                Product = Product!
            };
        }
    }
}