using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.infrastructure.Database;
using gcsb.ecommerce.infrastructure.Database.Entities;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Infraestructure.Database.Repositories
{
    [UseAutofacTestFramework]
    public class ClientRespositoryTest
    {
        private readonly Faker faker;
        private readonly Context context;
        private readonly IClientRepository repository; 
        private readonly IMapper mapper;
        public ClientRespositoryTest(
            Faker faker,
            Context context,
            IClientRepository repository,
            IMapper mapper)
        {
            this.faker = faker;
            this.context = context;
            this.repository = repository;
            this.mapper = mapper;
        }
        [Fact]
        public async Task ShouldNotBeNullByCreateAsyncSucess()
        {
            var client = ClientBuilder.New(faker).Build();
            var result = await repository.CreateAsync(client);
            var clientResult = context.Clients.FirstOrDefault(c => c.Id == client.Id);
            clientResult.Should().NotBeNull();
        }
        [Fact]
        public async Task ShouldNotBeNullOrEmptyByGetAllByNameAsync()
        {
            var clientDomain = ClientBuilder.New(faker).Build();
            var client = mapper.Map<Client>(clientDomain);
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            var result = await repository.GetAllByNameAsync(clientDomain.Name);
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task ShouldNotBeNullOrEmptyByGetPagination()
        {
            var clientDomain = ClientBuilder.New(faker).Build();
            var client = mapper.Map<Client>(clientDomain);
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            var result = await repository.GetAllPagination(e=>e.Id==client.Id,1,10);
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task ShouldNotBeNullAndBeNullByGetClienteByIdAsync()
        {
            var clientDomain = ClientBuilder.New(faker).Build();
            var client = mapper.Map<Client>(clientDomain);
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            var result1 = await repository.GetClienteByIdAsync(client.Id);
            result1.Should().NotBeNull();
            var result2 = await repository.GetClienteByIdAsync(Guid.NewGuid());
            result2.Should().BeNull();
        }
        [Fact]
        public async Task ShouldUpdateAllAtributesByUpdateAsync()
        {
            var clientDomain = ClientBuilder.New(faker).Build();
            var client = mapper.Map<Client>(clientDomain);
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            var newAttributes = ClientBuilder.New(faker).WithId(client.Id).Build();
            var result = await repository.UpdateAsync(newAttributes);
            result.Should().NotBeNull();
            result?.Name.Should().Be(newAttributes.Name);
            result?.Role.Should().Be(newAttributes.Role);
        }
    }
}