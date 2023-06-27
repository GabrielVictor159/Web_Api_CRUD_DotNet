using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using gcsb.ecommerce.application.Interfaces.Repositories;
using gcsb.ecommerce.application.UseCases.Client.CreateClient;
using gcsb.ecommerce.application.UseCases.Client.GetClients;
using gcsb.ecommerce.tests.Builder.Domain.Client;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace gcsb.ecommerce.tests.Cases.Application.Client.GetClients
{
    [UseAutofacTestFramework]
    public class getClientsUseCaseTest
    {
        private readonly Faker faker;
        private readonly GetClientsUseCase GetClientsUseCase;
        private List<domain.Client.Client> clients = new();
        public getClientsUseCaseTest(
            Faker faker, 
            GetClientsUseCase GetClientsUseCase,
            IClientRepository clientRepository)
        {
            this.faker = faker;
            this.GetClientsUseCase = GetClientsUseCase;
            for(int i = 0; i<5;i++)
            {
                clients.Add(ClientBuilder.New(faker).Build());
            }
            foreach(var client in clients)
            {
                clientRepository.CreateAsync(client).Wait();
            }
        }
        [Fact]
        public async Task ShouldClientResultNotBeNullGetClientsUseCase()
        {
            Expression<Func<domain.Client.Client, bool>> func = p =>
             p.Name!.Contains("");
            var request = new GetClientsRequest(func,1,10);
            await GetClientsUseCase.Execute(request);
            request.clientResult.Should().NotBeNull();

        }
    }
}