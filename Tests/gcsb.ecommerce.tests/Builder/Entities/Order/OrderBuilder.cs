using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using gcsb.ecommerce.domain.Enums;
using gcsb.ecommerce.tests.Builder.Entities.Client;
using gcsb.ecommerce.tests.Builder.Entities.OrderProduct;
using gcsb.ecommerce.tests.Builder.Entities.Product;

namespace gcsb.ecommerce.tests.Builder.Entities.Order
{
     public class OrderBuilder
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public infrastructure.Database.Entities.Client? Client { get; private set; }
        public Guid IdClient { get; private set; }
        public decimal TotalOrder { get; private set; }
        public string Cupons { get; private set; } = "";
        public DateTime OrderDate { get; private set; }
        public Guid IdPayment { get; private set; }
        public List<infrastructure.Database.Entities.OrderProduct> ListOrderProduct { get; private set; } = new ();
        public List<infrastructure.Database.Entities.Product> ListProducts {get; private set; } = new();
        public Faker? Faker {get; private set;}
        public dynamic? Context { get; private set; }

        public static async Task<OrderBuilder> New(Faker faker, dynamic context)
        {
            var orderBuilder = new OrderBuilder()
                .WithTotalOrder(faker.Random.Decimal(0,10))
                .WithCupons(faker.Random.Enum<Cupons>().ToString())
                .WithOrderDate(faker.Date.Past(100, DateTime.Now))
                .WithIdPayment(Guid.NewGuid());

            var client = await ClientBuilder.New(faker, context).Build();
            orderBuilder.WithClient(client).WithIdClient(client.Id);

            var products = new List<infrastructure.Database.Entities.Product>();
            for (int i = 0; i < faker.Random.Int(1, 8); i++)
            {
                var product = await ProductBuilder.New(faker, context).Build();
                products.Add(product);
            }
            orderBuilder.WithFaker(faker);
            orderBuilder.WithContext(context);
            return orderBuilder;
        }

        public OrderBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public OrderBuilder WithFaker(Faker faker)
        {
            Faker = faker;
            return this;
        }
        public OrderBuilder WithClient(infrastructure.Database.Entities.Client client)
        {
            Client = client;
            IdClient = client.Id;
            return this;
        }

        public OrderBuilder WithIdClient(Guid idClient)
        {
            IdClient = idClient;
            return this;
        }

        public OrderBuilder WithTotalOrder(decimal totalOrder)
        {
            TotalOrder = totalOrder;
            return this;
        }

        public OrderBuilder WithCupons(string cupons)
        {
            Cupons = cupons;
            return this;
        }

        public OrderBuilder WithOrderDate(DateTime orderDate)
        {
            OrderDate = orderDate;
            return this;
        }

        public OrderBuilder WithIdPayment(Guid idPayment)
        {
            IdPayment = idPayment;
            return this;
        }

        public OrderBuilder WithOrderProduct(List<infrastructure.Database.Entities.OrderProduct> orderProducts)
        {
            ListOrderProduct = orderProducts;
            TotalOrder =0;
            foreach(var orderProduct in ListOrderProduct)
            {
                TotalOrder += orderProduct.TotalOrderLine;
            }
            return this;
        }

        public OrderBuilder WithContext(dynamic context)
        {
            Context = context;
            return this;
        }

        public async Task<infrastructure.Database.Entities.Order> Build()
        {
            var entity = new infrastructure.Database.Entities.Order()
            {
                Id = Id,
                Client = Client!,
                IdClient = IdClient,
                TotalOrder = TotalOrder,
                Cupons = Cupons,
                OrderDate = OrderDate,
                IdPayment = IdPayment,
                ListOrderProduct = ListOrderProduct
            };
            await Context!.AddAsync(entity);
            await Context!.SaveChangesAsync();
            var orderProducts = new List<infrastructure.Database.Entities.OrderProduct>();
            foreach (var product in ListProducts)
            {
                var orderProduct = OrderProductBuilder.New(Faker!, entity, product).Build();
                orderProducts.Add(orderProduct);
            }
            
            entity.ListOrderProduct=orderProducts;
            await Context!.AddRangeAsync(orderProducts);
            await Context!.SaveChangesAsync();
            return entity;
        }
    }
}