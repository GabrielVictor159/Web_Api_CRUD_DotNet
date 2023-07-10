using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using gcsb.ecommerce.tests.Builder.Domain.Order;
using gcsb.ecommerce.tests.Builder.Domain.OrderProduct;
using gcsb.ecommerce.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Infraestructure.Database.Repositories
{
    [UseAutofacTestFramework]
    public class OrderRepositoryTest
    {
        private readonly Faker faker;
        private readonly Context context;
        private readonly IOrderRepository repository; 
        private readonly IMapper mapper;
        private List<domain.Product.Product> products;
        private domain.Client.Client client;
        public OrderRepositoryTest(
            Faker faker,
            Context context,
            IOrderRepository repository,
            IMapper mapper)
        {
            this.faker = faker;
            this.context = context;
            this.repository = repository;
            this.mapper = mapper;
            List<infrastructure.Database.Entities.Product> listProduct = new();
            for(int i=0; i<5; i++)
            {
                listProduct.Add(mapper.Map<infrastructure.Database.Entities.Product>( ProductBuilder.New(faker).WithId(Guid.NewGuid()).Build())); 
            }
            context.Products.AddRange(listProduct);
            context.SaveChanges();
            products = mapper.Map<List<domain.Product.Product>>(listProduct);
            var clientDomain = ClientBuilder.New(faker).Build();
            context.Clients.Add(mapper.Map<infrastructure.Database.Entities.Client>(clientDomain));
            context.SaveChanges();
            client = clientDomain;
        }
        [Fact]
        public async Task ShouldNotBeNullByCreateAsync()
        {
            Guid idOrder = Guid.NewGuid();
            List<domain.OrderProduct.OrderProduct> listOrderProduct = new();
            foreach(var product in products){
            var OrderProduct = OrderProductBuilder.New(faker).WithIdOrder(idOrder).WithProduct(product).Build();
            listOrderProduct.Add(OrderProduct);
            }
            var order = OrderBuilder.New(faker).WithIdClient(client.Id).WithId(idOrder).WithOrderProducts(listOrderProduct).Build();
            var result = await repository.CreateAsync(order);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldBeNullByDeleteAsync()
        {
            var order = OrderBuilder.New(faker).WithIdClient(client.Id).WithOrderProducts(new List<domain.OrderProduct.OrderProduct>()).Build();
            var orderPersist = mapper.Map<infrastructure.Database.Entities.Order>(order);
            await context.Orders.AddAsync(orderPersist);
            await context.SaveChangesAsync();
            await repository.DeleteAsync(orderPersist.Id);
            var result = context.Orders.FirstOrDefault(o => o.Id==orderPersist.Id);
            result.Should().BeNull();
        }
        [Fact]
        public async Task ShouldGetOrderAsync()
        {
            Guid idOrder = Guid.NewGuid();
            List<domain.OrderProduct.OrderProduct> listOrderProduct = new();
            foreach(var product in products){
            var OrderProduct = OrderProductBuilder.New(faker).WithIdOrder(idOrder).WithProduct(product).Build();
            listOrderProduct.Add(OrderProduct);
            }
            var order = OrderBuilder.New(faker).WithIdClient(client.Id).WithId(idOrder).WithOrderProducts(listOrderProduct).Build();
            var orderPersist = mapper.Map<infrastructure.Database.Entities.Order>(order);
            await context.Orders.AddAsync(orderPersist);
            await context.SaveChangesAsync();
            var result = await repository.GetOrderAsync(e=>e.Id==orderPersist.Id,1,10);
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task ShouldAtributesByUpdateAsync()
        {
            Guid idOrder = Guid.NewGuid();
            List<domain.OrderProduct.OrderProduct> listOrderProduct = new();
            foreach(var product in products){
            var OrderProduct = OrderProductBuilder.New(faker).WithIdOrder(idOrder).WithProduct(product).Build();
            listOrderProduct.Add(OrderProduct);
            }
            var order = OrderBuilder.New(faker).WithIdClient(client.Id).WithId(idOrder).WithOrderProducts(listOrderProduct).Build();
            var orderPersist = mapper.Map<infrastructure.Database.Entities.Order>(order);
            await context.Orders.AddAsync(orderPersist);
            await context.SaveChangesAsync();
            List<domain.OrderProduct.OrderProduct> listNewOrderProducts = new();
            int i=0;
            foreach(var product in products){
            var OrderProduct = OrderProductBuilder.New(faker).WithAmount(listOrderProduct[i].Amount+1).WithIdOrder(idOrder).WithProduct(product).Build();
            listNewOrderProducts.Add(OrderProduct);
            i++;
            }
            var newOrderAtributes = OrderBuilder.New(faker).WithIdClient(client.Id).WithId(idOrder).WithOrderProducts(listNewOrderProducts).Build();
            var result = await repository.UpdateAsync(newOrderAtributes);
            result?.TotalOrder.Should().NotBe(order.TotalOrder);
        }
    }  
}