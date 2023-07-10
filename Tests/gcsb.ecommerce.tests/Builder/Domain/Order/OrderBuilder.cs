using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.tests.Builder.Domain.OrderProduct;

namespace gcsb.ecommerce.tests.Builder.Domain.Order
{
    public class OrderBuilder
    {
        public Guid Id { get; private set; }
        public Guid IdClient { get; private set; }
        public List<domain.OrderProduct.OrderProduct> List { get; private set; } = new();
        public DateTime OrderDate { get; private set; }
        public string? NameCupom { get; private set; } = null;
        public static OrderBuilder New(Faker faker)
        {
            List<domain.OrderProduct.OrderProduct> list = new();
            for (int i = 0; i < faker.Random.Int(1, 10); i++)
            {
                list.Add(OrderProductBuilder.New(faker).Build());
            }
            return new OrderBuilder()
            {
                Id = Guid.NewGuid(),
                IdClient = Guid.NewGuid(),
                List = list,
                OrderDate = faker.Date.Past(100, DateTime.Now),
                NameCupom = faker.Random.Enum<Cupons>().ToString()
            };
        }
        public OrderBuilder WithId(Guid id)
        {
            this.Id = id;
            return this;
        }
        public OrderBuilder WithIdClient(Guid idClient)
        {
            this.IdClient = idClient;
            return this;
        }
        public OrderBuilder WithOrderProducts(List<domain.OrderProduct.OrderProduct> orderProducts)
        {
            this.List = orderProducts;
            return this;
        }
        public OrderBuilder WithOrderDate(DateTime orderDate)
        {
            this.OrderDate = orderDate;
            return this;
        }
        public OrderBuilder WithNameCupom(string nameCupom)
        {
            this.NameCupom = nameCupom;
            return this;
        }
        public domain.Order.Order Build()
          => new domain.Order.Order(Id,IdClient, List, OrderDate, NameCupom);
    }
}
